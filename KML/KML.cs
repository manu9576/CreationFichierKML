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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;


namespace CreationKML
{
    #region Enum
    // Definition des couleurs, le code hex correspond au codage de la couleur pour les fichiers KML
    public enum Couleur { rouge = 0x7f0000ff, bleu = 0x7fff0000, jaune = 0x7f00ffff, vert = 0x7f00ff00 };
    // Defini le type de valeur de la voie IMC
    public enum TypeVoie { sexadecimale, decimale };
    #endregion

    #region Delegate
    // declaration des delegate
    public delegate void CalculEffectue(KML nom, EventArgs e);  // produit lors de l'ajout d'un repertoire au fichier KML
    public delegate void CalculFini(KML nom, EvenArgsFin e);    // produit à la fin du calcul
    public delegate void AjoutLigneDataGrid(Object o, EventArgsString e);  //produit a la fin de la determination d'une ligne pour le dataGrid 
    public delegate void AjoutLigneDGFin(Object o, EventArgs e); // produit a la fin du remplissage de la DG

    #endregion

    #region Classes dérivées EventArgs

    /// <summary>
    /// argument pour la fin d'un calcul
    /// validité = true si pas d'erreur lors du calcul
    /// </summary>
    public class EvenArgsFin : EventArgs
    {
        // variable
        private bool m_validite;

        // propriete
        public bool Validite
        {
            get { return m_validite; }
        }

        // constructeur
        public EvenArgsFin(bool validite)
        {
            m_validite = validite;
        }
    }

    /// <summary>
    /// argument pour le passage d'une string 
    /// utilisé pour le remplissage du dataGrid
    /// </summary>
    public class EventArgsString : EventArgs
    {
        //variable
        private string[] m_str;

        //propriete
        public string[] Ligne
        {
            get { return m_str; }
            set { m_str = value; }
        }

        //constructeur
        public EventArgsString(string[] str)
        {
            m_str = str;

        }
    }

    #endregion

    #region struct
    /// <summary>
    /// structure pour le passage des parametres
    /// pour le thread remplissant la data grid
    /// NomFile : repertoire a scanner
    /// DataGrid : dataGrid à remplir
    /// </summary>
    public struct infoReplissageDataGrid
    {
        private string nomFile;
        private DataGridView dataGrid;

        public string NomFile
        {
            set { nomFile = value; }
            get { return nomFile; }
        }

        public DataGridView DataGrid
        {
            set { dataGrid = value; }
            get { return dataGrid; }
        }
    }
    #endregion


    public class KML
    {

        #region variables

        private static bool m_arretCalcul = false; // passe a true pour arreter le calcul

        // declaration des evenements
        public event CalculEffectue unTracetEstEcrit; //pour l'increment de la barre de progression
        public event CalculFini leCalculEstFini;    // pour prevenir de la fin du calcul


        // permet d'ecrire les coordonnées avec des points
        private CultureInfo m_culture = CultureInfo.CreateSpecificCulture("en-US");

        // parametre du fichier kml
        private string m_nomFichierKML;
        private string m_nom;
        private string m_longitude;
        private string m_latitude;

        private int m_precision; //distance mini entre deux points
        private int m_temps_mini = 1; // en minute

        private bool m_erreurTraitement = false; // passe à 1 si on genere une exception lors du traitement
        private bool m_DepArr; //ajout des emplacements depart et arrive au tracet

        private TypeVoie m_typeVoie;
        private Couleur m_clr;

        // parametre fichier log
        private string m_fichierLog;

        //verrou pour l'ecriture
        static private ReaderWriterLock rwl = new ReaderWriterLock(); // ecriture dans le fichier KML
        static private ReaderWriterLock rwl_log = new ReaderWriterLock(); // ecriture dans le log

        #endregion

        #region propriete

        /// <summary>
        /// Permet de lire le repertoire ou est généré le fichier log
        /// </summary>
        public string fichierLog
        {
            get { return m_fichierLog; }

        }

        /// <summary>
        /// Permet de modifier et lire le temps mini d'une acquisition
        /// </summary>
        public int Temps_mini
        {
            get { return m_temps_mini; }

            set
            {
                if (value >= 1 && value < 60) m_temps_mini = value;
                else throw new Exception("le temps mini doit être compris en 1 et 60.");

                ecritureLog("Modification : ");
                ecritureLog("temps minimum d'une acquisition : " + m_temps_mini + " min");
                ecritureLog("");
            }

        }

        /// <summary>
        /// Permet de modifier et lire si la creation des emplacements est prise en compte
        /// </summary>
        public bool DepArr
        {
            get { return m_DepArr; }
            set
            {
                m_DepArr = value;
                ecritureLog("Modification : ");
                ecritureLog(m_DepArr ? "Ajout des emplacements Depart - Arrivé" :
               "Sans les emplacements Depart - Arrivé.");
                ecritureLog("");
            }
        }

        #endregion

        /// <summary>
        /// Construteur du fichier kml
        /// </summary>
        /// <param name="fichier">emplacement et nom du fichier</param>
        /// <param name="nomLongitude">nom du fichier Longitude du type "F003_D11.raw"</param>
        /// <param name="nomLatitude">nom du fichier Latitude du type "F003_D08.raw"</param>
        /// <param name="typevoie">Enumerable : coordonnées sexadecimales ou decimales</param>
        /// <param name="precision"> distance mini entre deux points en metre</param>
        /// <param name="clr">Enumerable de la couleur du tracet</param>

        public KML(string fichier, string nomLongitude, string nomLatitude, TypeVoie typevoie, int precision = 30, Couleur clr = Couleur.bleu)
        {


            //renseigenement des variables menbres
            m_nomFichierKML = fichier;
            m_longitude = nomLongitude;
            m_latitude = nomLatitude;
            m_clr = clr;
            m_precision = precision;
            m_typeVoie = typevoie;

            m_arretCalcul = false;



            //recuperation du nom du fichier kml sans le chemin
            try
            {
                int indexTXT = fichier.LastIndexOf('\\') + 1;
                m_nom = fichier.Substring(indexTXT, fichier.Length - 4 - indexTXT);
            }
            catch (Exception e)
            {
                ecritureLog(" impossible de lire le nom du ficiher KML: " + e.Message);
                m_erreurTraitement = true;
            }


            //Creation du fichier log

            m_fichierLog = m_nom + "_log.txt";

            if (File.Exists(m_fichierLog)) File.Delete(m_fichierLog);
            ecritureLog("******   Creation du fichier MKL    *********");
            ecritureLog("Listes des paramétres : ");
            ecritureLog("nom de la voie longitude : " + m_longitude);
            ecritureLog("nom de la voie latitude : " + m_latitude);
            ecritureLog("type de données : " + m_typeVoie);
            ecritureLog("precision du trace : " + m_precision + " m");
            ecritureLog("couleur du tracet : " + m_clr);
            ecritureLog(m_DepArr ? "Ajout des emplacements Depart - Arrivé" :
                "Sans les emplacements Depart - Arrivé.");
            ecritureLog("temps minimum d'une acquisition : " + m_temps_mini + " min");
           

            try
            {
                CreationFichierKml();  // creation du fichier
                AjoutStyle();  // pour les couleurs

            }
            catch (Exception e)
            {
                ecritureLog(" l'erreur suivante s'est produite à l'ouverture du fichier : " + e.Message);
                m_erreurTraitement = true;
            }
            ecritureLog("");
        }

        #region fonction public


        /// <summary>
        /// Cloture le fichier
        /// </summary>
        public void Cloture()
        {
            ecritureLog("******   Cloture du fichier    *********");

            try
            {
                rwl.AcquireWriterLock(-1);
                using (StreamWriter sw = File.AppendText(m_nomFichierKML))
                {

                    sw.WriteLine("</Document>");
                    sw.WriteLine("</kml>");

                }
                rwl.ReleaseWriterLock();
            }
            catch (Exception e)
            {
                ecritureLog("La cloture du fichier a genéré l'erreur suivante : " + e.Message);
                m_erreurTraitement = true;
            }



        }

        /// <summary>
        /// Lance le traitement des fichiers
        /// </summary>
        /// <param name="O_Fichiers">sous forme String[]</param>
        public void AjoutTracets(object O_Fichiers)
        {

            string[] fichiers = (string[])O_Fichiers;

            ecritureLog("******   Ajout des tracets    *********");

            try
            {
                // determination des paramétres pour la parallélisation
                ParallelOptions PO = new ParallelOptions();
                PO.MaxDegreeOfParallelism = 2;

                //parallélisation des ajouts de tracet
                Parallel.ForEach<string>(fichiers, PO, fichier =>
                {
                    ecritureLog("Ajouts tracet : traitement du fichier " + fichier);
                    //calcul du tracet pour un repertoire
                    AjoutTracet(fichier);
                });

            }
            catch (Exception ex)
            {
                ecritureLog("Ajouts tracet : l'erreur suivante s'est produite" + ex.Message);
                m_erreurTraitement = true;
            }
            finally
            {
                // on cloture de toute facon le fichier kml -
                Cloture();
                //generation de l'évenement de fin de calul 
                leCalculEstFini(this, new EvenArgsFin(!m_erreurTraitement));
            }

        }

        /// <summary>
        /// Arrete le calcul
        /// passe la variable m_arretCalcul à true
        /// </summary>
        public static void ArretCalcul()
        {
            m_arretCalcul = true;
        }


        # endregion


        #region fonction private

        /// <summary>
        /// cree un tracet dans le ficier KML
        /// </summary>
        /// <param name="Repertoire contenant les fichiers de coordonnées"></param>
        private void AjoutTracet(string repetoire)
        {
           
            try
            {

                FichierGPS latitude = new FichierGPS(repetoire + @"\" + m_latitude);
                FichierGPS longitude = new FichierGPS(repetoire + @"\" + m_longitude);

                //test prealable des voies
                if (longitude.Count() != latitude.Count()) throw new Exception("les voies longitude et latitude ne font pas la meme longueur");
                // creation de l'entete  // nom description
                if (!File.Exists(m_nomFichierKML)) throw new Exception("ajoutTracet : le fichier KML n'existe pas");
                // on ne traite pas les acquisitions dont la duree d'acquis es t inféreiur à m_temps_mini
                if ((longitude.Count() * longitude.xDelta()) < m_temps_mini * 60)
                    throw new Exception("Acquisition de " + (longitude.Count() * longitude.xDelta()).ToString() + " sec. Inferieur au temps mini, pas de traitement");

                MemoryStream ms = new MemoryStream(); // utiliser pour l'ecriture temporaire
                using (StreamWriter sw = new StreamWriter(ms))
                {

                    // Ecriture de l'entete pour le dossier
                    sw.WriteLine("<Folder>");
                    sw.WriteLine("<name>" + repetoire.Substring(repetoire.LastIndexOf('\\') + 1) + "</name>");
                    sw.WriteLine(string.Format("<description> Duree : " + ((longitude.Count() * longitude.xDelta()) / 60).ToString("###.#")
                        + " min.</description>", (longitude.Count() * longitude.xDelta()) / 60));

                    //Ajout de l'emplacement Depart
                    int i = 0;
                    if (m_DepArr)
                        AjoutEmplacement("Départ", "", Conversion(longitude.get_Value(i)), Conversion(latitude.get_Value(i)), sw);

                    // Ecriture de l'entete pour le tracet
                    sw.WriteLine("<Placemark>");
                    sw.WriteLine("<name>Trajet</name>");
                    sw.WriteLine("<styleUrl>#" + m_clr + "Style</styleUrl>");
                    sw.WriteLine("<LineString>");
                    sw.WriteLine("<altitudeMode>clampToGround</altitudeMode>");
                    sw.WriteLine("<coordinates>");


                    while (++i < latitude.Count()-1 && !m_arretCalcul)  // on avance l'index jusqu'a la valeur suivante
                    {
                        sw.WriteLine("{0},{1}", Conversion(longitude.get_Value(i)).ToString("###.######", m_culture), Conversion(latitude.get_Value(i)).ToString("###.######", m_culture));

                        int suivant = RechercheValDiff(longitude, latitude, i);  // recherche de la valeur différente suivante

                        while ((suivant < latitude.Count() - 1) && 
                            Distance(Conversion(longitude.get_Value(i)), Conversion(latitude.get_Value(i)),Conversion(longitude.get_Value(suivant)), Conversion(latitude.get_Value(suivant))) < m_precision)
                        {
                            suivant++; // tant que la distance avec l'ancien point est ieférieur a la précision on passe au suivant
                            suivant = RechercheValDiff(longitude, latitude, suivant);
                        }

                        i = suivant;
                    }

                    if (m_arretCalcul) ecritureLog("Arret demandé par l'utilisateur.");

                    //sw.WriteLine("{0},{1}", Conversion(longitude.get_Value(i)).ToString("###.######", m_culture), Conversion(latitude.get_Value(i)).ToString("###.######", m_culture)); // ecriture de la derniere corrdonnée

                    // fin des coordonnées
                    sw.WriteLine("</coordinates>");
                    sw.WriteLine("</LineString>");
                    sw.WriteLine("</Placemark>");

                    if (m_DepArr)
                        AjoutEmplacement("Arrivé ", "", Conversion(longitude.get_Value(i)), Conversion(latitude.get_Value(i)), sw);
                    sw.WriteLine("</Folder>");

                    // Ecriture dans la memoire tampon
                    sw.Flush();
                    StreamReader SR = new StreamReader(ms);
                    ms.Seek(0, SeekOrigin.Begin);


                    // Ecriture dans le fichier KML du memorystream
                    rwl.AcquireWriterLock(-1);

                    using (StreamWriter SW = File.AppendText(m_nomFichierKML))
                    {
                        SW.Write(SR.ReadToEnd());
                    }
                    rwl.ReleaseWriterLock();

                    // generation de l'évenement de fin de calcul
                    unTracetEstEcrit(this, EventArgs.Empty);
                }

            }
            catch (Exception e)
            {
                ecritureLog("Ajout d'un tracet  - L'erreur suivante s'est porduite : " + e.Message);
                m_erreurTraitement = true;
            }
        }

        /// <summary>
        /// calcul de la ditance entre deux coordonnées GPS 
        /// renvoie la distance en métre
        /// Prevoir un try sur l'appel pour une division par 0
        /// Revoie -1 en cas d'erreur lors du calcul
        /// </summary>

        private double Distance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            try
            {
                // conversion des degres en radian
                double a = Math.PI / 180;

                double latRad1 = latitude1 * a;
                double latRad2 = latitude2 * a;
                double lonRad1 = longitude1 * a;
                double lonRad2 = longitude2 * a;

                // diamétre de la terre .... Sphére parfaite???

                double R = 6378000;

                double res = R * (Math.PI / 2 - Math.Asin(Math.Sin(latRad2) * Math.Sin(latRad1) + Math.Cos(lonRad2 - lonRad1) * Math.Cos(latRad2) * Math.Cos(latRad1)));

                return res;
            }
            catch (Exception e)
            {
                ecritureLog("Erreur lors du calcul de la distance : " + e.Message);
                m_erreurTraitement = true;
                return -1;
            }

        }

        /// <summary>
        /// Creer un fichier KML (Remplace un fichier exitant)
        /// </summary>
        /// <param name="nomFichier"></param> 
        private void CreationFichierKml()
        {
            ecritureLog("CreationFichierKml()");

            try
            {

                using (StreamWriter sw = File.CreateText(m_nomFichierKML))
                {

                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    sw.WriteLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\">");
                    sw.WriteLine(" <Document>");
                    sw.WriteLine("<name>" + m_nom + "</name>");
                    sw.WriteLine("<description> Calcul du : " + DateTime.Now + "</description>");

                }

            }

            catch (Exception e)
            {
                ecritureLog("Creation du fichier : " + e.Message);
                m_erreurTraitement = true;

            }
        }

        /// <summary>
        /// recherche l'index suivant ou l'une deux valeurs changent
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="index"></param>
        /// <returns>index des valeurs différentes suivantes</returns>
        private int RechercheValDiff(FichierGPS v1, FichierGPS v2, int index)
        {

            int indexCourant = index + 1;

            while ( (indexCourant < v1.Count() - 1) && 
                (v1.get_Value(indexCourant) == v1.get_Value(index))
                && (v2.get_Value(indexCourant) == v2.get_Value(index)) )
            {
                indexCourant++;
            }

            return indexCourant;
        }

        /// <summary>
        /// ajoute dans l'entete des styles de couleur pour les tracets
        /// </summary>
        private void AjoutStyle()
        {
            ecritureLog("AjoutStyle()");
            try
            {
                using (StreamWriter sw = File.AppendText(m_nomFichierKML))
                {
                    foreach (Couleur clr in Enum.GetValues(typeof(Couleur)))
                    {
                        sw.WriteLine("");
                        sw.WriteLine("<Style id=\"" + clr + "Style\">");
                        sw.WriteLine("<LineStyle>");
                        sw.WriteLine("<color>" + ((int)clr).ToString("x") + "</color>");
                        sw.WriteLine("<width>4</width>");
                        sw.WriteLine("</LineStyle>");
                        sw.WriteLine("</Style>");
                        sw.WriteLine("");
                    }
                }
            }
            catch (Exception e)
            {
                ecritureLog("Erreur lors de l'ecriture des style : " + e.Message);
                m_erreurTraitement = true;
            }

        }

        /// <summary>
        /// Cree un emplacement dans le flux passé en argument
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="description"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <StreamReader name="sw"></param>
        private void AjoutEmplacement(string nom, string description, double latitude, double longitude, StreamWriter sw)
        {

            try
            {
                sw.WriteLine("<Placemark>");
                sw.WriteLine("<name>" + nom + "</name>");

                if (description != "")
                {
                    sw.WriteLine("<description>");
                    sw.WriteLine(" <![CDATA[" + description + "]]>");
                    sw.WriteLine("</description>");
                }

                sw.WriteLine("<Point>");
                sw.WriteLine((string.Format("<coordinates>{0},{1}</coordinates>", latitude.ToString("###.######", m_culture), longitude.ToString("###.######", m_culture))));
                sw.WriteLine("</Point>");

                sw.WriteLine("</Placemark>");
            }
            catch (Exception e)
            {
                ecritureLog("Erreur lors de l'ecriture d'un emplacement : " + e.Message);
                m_erreurTraitement = true;
            }

        }

        /// <summary>
        /// Converti les valeurs de sexadéciaml en decimal
        /// que si m_typeVoie = TypeVoie.Sexadecimal
        /// </summary>
        /// <param name="value">valeur en decimal ou sexadecimal</param>
        /// <returns> la valeur en decimal</returns>
        private double Conversion(double value)
        {
            if (m_typeVoie == TypeVoie.decimale) return value;

            return Math.Floor(value / 100) + (value % 100) / 60;

        }

        /// <summary>
        /// Ecriture de la string dans le fichier log
        /// </summary>
        /// <param name="str">ligne a ecrire dans le log</param>
        private void ecritureLog(string str)
        {
            rwl_log.AcquireWriterLock(-1);

            StreamWriter sw = new StreamWriter(m_fichierLog, true);
            sw.WriteLine(DateTime.Now + " -- " + str);
            sw.Close();

            rwl_log.ReleaseWriterLock();

        }


        #endregion



    }


    # region remplissage DataGrid
    public class listeFichierRaw
    {
        public event AjoutLigneDataGrid ajoutLigneDataGrid;
        public event AjoutLigneDGFin finAjout;

        static bool calculEnCours = false;

        /// <summary>
        /// thread pour le remplissage de la dataGrid
        /// </summary>
        /// <param name="o">structure infoReplissageDataGrid</param>
        public void listeFichiers(object o)
        {
            if (!calculEnCours)
            {
                calculEnCours = true;

                string nomRep;
                DataGridView dataGrid;

                try
                {
                    infoReplissageDataGrid parametre = (infoReplissageDataGrid)o;
                    nomRep = parametre.NomFile;
                    dataGrid = parametre.DataGrid;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors du passage en argument de la fonction remplissageDataGrid :" + ex.Message);
                    throw;
                }

                //recuperation du nom de tous le fichiers du repertoire
                string[] files = Directory.GetFiles(nomRep);

                foreach (string file in files) // on parcourt chaque fichier du repertoire
                {

                    if (file.Contains(".raw") || file.Contains(".RAW")) // on ajout que les fichiers raw
                    {

                        //ouverture du ficiher raw
                        FichierGPS ficherRaw = new FichierGPS(file);
                        //ficherRaw.Open(file, DmFileOptionConstants.cdmFileQuickLoad);

                        // determination du nom du fichier, et du nom IMC
                        string nomFichier = file.Substring(file.LastIndexOf('\\') + 1);
                        string nomIMC = ficherRaw.Name;

                        // determination du max de la voie ( on lit 1 valeur sur 10)
                        //DChannel voie = ficherRaw.get_Value(1);
                        double max = 0;
                        for (int i = 1; i < ficherRaw.Length; i = i + 10) // on ne traite que 1 point sur 10 
                            if (max < ficherRaw.get_Value(i)) max = ficherRaw.get_Value(i);

                        // Ajout des champs dans le tabluea / envoi de la ligne
                        string[] ligne = { nomFichier, nomIMC, max.ToString("###.###") };


                        if (ajoutLigneDataGrid != null)
                            ajoutLigneDataGrid(this, new EventArgsString(ligne));

                    }

                }

                //fin de l'ajout des lignes
                if (finAjout != null)
                    finAjout(this, new EventArgs());

                calculEnCours = false;
            }
        }

    }

    #endregion

}