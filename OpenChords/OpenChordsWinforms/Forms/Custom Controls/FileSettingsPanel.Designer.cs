namespace OpenChords.Forms.Custom_Controls
{
    partial class FileSettingsPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileSettingsPanel));
            this.imgSettingsFolder = new System.Windows.Forms.PictureBox();
            this.imgApplicationDataFolder = new System.Windows.Forms.PictureBox();
            this.cmdCheck = new System.Windows.Forms.Button();
            this.chkUpdates = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtApplicationDataFolder = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtSettingsFolder = new System.Windows.Forms.TextBox();
            this.imgFileSyncFolder = new System.Windows.Forms.PictureBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtFileSync = new System.Windows.Forms.TextBox();
            this.imgOpenSongExecutableFolder = new System.Windows.Forms.PictureBox();
            this.imgOpenSongSetsFolder = new System.Windows.Forms.PictureBox();
            this.label28 = new System.Windows.Forms.Label();
            this.chkUseWelcomeSlide = new System.Windows.Forms.CheckBox();
            this.txtOpensongExecutable = new System.Windows.Forms.TextBox();
            this.txtWelcomeSlideName = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtOpenSongSetsAndSongs = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkPortableMode = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.imgSettingsFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgApplicationDataFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFileSyncFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOpenSongExecutableFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOpenSongSetsFolder)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgSettingsFolder
            // 
            this.imgSettingsFolder.Image = ((System.Drawing.Image)(resources.GetObject("imgSettingsFolder.Image")));
            this.imgSettingsFolder.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgSettingsFolder.InitialImage")));
            this.imgSettingsFolder.Location = new System.Drawing.Point(608, 3);
            this.imgSettingsFolder.Name = "imgSettingsFolder";
            this.imgSettingsFolder.Size = new System.Drawing.Size(20, 20);
            this.imgSettingsFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgSettingsFolder.TabIndex = 17;
            this.imgSettingsFolder.TabStop = false;
            this.imgSettingsFolder.Click += new System.EventHandler(this.imgSettingsFolder_Click);
            // 
            // imgApplicationDataFolder
            // 
            this.imgApplicationDataFolder.Image = ((System.Drawing.Image)(resources.GetObject("imgApplicationDataFolder.Image")));
            this.imgApplicationDataFolder.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgApplicationDataFolder.InitialImage")));
            this.imgApplicationDataFolder.Location = new System.Drawing.Point(608, 29);
            this.imgApplicationDataFolder.Name = "imgApplicationDataFolder";
            this.imgApplicationDataFolder.Size = new System.Drawing.Size(20, 20);
            this.imgApplicationDataFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgApplicationDataFolder.TabIndex = 16;
            this.imgApplicationDataFolder.TabStop = false;
            this.imgApplicationDataFolder.Click += new System.EventHandler(this.imgApplicationDataFolder_Click);
            // 
            // cmdCheck
            // 
            this.flowLayoutPanel4.SetFlowBreak(this.cmdCheck, true);
            this.cmdCheck.ForeColor = System.Drawing.Color.Black;
            this.cmdCheck.Location = new System.Drawing.Point(169, 3);
            this.cmdCheck.Name = "cmdCheck";
            this.cmdCheck.Size = new System.Drawing.Size(56, 36);
            this.cmdCheck.TabIndex = 15;
            this.cmdCheck.Text = "Check Now";
            this.cmdCheck.UseVisualStyleBackColor = true;
            this.cmdCheck.Click += new System.EventHandler(this.cmdCheck_Click);
            // 
            // chkUpdates
            // 
            this.chkUpdates.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUpdates.ForeColor = System.Drawing.Color.White;
            this.chkUpdates.Location = new System.Drawing.Point(3, 3);
            this.chkUpdates.Name = "chkUpdates";
            this.chkUpdates.Size = new System.Drawing.Size(160, 17);
            this.chkUpdates.TabIndex = 14;
            this.chkUpdates.Text = "Check for updates on start";
            this.chkUpdates.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUpdates.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.ForeColor = System.Drawing.Color.White;
            this.label30.Location = new System.Drawing.Point(3, 26);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(140, 23);
            this.label30.TabIndex = 3;
            this.label30.Text = "Application Data Folder";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtApplicationDataFolder
            // 
            this.txtApplicationDataFolder.Location = new System.Drawing.Point(149, 29);
            this.txtApplicationDataFolder.Name = "txtApplicationDataFolder";
            this.txtApplicationDataFolder.Size = new System.Drawing.Size(453, 20);
            this.txtApplicationDataFolder.TabIndex = 2;
            // 
            // label27
            // 
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(3, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(140, 23);
            this.label27.TabIndex = 1;
            this.label27.Text = "Settings Folder";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSettingsFolder
            // 
            this.txtSettingsFolder.Location = new System.Drawing.Point(149, 3);
            this.txtSettingsFolder.Name = "txtSettingsFolder";
            this.txtSettingsFolder.Size = new System.Drawing.Size(453, 20);
            this.txtSettingsFolder.TabIndex = 1;
            // 
            // imgFileSyncFolder
            // 
            this.imgFileSyncFolder.Image = ((System.Drawing.Image)(resources.GetObject("imgFileSyncFolder.Image")));
            this.imgFileSyncFolder.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgFileSyncFolder.InitialImage")));
            this.imgFileSyncFolder.Location = new System.Drawing.Point(608, 3);
            this.imgFileSyncFolder.Name = "imgFileSyncFolder";
            this.imgFileSyncFolder.Size = new System.Drawing.Size(20, 20);
            this.imgFileSyncFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgFileSyncFolder.TabIndex = 13;
            this.imgFileSyncFolder.TabStop = false;
            this.imgFileSyncFolder.Click += new System.EventHandler(this.imgFileSyncExecutable_Click);
            // 
            // label32
            // 
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(3, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(140, 23);
            this.label32.TabIndex = 13;
            this.label32.Text = "File Sync Tool Executable";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFileSync
            // 
            this.txtFileSync.Location = new System.Drawing.Point(149, 3);
            this.txtFileSync.Name = "txtFileSync";
            this.txtFileSync.Size = new System.Drawing.Size(453, 20);
            this.txtFileSync.TabIndex = 1;
            // 
            // imgOpenSongExecutableFolder
            // 
            this.imgOpenSongExecutableFolder.Image = ((System.Drawing.Image)(resources.GetObject("imgOpenSongExecutableFolder.Image")));
            this.imgOpenSongExecutableFolder.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgOpenSongExecutableFolder.InitialImage")));
            this.imgOpenSongExecutableFolder.Location = new System.Drawing.Point(608, 3);
            this.imgOpenSongExecutableFolder.Name = "imgOpenSongExecutableFolder";
            this.imgOpenSongExecutableFolder.Size = new System.Drawing.Size(20, 20);
            this.imgOpenSongExecutableFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgOpenSongExecutableFolder.TabIndex = 12;
            this.imgOpenSongExecutableFolder.TabStop = false;
            this.imgOpenSongExecutableFolder.Click += new System.EventHandler(this.imgOpenSongExecutable_Click);
            // 
            // imgOpenSongSetsFolder
            // 
            this.imgOpenSongSetsFolder.Image = ((System.Drawing.Image)(resources.GetObject("imgOpenSongSetsFolder.Image")));
            this.imgOpenSongSetsFolder.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgOpenSongSetsFolder.InitialImage")));
            this.imgOpenSongSetsFolder.Location = new System.Drawing.Point(608, 29);
            this.imgOpenSongSetsFolder.Name = "imgOpenSongSetsFolder";
            this.imgOpenSongSetsFolder.Size = new System.Drawing.Size(20, 20);
            this.imgOpenSongSetsFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgOpenSongSetsFolder.TabIndex = 11;
            this.imgOpenSongSetsFolder.TabStop = false;
            this.imgOpenSongSetsFolder.Click += new System.EventHandler(this.imgOpenSongSetsFolder_Click);
            // 
            // label28
            // 
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(3, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(140, 23);
            this.label28.TabIndex = 2;
            this.label28.Text = "OpenSong Executable";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkUseWelcomeSlide
            // 
            this.chkUseWelcomeSlide.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseWelcomeSlide.ForeColor = System.Drawing.Color.White;
            this.chkUseWelcomeSlide.Location = new System.Drawing.Point(3, 75);
            this.chkUseWelcomeSlide.Name = "chkUseWelcomeSlide";
            this.chkUseWelcomeSlide.Size = new System.Drawing.Size(160, 22);
            this.chkUseWelcomeSlide.TabIndex = 4;
            this.chkUseWelcomeSlide.Text = "Use Welcome Slide";
            this.chkUseWelcomeSlide.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseWelcomeSlide.UseVisualStyleBackColor = true;
            // 
            // txtOpensongExecutable
            // 
            this.txtOpensongExecutable.Location = new System.Drawing.Point(149, 3);
            this.txtOpensongExecutable.Name = "txtOpensongExecutable";
            this.txtOpensongExecutable.Size = new System.Drawing.Size(453, 20);
            this.txtOpensongExecutable.TabIndex = 1;
            // 
            // txtWelcomeSlideName
            // 
            this.txtWelcomeSlideName.Location = new System.Drawing.Point(149, 55);
            this.txtWelcomeSlideName.Name = "txtWelcomeSlideName";
            this.txtWelcomeSlideName.Size = new System.Drawing.Size(453, 20);
            this.txtWelcomeSlideName.TabIndex = 3;
            this.txtWelcomeSlideName.Visible = false;
            // 
            // label29
            // 
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(3, 26);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(140, 23);
            this.label29.TabIndex = 4;
            this.label29.Text = "OpenSong Songs and Sets Folder";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(3, 52);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(140, 23);
            this.label31.TabIndex = 8;
            this.label31.Text = "Welcome Slide Name";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label31.Visible = false;
            // 
            // txtOpenSongSetsAndSongs
            // 
            this.txtOpenSongSetsAndSongs.Location = new System.Drawing.Point(149, 29);
            this.txtOpenSongSetsAndSongs.Name = "txtOpenSongSetsAndSongs";
            this.txtOpenSongSetsAndSongs.Size = new System.Drawing.Size(453, 20);
            this.txtOpenSongSetsAndSongs.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.CheckFileExists = false;
            this.openFileDialog1.CheckPathExists = false;
            this.openFileDialog1.FilterIndex = 0;
            this.openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.ValidateNames = false;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Controls.Add(this.label27);
            this.flowLayoutPanel1.Controls.Add(this.txtSettingsFolder);
            this.flowLayoutPanel1.Controls.Add(this.imgSettingsFolder);
            this.flowLayoutPanel1.Controls.Add(this.label30);
            this.flowLayoutPanel1.Controls.Add(this.txtApplicationDataFolder);
            this.flowLayoutPanel1.Controls.Add(this.imgApplicationDataFolder);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 112);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(643, 59);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel2.Controls.Add(this.label28);
            this.flowLayoutPanel2.Controls.Add(this.txtOpensongExecutable);
            this.flowLayoutPanel2.Controls.Add(this.imgOpenSongExecutableFolder);
            this.flowLayoutPanel2.Controls.Add(this.label29);
            this.flowLayoutPanel2.Controls.Add(this.txtOpenSongSetsAndSongs);
            this.flowLayoutPanel2.Controls.Add(this.imgOpenSongSetsFolder);
            this.flowLayoutPanel2.Controls.Add(this.label31);
            this.flowLayoutPanel2.Controls.Add(this.txtWelcomeSlideName);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 177);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(643, 87);
            this.flowLayoutPanel2.TabIndex = 17;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel3.Controls.Add(this.label32);
            this.flowLayoutPanel3.Controls.Add(this.txtFileSync);
            this.flowLayoutPanel3.Controls.Add(this.imgFileSyncFolder);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 270);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(643, 40);
            this.flowLayoutPanel3.TabIndex = 18;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel4.Controls.Add(this.chkUpdates);
            this.flowLayoutPanel4.Controls.Add(this.cmdCheck);
            this.flowLayoutPanel4.Controls.Add(this.chkPortableMode);
            this.flowLayoutPanel4.Controls.Add(this.chkUseWelcomeSlide);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(643, 103);
            this.flowLayoutPanel4.TabIndex = 19;
            // 
            // chkPortableMode
            // 
            this.flowLayoutPanel4.SetFlowBreak(this.chkPortableMode, true);
            this.chkPortableMode.ForeColor = System.Drawing.Color.White;
            this.chkPortableMode.Location = new System.Drawing.Point(3, 45);
            this.chkPortableMode.Name = "chkPortableMode";
            this.chkPortableMode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkPortableMode.Size = new System.Drawing.Size(160, 24);
            this.chkPortableMode.TabIndex = 18;
            this.chkPortableMode.Text = "Portable Mode";
            this.chkPortableMode.UseVisualStyleBackColor = true;
            this.chkPortableMode.CheckedChanged += new System.EventHandler(this.chkPortableMode_CheckedChanged);
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel5.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanel5.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel5.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(649, 313);
            this.flowLayoutPanel5.TabIndex = 20;
            // 
            // FileSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.Controls.Add(this.flowLayoutPanel5);
            this.Name = "FileSettingsPanel";
            this.Size = new System.Drawing.Size(655, 319);
            ((System.ComponentModel.ISupportInitialize)(this.imgSettingsFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgApplicationDataFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFileSyncFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOpenSongExecutableFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOpenSongSetsFolder)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkUpdates;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtFileSync;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtApplicationDataFolder;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtSettingsFolder;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.CheckBox chkUseWelcomeSlide;
        private System.Windows.Forms.TextBox txtOpensongExecutable;
        private System.Windows.Forms.TextBox txtWelcomeSlideName;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtOpenSongSetsAndSongs;
        private System.Windows.Forms.Button cmdCheck;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox imgSettingsFolder;
        private System.Windows.Forms.PictureBox imgApplicationDataFolder;
        private System.Windows.Forms.PictureBox imgFileSyncFolder;
        private System.Windows.Forms.PictureBox imgOpenSongExecutableFolder;
        private System.Windows.Forms.PictureBox imgOpenSongSetsFolder;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.CheckBox chkPortableMode;
    }
}
