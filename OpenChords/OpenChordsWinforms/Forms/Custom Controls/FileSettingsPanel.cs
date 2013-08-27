using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenChords.Forms.Custom_Controls
{
    public partial class FileSettingsPanel : UserControl
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       private Entities.FileAndFolderSettings settings;

        
        public FileSettingsPanel()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            settings = Entities.FileAndFolderSettings.loadSettings();
            
        }

        public void loadSettings()
        {
            updateGui();
        }


        void updateGui()
        {
            //file settings
            
            txtApplicationDataFolder.Text = settings.ApplicationDataFolder;
            txtOpensongExecutable.Text = settings.OpenSongExecutable;
            txtOpenSongSetsAndSongs.Text = settings.OpenSongSetsAndSongs;
            chkUseWelcomeSlide.Checked = settings.OpenSongUseWelcomeSlide;
            txtWelcomeSlideName.Text = settings.OpenSongWelcomeSlide;
            txtFileSync.Text = settings.FileSync;
            chkUpdates.Checked = settings.CheckForUpdates;
            chkPortableMode.Checked = settings.PortableMode;

            if (!chkPortableMode.Checked)
            {
                txtSettingsFolder.ReadOnly = true;
                imgSettingsFolder.Enabled = false;
                txtSettingsFolder.Text = Application.UserAppDataPath + "\\";
            }
            else
            {
                txtSettingsFolder.Text = settings.SettingsFolder;
            }
        }

        private int getInt(string input)
        {
            int output = 0;
            int.TryParse(input, out output);
            return output;
        }

        private float getFloat(string input)
        {
            float output = 0;
            float.TryParse(input, out output);
            return output;
        }

        void updateSettingsObject()
        {
            //file settings
            
            settings.ApplicationDataFolder = txtApplicationDataFolder.Text;
            settings.OpenSongExecutable = txtOpensongExecutable.Text;
            settings.OpenSongSetsAndSongs = txtOpenSongSetsAndSongs.Text;
            settings.OpenSongUseWelcomeSlide = chkUseWelcomeSlide.Checked;
            settings.OpenSongWelcomeSlide = txtWelcomeSlideName.Text;
            settings.FileSync = txtFileSync.Text;
            settings.CheckForUpdates = chkUpdates.Checked;
            settings.PortableMode = chkPortableMode.Checked;
            if (settings.PortableMode)
                settings.SettingsFolder = txtSettingsFolder.Text;
            else
                settings.SettingsFolder = "";
        }

        public void saveSettings()
        {
            updateSettingsObject();
            settings.saveSettings();
        }

        private void cmdCheck_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            Functions.Updater.checkForNewVersion(true);
        }

        private string getInitialFolder(string path)
        {
            string newpath = "";
            if (!String.IsNullOrEmpty(path) && path.Length > 0 && path[0] == '.')
            {
                newpath = Path.GetFullPath(path);
                newpath = newpath.Replace('\\', System.IO.Path.DirectorySeparatorChar);
                newpath = newpath.Replace('/', System.IO.Path.DirectorySeparatorChar);
            }
            else
            {
                newpath = path;
            }

            return newpath;
        }

        private void showFolderDialog(TextBox textbox)
        {
            openFileDialog1.Title = "Please select a folder";
            openFileDialog1.FileName = "folder.name";

            openFileDialog1.InitialDirectory = getInitialFolder(textbox.Text);
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
                textbox.Text = openFileDialog1.FileName;
        }

        private void imgSettingsFolder_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            showFolderDialog(txtSettingsFolder);
        }

        private void imgApplicationDataFolder_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            showFolderDialog(txtApplicationDataFolder);
        }

        private void imgOpenSongExecutableFolder_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            showFolderDialog(txtOpensongExecutable);
        }

        private void imgOpenSongSetsFolder_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            showFolderDialog(txtOpenSongSetsAndSongs);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (!String.IsNullOrEmpty(openFileDialog1.FileName))
                openFileDialog1.FileName = openFileDialog1.FileName.Replace("\\folder.name", "");

        }

        private void showExecutableDialog(TextBox textbox, string fileName)
        {
            openFileDialog1.Title = "Please select the location for " + fileName;
            openFileDialog1.FileName = fileName;

            openFileDialog1.InitialDirectory = getInitialFolder(textbox.Text);
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
                textbox.Text = openFileDialog1.FileName;
        }

        private void imgOpenSongExecutable_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            showExecutableDialog(txtOpensongExecutable, "OpenSong.exe");
        }

       

        private void imgFileSyncExecutable_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            showExecutableDialog(txtFileSync, "FileSyncTool.exe");
        }

        private void chkPortableMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPortableMode.Checked)
            {

                txtSettingsFolder.ReadOnly = false;
                imgSettingsFolder.Enabled = true;
                var defaultFileSettings = new Entities.FileAndFolderSettings();
                txtSettingsFolder.Text = defaultFileSettings.SettingsFolder;
            }
            else
            {
                txtSettingsFolder.ReadOnly = true;
                imgSettingsFolder.Enabled = false;
                txtSettingsFolder.Text = Application.UserAppDataPath + "\\";
            }
        }

    }
}
