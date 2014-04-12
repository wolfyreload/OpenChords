namespace OpenChords.Forms
{
    partial class Settings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPageFileSettings = new System.Windows.Forms.TabPage();
            this.fileSettingsCustomControl = new OpenChords.Forms.Custom_Controls.FileSettingsPanel();
            this.tabPageDisplaySettings = new System.Windows.Forms.TabPage();
            this.displaySettingsCustomControl = new OpenChords.Forms.Custom_Controls.SettingsPanel();
            this.tabPager = new System.Windows.Forms.TabControl();
            this.tabPagePrintSettings = new System.Windows.Forms.TabPage();
            this.printSettingsCustomControl = new OpenChords.Forms.Custom_Controls.SettingsPanel();
            this.tabPageTabletSettings = new System.Windows.Forms.TabPage();
            this.tabletSettingsCustomControl = new OpenChords.Forms.Custom_Controls.SettingsPanel();
            this.tabPageFileSettings.SuspendLayout();
            this.tabPageDisplaySettings.SuspendLayout();
            this.tabPager.SuspendLayout();
            this.tabPagePrintSettings.SuspendLayout();
            this.tabPageTabletSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageFileSettings
            // 
            this.tabPageFileSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.tabPageFileSettings.Controls.Add(this.fileSettingsCustomControl);
            this.tabPageFileSettings.Location = new System.Drawing.Point(4, 25);
            this.tabPageFileSettings.Name = "tabPageFileSettings";
            this.tabPageFileSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFileSettings.Size = new System.Drawing.Size(784, 408);
            this.tabPageFileSettings.TabIndex = 2;
            this.tabPageFileSettings.Text = "General Settings";
            // 
            // fileSettingsCustomControl
            // 
            this.fileSettingsCustomControl.AutoSize = true;
            this.fileSettingsCustomControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fileSettingsCustomControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.fileSettingsCustomControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileSettingsCustomControl.Location = new System.Drawing.Point(3, 3);
            this.fileSettingsCustomControl.Name = "fileSettingsCustomControl";
            this.fileSettingsCustomControl.Size = new System.Drawing.Size(778, 402);
            this.fileSettingsCustomControl.TabIndex = 0;
            // 
            // tabPageDisplaySettings
            // 
            this.tabPageDisplaySettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.tabPageDisplaySettings.Controls.Add(this.displaySettingsCustomControl);
            this.tabPageDisplaySettings.Location = new System.Drawing.Point(4, 25);
            this.tabPageDisplaySettings.Name = "tabPageDisplaySettings";
            this.tabPageDisplaySettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisplaySettings.Size = new System.Drawing.Size(784, 439);
            this.tabPageDisplaySettings.TabIndex = 0;
            this.tabPageDisplaySettings.Text = "Display Settings";
            // 
            // displaySettingsCustomControl
            // 
            this.displaySettingsCustomControl.AutoSize = true;
            this.displaySettingsCustomControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.displaySettingsCustomControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.displaySettingsCustomControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displaySettingsCustomControl.Location = new System.Drawing.Point(3, 3);
            this.displaySettingsCustomControl.Name = "displaySettingsCustomControl";
            this.displaySettingsCustomControl.Size = new System.Drawing.Size(778, 433);
            this.displaySettingsCustomControl.TabIndex = 0;
            // 
            // tabPager
            // 
            this.tabPager.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabPager.Controls.Add(this.tabPageFileSettings);
            this.tabPager.Controls.Add(this.tabPageDisplaySettings);
            this.tabPager.Controls.Add(this.tabPagePrintSettings);
            this.tabPager.Controls.Add(this.tabPageTabletSettings);
            this.tabPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPager.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabPager.Location = new System.Drawing.Point(0, 0);
            this.tabPager.Name = "tabPager";
            this.tabPager.SelectedIndex = 0;
            this.tabPager.Size = new System.Drawing.Size(792, 468);
            this.tabPager.TabIndex = 0;
            // 
            // tabPagePrintSettings
            // 
            this.tabPagePrintSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.tabPagePrintSettings.Controls.Add(this.printSettingsCustomControl);
            this.tabPagePrintSettings.Location = new System.Drawing.Point(4, 25);
            this.tabPagePrintSettings.Name = "tabPagePrintSettings";
            this.tabPagePrintSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePrintSettings.Size = new System.Drawing.Size(784, 408);
            this.tabPagePrintSettings.TabIndex = 1;
            this.tabPagePrintSettings.Text = "Print Settings";
            // 
            // printSettingsCustomControl
            // 
            this.printSettingsCustomControl.AutoSize = true;
            this.printSettingsCustomControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.printSettingsCustomControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.printSettingsCustomControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printSettingsCustomControl.Location = new System.Drawing.Point(3, 3);
            this.printSettingsCustomControl.Name = "printSettingsCustomControl";
            this.printSettingsCustomControl.Size = new System.Drawing.Size(778, 402);
            this.printSettingsCustomControl.TabIndex = 0;
            // 
            // tabPageTabletSettings
            // 
            this.tabPageTabletSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.tabPageTabletSettings.Controls.Add(this.tabletSettingsCustomControl);
            this.tabPageTabletSettings.Location = new System.Drawing.Point(4, 25);
            this.tabPageTabletSettings.Name = "tabPageTabletSettings";
            this.tabPageTabletSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTabletSettings.Size = new System.Drawing.Size(784, 408);
            this.tabPageTabletSettings.TabIndex = 3;
            this.tabPageTabletSettings.Text = "Tablet Settings";
            // 
            // tabletSettingsCustomControl
            // 
            this.tabletSettingsCustomControl.AutoSize = true;
            this.tabletSettingsCustomControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tabletSettingsCustomControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.tabletSettingsCustomControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabletSettingsCustomControl.Location = new System.Drawing.Point(3, 3);
            this.tabletSettingsCustomControl.Name = "tabletSettingsCustomControl";
            this.tabletSettingsCustomControl.Size = new System.Drawing.Size(778, 402);
            this.tabletSettingsCustomControl.TabIndex = 0;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(792, 468);
            this.Controls.Add(this.tabPager);
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tabPageFileSettings.ResumeLayout(false);
            this.tabPageFileSettings.PerformLayout();
            this.tabPageDisplaySettings.ResumeLayout(false);
            this.tabPageDisplaySettings.PerformLayout();
            this.tabPager.ResumeLayout(false);
            this.tabPagePrintSettings.ResumeLayout(false);
            this.tabPagePrintSettings.PerformLayout();
            this.tabPageTabletSettings.ResumeLayout(false);
            this.tabPageTabletSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageFileSettings;
        private System.Windows.Forms.TabPage tabPageDisplaySettings;
        private System.Windows.Forms.TabControl tabPager;
        private System.Windows.Forms.TabPage tabPagePrintSettings;
        private System.Windows.Forms.TabPage tabPageTabletSettings;
        private Custom_Controls.FileSettingsPanel fileSettingsCustomControl;
        private Custom_Controls.SettingsPanel displaySettingsCustomControl;
        private Custom_Controls.SettingsPanel printSettingsCustomControl;
        private Custom_Controls.SettingsPanel tabletSettingsCustomControl;

    }
}