namespace CreationFichierKML
{
    partial class MainWindows
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.acquisitionTab = new System.Windows.Forms.TabPage();
            this.bt_effacerSelectionListe = new System.Windows.Forms.Button();
            this.bt_viderListe = new System.Windows.Forms.Button();
            this.tb_strRepAcq = new System.Windows.Forms.TextBox();
            this.bt_AjoutRepAcq = new System.Windows.Forms.Button();
            this.lb_strRep = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.parametreTab = new System.Windows.Forms.TabPage();
            this.bt_latitude = new System.Windows.Forms.Button();
            this.bt_long = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.nomFichier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomVoie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_strLatitude = new System.Windows.Forms.TextBox();
            this.tb_strLongitude = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_format = new System.Windows.Forms.GroupBox();
            this.rb_sexadecimal = new System.Windows.Forms.RadioButton();
            this.rb_Decimal = new System.Windows.Forms.RadioButton();
            this.fichierTab = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_DepArr = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nup_tempsMin = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_couleur = new System.Windows.Forms.ComboBox();
            this.nup_precision = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.bt_RepKML = new System.Windows.Forms.Button();
            this.tb_repKML = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.bt_calcul = new System.Windows.Forms.Button();
            this.bt_quitter = new System.Windows.Forms.Button();
            this.cb_About = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.acquisitionTab.SuspendLayout();
            this.parametreTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.gb_format.SuspendLayout();
            this.fichierTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_tempsMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_precision)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.acquisitionTab);
            this.tabControl.Controls.Add(this.parametreTab);
            this.tabControl.Controls.Add(this.fichierTab);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(404, 412);
            this.tabControl.TabIndex = 11;
            // 
            // acquisitionTab
            // 
            this.acquisitionTab.Controls.Add(this.bt_effacerSelectionListe);
            this.acquisitionTab.Controls.Add(this.bt_viderListe);
            this.acquisitionTab.Controls.Add(this.tb_strRepAcq);
            this.acquisitionTab.Controls.Add(this.bt_AjoutRepAcq);
            this.acquisitionTab.Controls.Add(this.lb_strRep);
            this.acquisitionTab.Controls.Add(this.label3);
            this.acquisitionTab.Location = new System.Drawing.Point(4, 22);
            this.acquisitionTab.Margin = new System.Windows.Forms.Padding(2);
            this.acquisitionTab.Name = "acquisitionTab";
            this.acquisitionTab.Padding = new System.Windows.Forms.Padding(2);
            this.acquisitionTab.Size = new System.Drawing.Size(396, 386);
            this.acquisitionTab.TabIndex = 1;
            this.acquisitionTab.Text = "Acquisitions à traiter";
            this.acquisitionTab.UseVisualStyleBackColor = true;
            // 
            // bt_effacerSelectionListe
            // 
            this.bt_effacerSelectionListe.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bt_effacerSelectionListe.Location = new System.Drawing.Point(151, 356);
            this.bt_effacerSelectionListe.Margin = new System.Windows.Forms.Padding(2);
            this.bt_effacerSelectionListe.Name = "bt_effacerSelectionListe";
            this.bt_effacerSelectionListe.Size = new System.Drawing.Size(116, 24);
            this.bt_effacerSelectionListe.TabIndex = 10;
            this.bt_effacerSelectionListe.Text = "Supprimer selection";
            this.bt_effacerSelectionListe.UseVisualStyleBackColor = true;
            this.bt_effacerSelectionListe.Click += new System.EventHandler(this.bt_effacerSelectionListe_Click);
            // 
            // bt_viderListe
            // 
            this.bt_viderListe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_viderListe.Location = new System.Drawing.Point(294, 356);
            this.bt_viderListe.Margin = new System.Windows.Forms.Padding(2);
            this.bt_viderListe.Name = "bt_viderListe";
            this.bt_viderListe.Size = new System.Drawing.Size(95, 24);
            this.bt_viderListe.TabIndex = 11;
            this.bt_viderListe.Text = "Vider la liste";
            this.bt_viderListe.UseVisualStyleBackColor = true;
            this.bt_viderListe.Click += new System.EventHandler(this.bt_viderListe_Click);
            this.bt_viderListe.StyleChanged += new System.EventHandler(this.bt_viderListe_Click);
            // 
            // tb_strRepAcq
            // 
            this.tb_strRepAcq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_strRepAcq.Enabled = false;
            this.tb_strRepAcq.Location = new System.Drawing.Point(87, 27);
            this.tb_strRepAcq.Margin = new System.Windows.Forms.Padding(2);
            this.tb_strRepAcq.Name = "tb_strRepAcq";
            this.tb_strRepAcq.Size = new System.Drawing.Size(303, 20);
            this.tb_strRepAcq.TabIndex = 7;
            this.tb_strRepAcq.TextChanged += new System.EventHandler(this.tb_strRepAcq_TextChanged);
            // 
            // bt_AjoutRepAcq
            // 
            this.bt_AjoutRepAcq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_AjoutRepAcq.Location = new System.Drawing.Point(6, 356);
            this.bt_AjoutRepAcq.Margin = new System.Windows.Forms.Padding(2);
            this.bt_AjoutRepAcq.Name = "bt_AjoutRepAcq";
            this.bt_AjoutRepAcq.Size = new System.Drawing.Size(124, 24);
            this.bt_AjoutRepAcq.TabIndex = 9;
            this.bt_AjoutRepAcq.Text = "Ajout d\'un répertoire";
            this.bt_AjoutRepAcq.UseVisualStyleBackColor = true;
            this.bt_AjoutRepAcq.Click += new System.EventHandler(this.bt_AjoutRepAcq_Click);
            // 
            // lb_strRep
            // 
            this.lb_strRep.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_strRep.FormattingEnabled = true;
            this.lb_strRep.Location = new System.Drawing.Point(6, 58);
            this.lb_strRep.Margin = new System.Windows.Forms.Padding(2);
            this.lb_strRep.Name = "lb_strRep";
            this.lb_strRep.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lb_strRep.Size = new System.Drawing.Size(384, 277);
            this.lb_strRep.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Repertoire";
            // 
            // parametreTab
            // 
            this.parametreTab.Controls.Add(this.bt_latitude);
            this.parametreTab.Controls.Add(this.bt_long);
            this.parametreTab.Controls.Add(this.dataGridView);
            this.parametreTab.Controls.Add(this.tb_strLatitude);
            this.parametreTab.Controls.Add(this.tb_strLongitude);
            this.parametreTab.Controls.Add(this.label2);
            this.parametreTab.Controls.Add(this.label1);
            this.parametreTab.Controls.Add(this.gb_format);
            this.parametreTab.Location = new System.Drawing.Point(4, 22);
            this.parametreTab.Margin = new System.Windows.Forms.Padding(2);
            this.parametreTab.Name = "parametreTab";
            this.parametreTab.Padding = new System.Windows.Forms.Padding(2);
            this.parametreTab.Size = new System.Drawing.Size(396, 386);
            this.parametreTab.TabIndex = 2;
            this.parametreTab.Text = "Paramétres";
            this.parametreTab.UseVisualStyleBackColor = true;
            // 
            // bt_latitude
            // 
            this.bt_latitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_latitude.Location = new System.Drawing.Point(318, 37);
            this.bt_latitude.Margin = new System.Windows.Forms.Padding(2);
            this.bt_latitude.Name = "bt_latitude";
            this.bt_latitude.Size = new System.Drawing.Size(78, 22);
            this.bt_latitude.TabIndex = 19;
            this.bt_latitude.Text = "Appliquer";
            this.bt_latitude.UseVisualStyleBackColor = true;
            this.bt_latitude.Click += new System.EventHandler(this.cb_latitude_Click);
            // 
            // bt_long
            // 
            this.bt_long.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_long.Location = new System.Drawing.Point(318, 6);
            this.bt_long.Margin = new System.Windows.Forms.Padding(2);
            this.bt_long.Name = "bt_long";
            this.bt_long.Size = new System.Drawing.Size(78, 24);
            this.bt_long.TabIndex = 18;
            this.bt_long.Text = "Appliquer";
            this.bt_long.UseVisualStyleBackColor = true;
            this.bt_long.Click += new System.EventHandler(this.cb_long_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nomFichier,
            this.nomVoie,
            this.Max});
            this.dataGridView.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.dataGridView.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridView.Location = new System.Drawing.Point(4, 63);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView.RowTemplate.Height = 50;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(392, 278);
            this.dataGridView.TabIndex = 17;
            // 
            // nomFichier
            // 
            this.nomFichier.HeaderText = "Nom fichier";
            this.nomFichier.Name = "nomFichier";
            this.nomFichier.ReadOnly = true;
            // 
            // nomVoie
            // 
            this.nomVoie.HeaderText = "Nom voie";
            this.nomVoie.Name = "nomVoie";
            this.nomVoie.ReadOnly = true;
            // 
            // Max
            // 
            this.Max.HeaderText = "Maximum de la voie";
            this.Max.Name = "Max";
            this.Max.ReadOnly = true;
            // 
            // tb_strLatitude
            // 
            this.tb_strLatitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_strLatitude.Enabled = false;
            this.tb_strLatitude.Location = new System.Drawing.Point(132, 38);
            this.tb_strLatitude.Margin = new System.Windows.Forms.Padding(2);
            this.tb_strLatitude.Name = "tb_strLatitude";
            this.tb_strLatitude.Size = new System.Drawing.Size(167, 20);
            this.tb_strLatitude.TabIndex = 15;
            // 
            // tb_strLongitude
            // 
            this.tb_strLongitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_strLongitude.Enabled = false;
            this.tb_strLongitude.Location = new System.Drawing.Point(132, 10);
            this.tb_strLongitude.Margin = new System.Windows.Forms.Padding(2);
            this.tb_strLongitude.Name = "tb_strLongitude";
            this.tb_strLongitude.Size = new System.Drawing.Size(167, 20);
            this.tb_strLongitude.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Nom de la voie latitude";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nom de la voie longitude";
            // 
            // gb_format
            // 
            this.gb_format.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_format.Controls.Add(this.rb_sexadecimal);
            this.gb_format.Controls.Add(this.rb_Decimal);
            this.gb_format.Location = new System.Drawing.Point(2, 346);
            this.gb_format.Margin = new System.Windows.Forms.Padding(2);
            this.gb_format.Name = "gb_format";
            this.gb_format.Padding = new System.Windows.Forms.Padding(2);
            this.gb_format.Size = new System.Drawing.Size(392, 37);
            this.gb_format.TabIndex = 11;
            this.gb_format.TabStop = false;
            this.gb_format.Text = "Format des coordonnées";
            // 
            // rb_sexadecimal
            // 
            this.rb_sexadecimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_sexadecimal.AutoSize = true;
            this.rb_sexadecimal.Location = new System.Drawing.Point(197, 15);
            this.rb_sexadecimal.Margin = new System.Windows.Forms.Padding(2);
            this.rb_sexadecimal.Name = "rb_sexadecimal";
            this.rb_sexadecimal.Size = new System.Drawing.Size(186, 17);
            this.rb_sexadecimal.TabIndex = 1;
            this.rb_sexadecimal.Text = "Sexadécimal (max voie sup à 100)";
            this.rb_sexadecimal.UseVisualStyleBackColor = true;
            // 
            // rb_Decimal
            // 
            this.rb_Decimal.AutoSize = true;
            this.rb_Decimal.Checked = true;
            this.rb_Decimal.Location = new System.Drawing.Point(11, 15);
            this.rb_Decimal.Margin = new System.Windows.Forms.Padding(2);
            this.rb_Decimal.Name = "rb_Decimal";
            this.rb_Decimal.Size = new System.Drawing.Size(158, 17);
            this.rb_Decimal.TabIndex = 0;
            this.rb_Decimal.TabStop = true;
            this.rb_Decimal.Text = "Décimal (max voie inf à 100)";
            this.rb_Decimal.UseVisualStyleBackColor = true;
            // 
            // fichierTab
            // 
            this.fichierTab.Controls.Add(this.cb_About);
            this.fichierTab.Controls.Add(this.groupBox1);
            this.fichierTab.Controls.Add(this.groupBox4);
            this.fichierTab.Location = new System.Drawing.Point(4, 22);
            this.fichierTab.Margin = new System.Windows.Forms.Padding(2);
            this.fichierTab.Name = "fichierTab";
            this.fichierTab.Padding = new System.Windows.Forms.Padding(2);
            this.fichierTab.Size = new System.Drawing.Size(396, 386);
            this.fichierTab.TabIndex = 3;
            this.fichierTab.Text = "Fichier de Sortie";
            this.fichierTab.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cb_DepArr);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.nup_tempsMin);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cb_couleur);
            this.groupBox1.Controls.Add(this.nup_precision);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(2, 108);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(386, 158);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options du tracet";
            // 
            // cb_DepArr
            // 
            this.cb_DepArr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_DepArr.AutoSize = true;
            this.cb_DepArr.Checked = true;
            this.cb_DepArr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_DepArr.Location = new System.Drawing.Point(271, 128);
            this.cb_DepArr.Margin = new System.Windows.Forms.Padding(2);
            this.cb_DepArr.Name = "cb_DepArr";
            this.cb_DepArr.Size = new System.Drawing.Size(15, 14);
            this.cb_DepArr.TabIndex = 20;
            this.cb_DepArr.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 128);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Indiquer les départs et les arrivées";
            // 
            // nup_tempsMin
            // 
            this.nup_tempsMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nup_tempsMin.Location = new System.Drawing.Point(273, 90);
            this.nup_tempsMin.Margin = new System.Windows.Forms.Padding(2);
            this.nup_tempsMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nup_tempsMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nup_tempsMin.Name = "nup_tempsMin";
            this.nup_tempsMin.Size = new System.Drawing.Size(46, 20);
            this.nup_tempsMin.TabIndex = 18;
            this.nup_tempsMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nup_tempsMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 94);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Temps minimum d\'une acquistion (en minute)";
            // 
            // cb_couleur
            // 
            this.cb_couleur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_couleur.FormattingEnabled = true;
            this.cb_couleur.Location = new System.Drawing.Point(273, 22);
            this.cb_couleur.Margin = new System.Windows.Forms.Padding(2);
            this.cb_couleur.Name = "cb_couleur";
            this.cb_couleur.Size = new System.Drawing.Size(110, 21);
            this.cb_couleur.TabIndex = 16;
            // 
            // nup_precision
            // 
            this.nup_precision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nup_precision.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nup_precision.Location = new System.Drawing.Point(273, 57);
            this.nup_precision.Margin = new System.Windows.Forms.Padding(2);
            this.nup_precision.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nup_precision.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nup_precision.Name = "nup_precision";
            this.nup_precision.Size = new System.Drawing.Size(46, 20);
            this.nup_precision.TabIndex = 15;
            this.nup_precision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nup_precision.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 61);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Précision du tracet (m)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 28);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Couleur du tracet";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.bt_RepKML);
            this.groupBox4.Controls.Add(this.tb_repKML);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(2, 12);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(386, 91);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "FIchier kml";
            // 
            // bt_RepKML
            // 
            this.bt_RepKML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_RepKML.Location = new System.Drawing.Point(252, 38);
            this.bt_RepKML.Margin = new System.Windows.Forms.Padding(2);
            this.bt_RepKML.Name = "bt_RepKML";
            this.bt_RepKML.Size = new System.Drawing.Size(130, 26);
            this.bt_RepKML.TabIndex = 12;
            this.bt_RepKML.Text = "Modification";
            this.bt_RepKML.UseVisualStyleBackColor = true;
            this.bt_RepKML.Click += new System.EventHandler(this.bt_RepKML_Click);
            // 
            // tb_repKML
            // 
            this.tb_repKML.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_repKML.Enabled = false;
            this.tb_repKML.Location = new System.Drawing.Point(19, 46);
            this.tb_repKML.Margin = new System.Windows.Forms.Padding(2);
            this.tb_repKML.Name = "tb_repKML";
            this.tb_repKML.Size = new System.Drawing.Size(218, 20);
            this.tb_repKML.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nom du fichier kml :";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(60, 17);
            this.StatusLabel.Text = "En attente";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(75, 16);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.ProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 451);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(406, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // bt_calcul
            // 
            this.bt_calcul.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_calcul.Location = new System.Drawing.Point(91, 416);
            this.bt_calcul.Margin = new System.Windows.Forms.Padding(2);
            this.bt_calcul.Name = "bt_calcul";
            this.bt_calcul.Size = new System.Drawing.Size(98, 27);
            this.bt_calcul.TabIndex = 1;
            this.bt_calcul.Text = "Calcul";
            this.bt_calcul.UseVisualStyleBackColor = true;
            this.bt_calcul.Click += new System.EventHandler(this.bt_calcul_Click);
            this.bt_calcul.StyleChanged += new System.EventHandler(this.bt_calcul_Click);
            // 
            // bt_quitter
            // 
            this.bt_quitter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_quitter.Location = new System.Drawing.Point(237, 416);
            this.bt_quitter.Margin = new System.Windows.Forms.Padding(2);
            this.bt_quitter.Name = "bt_quitter";
            this.bt_quitter.Size = new System.Drawing.Size(98, 27);
            this.bt_quitter.TabIndex = 2;
            this.bt_quitter.Text = "Quitter";
            this.bt_quitter.UseVisualStyleBackColor = true;
            this.bt_quitter.Click += new System.EventHandler(this.bt_quitter_Click);
            this.bt_quitter.StyleChanged += new System.EventHandler(this.bt_quitter_Click);
            // 
            // cb_About
            // 
            this.cb_About.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_About.Location = new System.Drawing.Point(283, 354);
            this.cb_About.Name = "cb_About";
            this.cb_About.Size = new System.Drawing.Size(108, 27);
            this.cb_About.TabIndex = 17;
            this.cb_About.Text = "A propos...";
            this.cb_About.UseVisualStyleBackColor = true;
            this.cb_About.Click += new System.EventHandler(this.cb_About_Click);
            // 
            // MainWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 473);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.bt_quitter);
            this.Controls.Add(this.bt_calcul);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(422, 512);
            this.Name = "MainWindows";
            this.Text = "Génération d\'un fichier kml";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindows_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.acquisitionTab.ResumeLayout(false);
            this.acquisitionTab.PerformLayout();
            this.parametreTab.ResumeLayout(false);
            this.parametreTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.gb_format.ResumeLayout(false);
            this.gb_format.PerformLayout();
            this.fichierTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_tempsMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_precision)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;



        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage acquisitionTab;
        private System.Windows.Forms.Button bt_viderListe;
        private System.Windows.Forms.Button bt_effacerSelectionListe;
        private System.Windows.Forms.Button bt_AjoutRepAcq;
        private System.Windows.Forms.TextBox tb_strRepAcq;
        private System.Windows.Forms.ListBox lb_strRep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage parametreTab;
        internal System.Windows.Forms.TextBox tb_strLatitude;
        internal System.Windows.Forms.TextBox tb_strLongitude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gb_format;
        private System.Windows.Forms.RadioButton rb_sexadecimal;
        private System.Windows.Forms.RadioButton rb_Decimal;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Button bt_calcul;
        private System.Windows.Forms.Button bt_quitter;
        private System.Windows.Forms.TabPage fichierTab;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button bt_RepKML;
        private System.Windows.Forms.TextBox tb_repKML;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button bt_latitude;
        private System.Windows.Forms.Button bt_long;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomFichier;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomVoie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Max;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_couleur;
        private System.Windows.Forms.NumericUpDown nup_precision;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nup_tempsMin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cb_DepArr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cb_About;
    }
}

