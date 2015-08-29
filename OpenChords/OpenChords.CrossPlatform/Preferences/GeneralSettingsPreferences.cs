using Eto.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenChords.CrossPlatform.Preferences
{
    class GeneralSettingsPreferences : Panel
    {
        private Entities.FileAndFolderSettings fileAndFolderSettings;
        private CheckBox chkPortableMode = new CheckBox();
        private TextBox txtApplicationDataFolder = new TextBox() { Width = 550 };
        private TextBox txtOpenSongExecutablePath = new TextBox();
        private TextBox txtOpenSongDataPath = new TextBox();
        private Button cmdApplicationDataFolder = new Button() { Text = "", Style = "OpenFolder", Image = Graphics.ImageExplore };
        private Button cmdOpenSongExecutablePath = new Button() { Text = "", Style = "OpenFolder", Image = Graphics.ImageExplore };
        private Button cmdOpenSongSongsAndSetsPath = new Button() { Text = "", Style = "OpenFolder", Image = Graphics.ImageExplore };
        private CheckBox chkHttpServerEnabled = new CheckBox();
        private TextBox txtHttpServerPort = new TextBox() { Width = 50 };
        private bool OptionsChanged;

        public GeneralSettingsPreferences(Entities.FileAndFolderSettings fileAndFolderSettings)
        {
            this.fileAndFolderSettings = fileAndFolderSettings;

            Content = new TableLayout()
            {
                Rows =
                {
                    new TableRow(new Label() { Text = "Portable Mode" }, chkPortableMode),
                    new TableRow(new Label() { Text = "OpenChords Data Folder" }, txtApplicationDataFolder, cmdApplicationDataFolder, new Label() ),
                    new TableRow(new Label() { Text = "OpenSong Executable" }, txtOpenSongExecutablePath, cmdOpenSongExecutablePath, new Label() ),
                    new TableRow(new Label() { Text = "OpenSong Data Folder" }, txtOpenSongDataPath, cmdOpenSongSongsAndSetsPath, new Label() ),
                    new TableRow(new Label() { Text = "Http Server Enabled" }, chkHttpServerEnabled ),
                    new TableRow(new Label() { Text = "Http Server Port" }, txtHttpServerPort ),
                    null
                }
            };

            chkPortableMode.Checked = fileAndFolderSettings.PortableMode;
            txtApplicationDataFolder.Text = fileAndFolderSettings.ApplicationDataFolder;
            txtOpenSongExecutablePath.Text = fileAndFolderSettings.OpenSongExecutable;
            txtOpenSongDataPath.Text = fileAndFolderSettings.OpenSongSetsAndSongs;
            chkHttpServerEnabled.Checked = fileAndFolderSettings.HttpServerEnabled;
            txtHttpServerPort.Text = fileAndFolderSettings.HttpServerPort.ToString();

            //events
            chkPortableMode.CheckedChanged += chkPortableMode_CheckedChanged;
            txtApplicationDataFolder.TextChanged += textChanged;
            txtOpenSongDataPath.TextChanged += textChanged;
            txtOpenSongExecutablePath.TextChanged += textChanged;
            chkHttpServerEnabled.CheckedChanged += checkedChanged;
            txtHttpServerPort.TextChanged += textChanged;
            cmdApplicationDataFolder.Click += CmdApplicationDataFolder_Click;
            cmdOpenSongExecutablePath.Click += CmdOpenSongExecutablePath_Click;
            cmdOpenSongSongsAndSetsPath.Click += CmdOpenSongSongsAndSetsPath_Click;
        }

        private void CmdApplicationDataFolder_Click(object sender, EventArgs e)
        {
            txtApplicationDataFolder.Text = getFolderPath() ?? "";
        }


        private void CmdOpenSongExecutablePath_Click(object sender, EventArgs e)
        {
            FileDialogFilter filter = new FileDialogFilter("OpenSong Executable", "OpenSong.exe", "OpenSong");
            List<FileDialogFilter> filters = new List<FileDialogFilter>() { filter };
            using (var openFileDialog = new OpenFileDialog() { Filters=filters })
            {
                if (openFileDialog.ShowDialog(this) == DialogResult.Ok)
                    txtOpenSongExecutablePath.Text = openFileDialog.FileName;
            }
        }

        private void CmdOpenSongSongsAndSetsPath_Click(object sender, EventArgs e)
        {
            txtOpenSongDataPath.Text = getFolderPath() ?? "";
        }

        private string getFolderPath()
        {
            using (var folderDialog = new SelectFolderDialog())
            {
                if (folderDialog.ShowDialog(this) == DialogResult.Ok)
                    return folderDialog.Directory;
                else
                    return null;
            }
        }

        void checkedChanged(object sender, EventArgs e)
        {
            OptionsChanged = true;
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
            fileAndFolderSettings.HttpServerEnabled = chkHttpServerEnabled.Checked ?? false;
            fileAndFolderSettings.HttpServerPort = int.Parse(txtHttpServerPort.Text);

            fileAndFolderSettings.saveSettings();
            if (OptionsChanged)
                Helpers.PopupMessages.ShowInformationMessage(this, "Changes to general settings will only take effect after restarting OpenChords");

        }
    }
}
