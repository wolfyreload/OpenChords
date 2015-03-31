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
            this.imgOpenSongExecutableFolder = new System.Windows.Forms.PictureBox();
            this.imgOpenSongSetsFolder = new System.Windows.Forms.PictureBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtOpensongExecutable = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtOpenSongSetsAndSongs = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.chkPortableMode = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.imgSettingsFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgApplicationDataFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOpenSongExecutableFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOpenSongSetsFolder)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
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
            // txtOpensongExecutable
            // 
            this.txtOpensongExecutable.Location = new System.Drawing.Point(149, 3);
            this.txtOpensongExecutable.Name = "txtOpensongExecutable";
            this.txtOpensongExecutable.Size = new System.Drawing.Size(453, 20);
            this.txtOpensongExecutable.TabIndex = 1;
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 84);
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
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 149);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(643, 64);
            this.flowLayoutPanel2.TabIndex = 17;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel4.Controls.Add(this.chkUpdates);
            this.flowLayoutPanel4.Controls.Add(this.cmdCheck);
            this.flowLayoutPanel4.Controls.Add(this.chkPortableMode);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(643, 75);
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
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(649, 216);
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
            this.Size = new System.Drawing.Size(655, 222);
            ((System.ComponentModel.ISupportInitialize)(this.imgSettingsFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgApplicationDataFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOpenSongExecutableFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgOpenSongSetsFolder)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkUpdates;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtApplicationDataFolder;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtSettingsFolder;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtOpensongExecutable;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtOpenSongSetsAndSongs;
        private System.Windows.Forms.Button cmdCheck;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox imgSettingsFolder;
        private System.Windows.Forms.PictureBox imgApplicationDataFolder;
        private System.Windows.Forms.PictureBox imgOpenSongExecutableFolder;
        private System.Windows.Forms.PictureBox imgOpenSongSetsFolder;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.CheckBox chkPortableMode;
    }
}
