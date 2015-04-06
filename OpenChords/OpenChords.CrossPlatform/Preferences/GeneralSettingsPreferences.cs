using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChords.CrossPlatform.Preferences
{
    class GeneralSettingsPreferences : Panel
    {
        private Entities.FileAndFolderSettings fileAndFolderSettings;
        private CheckBox chkPortableMode = new CheckBox();
        private TextBox txtApplicationDataFolder = new TextBox();
        private TextBox txtOpenSongExecutablePath = new TextBox();
        private TextBox txtOpenSongDataPath = new TextBox();
        private Button cmdApplicationDataFolder = new Button() { Text = "Open", Style = "OpenFolder" };
        private Button cmdOpenSongExecutablePath = new Button() { Text = "Open", Style = "OpenFolder" };
        private Button cmdOpenSongSongsAndSetsPath = new Button() { Text = "Open", Style = "OpenFolder" };
        private bool OptionsChanged;

        public GeneralSettingsPreferences(Entities.FileAndFolderSettings fileAndFolderSettings)
        {
            this.fileAndFolderSettings = fileAndFolderSettings;

            Content = new TableLayout()
            {
                Rows =
                {
                    new TableRow(new Label() { Text = "Portable Mode" }, chkPortableMode),
                    new TableRow(new Label() { Text = "OpenChords Data Folder" }, txtApplicationDataFolder ),
                    new TableRow(new Label() { Text = "OpenSong Executable" }, txtOpenSongExecutablePath ),
                    new TableRow(new Label() { Text = "OpenSong Data Folder" }, txtOpenSongDataPath ),
                    null
                }
            };

            chkPortableMode.Checked = fileAndFolderSettings.PortableMode;
            txtApplicationDataFolder.Text = fileAndFolderSettings.ApplicationDataFolder;
            txtOpenSongExecutablePath.Text = fileAndFolderSettings.OpenSongExecutable;
            txtOpenSongDataPath.Text = fileAndFolderSettings.OpenSongSetsAndSongs;

            //events
            chkPortableMode.CheckedChanged += chkPortableMode_CheckedChanged;
            txtApplicationDataFolder.TextChanged += textChanged;
            txtOpenSongDataPath.TextChanged += textChanged;
            txtOpenSongExecutablePath.TextChanged += textChanged;
        }

        private void textChanged(object sender, EventArgs e)
        {
            OptionsChanged = true;
        }

        void chkPortableMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPortableMode.Checked ?? false)
                txtApplicationDataFolder.Text = "Data";
            else
                txtApplicationDataFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.Personal).Replace("\\", "/") + "/OpenChords/";

            OptionsChanged = true;
        }

        internal void SavePreferences()
        {
            fileAndFolderSettings.PortableMode = chkPortableMode.Checked ?? false;
            fileAndFolderSettings.ApplicationDataFolder = txtApplicationDataFolder.Text;
            fileAndFolderSettings.OpenSongExecutable = txtOpenSongExecutablePath.Text;
            fileAndFolderSettings.OpenSongSetsAndSongs = txtOpenSongDataPath.Text;

            fileAndFolderSettings.saveSettings();
            if (OptionsChanged)
                MessageBox.Show("Changes to general settings will only take effect after restarting OpenChords", "", MessageBoxType.Information);

        }
    }
}
