///
///Copyright (c) 2013, E. Lemaistre
///All rights reserved.
///
///Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
///
///    Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
///    Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
///
///THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows;
using System.Configuration;
using System.Threading;
using CreationKML;
using FenetreAbout;



namespace CreationFichierKML
{
    public partial class MainWindows : Form
    {

        private DateTime m_debutTraitement;  // permettra de calculer le temps de traitement
        CreationKML.KML m_fichier; // l'initialisation permet l'utilisation le renseignement des voies avant le traitement
        listeFichierRaw liste;

        /// <summary>
        /// Initialisation de la fenetre principale
        /// </summary>
        public MainWindows()
        {
            InitializeComponent();
            // Utilisé pour le remplissage de la dataGrid en Thread
            liste = new listeFichierRaw();
            liste.ajoutLigneDataGrid += new CreationKML.AjoutLigneDataGrid(ajoutLigne);
            liste.finAjout += new CreationKML.AjoutLigneDGFin(finRemplissageDataGrid);

            // Chargement des settings
            tb_strLatitude.Text = Properties.Settings.Default.nomLatitude;
            tb_strLongitude.Text = Properties.Settings.Default.nomLongitude;
            tb_strRepAcq.Text = Properties.Settings.Default.repertoire;
            rb_Decimal.Checked = Properties.Settings.Default.formatDecimal;
            rb_sexadecimal.Checked = !Properties.Settings.Default.formatDecimal;
            nup_precision.Value = Properties.Settings.Default.precision;
            tb_repKML.Text = Properties.Settings.Default.repKML;
            nup_tempsMin.Value = Properties.Settings.Default.tempsMin;
            cb_DepArr.Checked = Properties.Settings.Default.DepArr;

            //remplissage de la combo box avec le couleur de l'enum
            foreach (CreationKML.Couleur clr in Enum.GetValues(typeof(CreationKML.Couleur)))
                cb_couleur.Items.Add(clr);
            cb_couleur.SelectedIndex = 0;

            // Selection de l'icone
            this.Icon = Properties.Resources.google_earth;

            // Paramétrage de la fenetre de recherche d'un repertoire
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.SelectedPath = Properties.Settings.Default.repertoire;

            // Parametrage de la fenetre de recherche de nom ficheir
            saveFileDialog.FileName = "fichierKML";
            saveFileDialog.DefaultExt = "kml";
            saveFileDialog.Filter = "fichier kml (*.kml)|*.kml";
            saveFileDialog.InitialDirectory = Properties.Settings.Default.repKML;



        }

        /// <summary>
        /// Lancement du calcul sur les repertoires indiqué dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_calcul_Click(object sender, EventArgs e)
        {

            #region Validation des champs
            if (tb_strLatitude.Text == "" || tb_strLongitude.Text == "")
            {
                MessageBox.Show("Les champs longitude et latitude ne sont pas correctement renseignés.", "Erreur"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lb_strRep.Items.Count == 0)
            {
                MessageBox.Show("Il n'y a pas de dossier à traiter.", "Erreur"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_repKML.Text == "" || !tb_repKML.Text.EndsWith(".kml"))
            {
                MessageBox.Show("Le champ du repertoire KML n'est pas correctement renseigné.", "Erreur"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #endregion Validation des champs

            //Creation du fichier KML
            ActivationDesactivationControl(false);

            try
            {

                // creation du fichier KML
                m_fichier = new CreationKML.KML(
                    tb_repKML.Text, tb_strLongitude.Text,
                    tb_strLatitude.Text,
                    (rb_Decimal.Checked ? CreationKML.TypeVoie.decimale : CreationKML.TypeVoie.sexadecimale),
                    (int)nup_precision.Value,
                    (CreationKML.Couleur)cb_couleur.SelectedItem);

                // parametrage des proprietes
                m_fichier.Temps_mini = (int)nup_tempsMin.Value;
                m_fichier.DepArr = cb_DepArr.Checked;

                // abonnement aux evenements du calcul
                m_fichier.unTracetEstEcrit += new CreationKML.CalculEffectue(incrementProgressBar);
                m_fichier.leCalculEstFini += new CreationKML.CalculFini(finTraitement);

                // recupération des fichiers a traiter
                string[] liste = lb_strRep.Items.OfType<object>().Select(item => item.ToString()).ToArray();

                // preparation de la status bar
                StatusLabel.Text = " Calcul en cours";
                ProgressBar.Value = 0;
                ProgressBar.Maximum = liste.Length;

                // recuperaiton de la date heure du debut du traitnement
                m_debutTraitement = DateTime.Now;

                //lancement du thread
                Thread calcul = new Thread(new ParameterizedThreadStart(m_fichier.AjoutTracets));
                calcul.Priority = ThreadPriority.Highest; // gain de 5 sec sur 45 sec....
                calcul.Start(liste);

            }

            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
                ProgressBar.Value = 0;
                ActivationDesactivationControl(true);
            }


        }

        /// <summary>
        /// A chaque evement finAjoutTracet, on increment la progressbar
        /// </summary>
        /// <param name="o">sender</param>
        /// <param name="e">argument </param>
        private void incrementProgressBar(Object o, EventArgs e)
        {

            this.BeginInvoke((Action)(() => ProgressBar.Increment(1)));

        }

        /// <summary>
        /// Traitement effecuter lors de la l"evenemnt fin de cacul
        /// Activation des controls
        /// Mise a jour de la status bar
        /// Avertisserment en cas d'errer lors du calcul
        /// </summary>
        /// <param name="o">sender</param>
        /// <param name="e">EvenArgsfin // =1 si traitement sans erreur</param>
        private void finTraitement(Object o, CreationKML.EvenArgsFin e)
        {

            this.BeginInvoke((Action)(() =>
            {
                // activation des controles
                ActivationDesactivationControl(true);

                // status bar
                StatusLabel.Text = " Calcul fini en " + (DateTime.Now - m_debutTraitement).ToString() + " sec";
                ProgressBar.Value = 0;

                if (!e.Validite)  // dans ce cas il y a eu des erreurs, on previens l'utlisateur.
                {
                    if (MessageBox.Show("Des erreurs se sont produites lors du traitement. \r Voulez-vous consulter le fichier log pour plus de détail?",
                        "Erreurs", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        // ouverture du fichier log
                        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(m_fichier.fichierLog, "");
                        System.Diagnostics.Process.Start(psi);
                    }

                }
            }));



        }

        /// <summary>
        /// renseigenemt de la texteBox avec le repertoire des acquisitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_AjoutRepAcq_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tb_strRepAcq.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// mettre une string dans la propiete Ligne 
        /// pour ajouter une ligne a la dataGrid
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void ajoutLigne(Object o, CreationKML.EventArgsString e)
        {

            try
            {
                this.BeginInvoke((Action)(() => dataGridView.Rows.Add(e.Ligne)));
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du renseignement du nom des voies : " + ex.Message);
                ProgressBar.Value = 0;
                ActivationDesactivationControl(true);
            }

        }

        /// <summary>
        /// On reactive les controles une fois que le remplissage est terminé
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void finRemplissageDataGrid(Object o, EventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                ActivationDesactivationControl(true);
                ProgressBar.Style = ProgressBarStyle.Continuous;
                StatusLabel.Text = "En attente";

            }
             ));



        }

        /// <summary>
        /// Quitter l'application
        /// ou
        /// Arret le du calcul
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_quitter_Click(object sender, EventArgs e)
        {
            if (bt_quitter.Text == "Quitter")
            {
                sauvegardeDesSettings();
                this.Close();
            }
            else
                CreationKML.KML.ArretCalcul();
        }

        /// <summary>
        /// Reseigne la texteBox avec le nom du fichier KML voulu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_RepKML_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                tb_repKML.Text = saveFileDialog.FileName;
            }
        }

        /// <summary>
        /// Appel d'"AjoutListeRep" lors de la modification du nom 
        /// de la textBox repAcq
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_strRepAcq_TextChanged(object sender, EventArgs e)
        {
            // si le texte est vide, on quitte
            if (tb_strRepAcq.Text == String.Empty) return;

            // on ajoute les repertoires dans la liste
            MiseAJourListeRep();

            // si la liste est vide, on quitte
            if (lb_strRep.Items.Count == 0) return;

            //recupération le nom du premier repertoire de la liste
            string nomRep = lb_strRep.Items[0].ToString();

            //on vide la datagridView
            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            //creation du thread pour le remplissage de la dataGrid
            Thread remplissage = new Thread(new ParameterizedThreadStart(liste.listeFichiers));

            //Creation de la strucuture pour passer les parametres du remplissage
            CreationKML.infoReplissageDataGrid parametre = new CreationKML.infoReplissageDataGrid { DataGrid = dataGridView, NomFile = nomRep };

            remplissage.Start(parametre);

            // On désactive des controles pour eviter de relancer plusieur fois une remplissage
            ActivationDesactivationControl(false);

            ProgressBar.Style = ProgressBarStyle.Marquee;
            StatusLabel.Text = "Remplissage du tableau des voies";

        }

        /// <summary>
        /// On sauve les settings avant la fermeture de la form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindows_FormClosing(object sender, FormClosingEventArgs e)
        {
            sauvegardeDesSettings();
        }

        /// <summary>
        /// recuperation des champs de l'interface pour les affecter
        /// dans les variables de settings
        /// sauvegarde des settings
        /// </summary>
        private void sauvegardeDesSettings()
        {
            Properties.Settings.Default.nomLatitude = tb_strLatitude.Text;
            Properties.Settings.Default.nomLongitude = tb_strLongitude.Text;
            Properties.Settings.Default.formatDecimal = rb_Decimal.Checked;
            Properties.Settings.Default.precision = (int)nup_precision.Value;
            Properties.Settings.Default.repKML = tb_repKML.Text;
            Properties.Settings.Default.tempsMin = (int)nup_tempsMin.Value;
            Properties.Settings.Default.repertoire = tb_strRepAcq.Text;
            Properties.Settings.Default.DepArr = cb_DepArr.Checked;

            Properties.Settings.Default.Save();


        }

        /// <summary>
        /// Vider la listeBox Rep des repertoires d'acquisition
        /// vide aussi le champ RepAcq
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_viderListe_Click(object sender, EventArgs e)
        {
            tb_strRepAcq.Text = "";
            lb_strRep.Items.Clear();

            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        /// <summary>
        /// Supprime les elements selectionnés de la listeBox Rep
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_effacerSelectionListe_Click(object sender, EventArgs e)
        {

            while (lb_strRep.SelectedItem != null)
                lb_strRep.Items.Remove(lb_strRep.SelectedItem);


        }

        /// <summary>
        /// Met a jour la liste des acqus en fonction du nom de RepAcq
        /// </summary>
        private void MiseAJourListeRep()
        {
            string nomRep = tb_strRepAcq.Text;

            if ((nomRep.Trim() == String.Empty)||(!Directory.Exists(nomRep))) return;

            // test  de l'existance des fichiers RAW directement dans le repertoire si oui on ajoute a la liste


            // determine si il a y des fichiers RAW dans le repertoire.
          
            string[] files = Directory.GetFiles(nomRep);
            bool ajouterRep = false;

            foreach (string str in files)
                if (str.Contains(".raw") || str.Contains(".RAW"))
                    ajouterRep = true;

            if (ajouterRep) //si oui on ajoute juste le repertoire
                lb_strRep.Items.Add(nomRep);

            else // sinon on traite le cas d'une liste de repertoire de fichier d'acqui
            {
                if (!Directory.Exists(nomRep)) return;

                string[] listeDossier = System.IO.Directory.GetDirectories(nomRep);  // resuperation de la liste de fichier

                foreach (string repertoireCourant in listeDossier) // on boucle sur chaque rep contenu
                {
                    string[] Listefichiers = System.IO.Directory.GetFiles(repertoireCourant); // on liste les fichiers

                    for (int i = 0; i < Listefichiers.Length; i++)
                    {
                        // test  de l'existance des fichiers RAW directement dans le repertoire si oui on ajoute a la liste
                        if (Listefichiers[i].Contains(".raw") || Listefichiers[i].Contains(".RAW"))
                        {
                            lb_strRep.Items.Add(repertoireCourant);
                            break;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// applique le bool à la propriete Enabled des boutnons
        /// modif le texte du bouton QUitter <> Arret calcul
        /// </summary>
        /// <param name="etat"></param>
        private void ActivationDesactivationControl(bool etat)
        {
            // bouton
            bt_AjoutRepAcq.Enabled = etat;
            bt_viderListe.Enabled = etat;
            bt_calcul.Enabled = etat;
            bt_RepKML.Enabled = etat;
            bt_effacerSelectionListe.Enabled = etat;
            bt_long.Enabled = etat;
            bt_latitude.Enabled = etat;

            //spinbox
            nup_precision.Enabled = etat;
            nup_tempsMin.Enabled = etat;

            //goupebob
            gb_format.Enabled = etat;

            //combobox
            cb_couleur.Enabled = etat;

            //checkbox
            cb_DepArr.Enabled = etat;

            //gridView
            dataGridView.Enabled = etat;

            // modification du bouton quitter en Arret calcul
            if (etat)
                bt_quitter.Text = "Quitter";
            else
                bt_quitter.Text = "Arret calcul";
        }

        /// <summary>
        /// assigne au textBox Latitude le nom fichier indiquer dans la gridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_long_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 1)
                tb_strLongitude.Text = (string)dataGridView.CurrentRow.Cells[0].Value;
        }

        /// <summary>
        /// assigne au textBox longitude le nom fichier indiquer dans la gridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_latitude_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 1)
                tb_strLatitude.Text = (string)dataGridView.CurrentRow.Cells[0].Value;
        }


        private void ShowAbout()
        {

            FenetreAboutClass ab = new FenetreAboutClass();
            ab.AppTitle = Application.ProductName;
            ab.AppDescription = "";
            ab.AppVersion = Application.ProductVersion;
            ab.AppCopyright = "Copyright (c) 2013, E. Lemaistre";
            ab.AppMoreInfo = Properties.Resources.Licence;
            ab.AppDetailsButton = true;
            ab.Icon = Properties.Resources.google_earth;
            ab.ShowDialog(this);
        }

        private void cb_About_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }


    }
}
