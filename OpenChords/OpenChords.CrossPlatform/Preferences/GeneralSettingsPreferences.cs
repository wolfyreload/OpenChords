using Eto.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using static OpenChords.Entities.FileAndFolderSettings;

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
        private TextArea txtInterfaceHelp = new TextArea() { Height = 100 };
        protected RadioButtonList radioListSharpsOrFlats = new RadioButtonList();
        private DropDown ddlKeyNotationLanguage = new DropDown();
        private bool OptionsChanged;

        public GeneralSettingsPreferences(Entities.FileAndFolderSettings fileAndFolderSettings)
        {
            //add radio button options
            radioListSharpsOrFlats.Items.Add("Flats");
            radioListSharpsOrFlats.Items.Add("Sharps");

            ddlKeyNotationLanguage.Items.Add("English");
            ddlKeyNotationLanguage.Items.Add("German");

            this.fileAndFolderSettings = fileAndFolderSettings;

            Content = new TableLayout()
            {
                Style = "padded-table",
                Rows =
                {
                    new TableRow(new Label() { Text = "Portable Mode" }, chkPortableMode),
                    new TableRow(new Label() { Text = "OpenChords Data Folder" }, txtApplicationDataFolder, cmdApplicationDataFolder, new Label() ),
                    new TableRow(new Label() { Text = "OpenSong Executable" }, txtOpenSongExecutablePath, cmdOpenSongExecutablePath, new Label() ),
                    new TableRow(new Label() { Text = "OpenSong Data Folder" }, txtOpenSongDataPath, cmdOpenSongSongsAndSetsPath, new Label() ),
                    new TableRow(new Label() { Text = "Http Server Enabled" }, chkHttpServerEnabled ),
                    new TableRow(new Label() { Text = "Http Server Port" }, txtHttpServerPort ),
                    new TableRow(new Label() { Text = "Http Examples" }, txtInterfaceHelp),
                    new TableRow(new Label() { Text = "Sharp/Flat Key Preference" }, radioListSharpsOrFlats),
                    new TableRow(new Label() { Text = "Key Notation Language" }, ddlKeyNotationLanguage),
                    null
                }
            };

            chkPortableMode.Checked = fileAndFolderSettings.PortableMode;
            txtApplicationDataFolder.Text = fileAndFolderSettings.ApplicationDataFolder;
            txtOpenSongExecutablePath.Text = fileAndFolderSettings.OpenSongExecutable;
            txtOpenSongDataPath.Text = fileAndFolderSettings.OpenSongSetsAndSongs;
            chkHttpServerEnabled.Checked = fileAndFolderSettings.HttpServerEnabled;
            txtHttpServerPort.Text = fileAndFolderSettings.HttpServerPort.ToString();
            radioListSharpsOrFlats.SelectedIndex = fileAndFolderSettings.PreferFlats ? 0 : 1;
            setHelpOnInterfaces(chkHttpServerEnabled.Checked ?? false);
            ddlKeyNotationLanguage.SelectedIndex = (fileAndFolderSettings.KeyNotationLanguage == KeyNotationLanguageType.English) ? 0 : 1;

            //events
            chkPortableMode.CheckedChanged += chkPortableMode_CheckedChanged;
            txtApplicationDataFolder.TextChanged += textChanged;
            txtOpenSongDataPath.TextChanged += textChanged;
            txtOpenSongExecutablePath.TextChanged += textChanged;
            chkHttpServerEnabled.CheckedChanged += checkedChanged;
            chkHttpServerEnabled.CheckedChanged += ChkHttpServerEnabled_CheckedChanged;
            txtHttpServerPort.TextChanged += textChanged;
            txtHttpServerPort.TextChanged += TxtHttpServerPort_TextChanged;
            txtHttpServerPort.LostFocus += TxtHttpServerPort_LostFocus;
            cmdApplicationDataFolder.Click += CmdApplicationDataFolder_Click;
            cmdOpenSongExecutablePath.Click += CmdOpenSongExecutablePath_Click;
            cmdOpenSongSongsAndSetsPath.Click += CmdOpenSongSongsAndSetsPath_Click;
            radioListSharpsOrFlats.SelectedIndexChanged += textChanged;
            ddlKeyNotationLanguage.SelectedIndexChanged += textChanged;
        }

        private void TxtHttpServerPort_LostFocus(object sender, EventArgs e)
        {
            bool portSetCorrectly = false;
            int port = 0;
            //check if port is valid
            if (portSetCorrectly = int.TryParse(txtHttpServerPort.Text, out port))
            {
                if (port >= 1024 && port <= 65535)
                {
                    portSetCorrectly = true;
                }
                else
                {
                    Helpers.PopupMessages.ShowErrorMessage("Server port must be between 1024 and 65535");
                    portSetCorrectly = false;
                }
            }
            //reset the port
            if (portSetCorrectly)
                txtHttpServerPort.Text = port.ToString();
            else
                txtHttpServerPort.Text = "8083";

        }

        private void TxtHttpServerPort_TextChanged(object sender, EventArgs e)
        {
            setHelpOnInterfaces(chkHttpServerEnabled.Checked ?? true);
        }

        private void ChkHttpServerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            setHelpOnInterfaces(chkHttpServerEnabled.Checked ?? true);
        }

        private void setHelpOnInterfaces(bool show)
        {
            if (show)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("You can access OpenChords externally using:");
                // Get host name
                String strHostName = Dns.GetHostName();

                // Find host by name
                IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

                // Enumerate IP addresses
                foreach (IPAddress ipaddress in iphostentry.AddressList)
                {
                    if (!ipaddress.ToString().Contains(":")) //only show ip version 4 addresses
                        sb.AppendLine(string.Format("http://{0}:{1}/song", ipaddress, txtHttpServerPort.Text));
                }
                
                txtInterfaceHelp.ReadOnly = false;
                txtInterfaceHelp.Text = sb.ToString().Trim();
                txtInterfaceHelp.ReadOnly = true;
            }
            else
            {
                txtInterfaceHelp.ReadOnly = false;
                txtInterfaceHelp.Text = "You cannot access OpenChords externally since the http server is disabled";
                txtInterfaceHelp.ReadOnly = true;
            }
        }
        
        private void CmdApplicationDataFolder_Click(object sender, EventArgs e)
        {
            var path = getFolderPath();
            if (path != null)
                txtApplicationDataFolder.Text = path;
        }


        private void CmdOpenSongExecutablePath_Click(object sender, EventArgs e)
        {
            FileDialogFilter filter = new FileDialogFilter("OpenSong Executable", "OpenSong.exe", "OpenSong");
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filters.Add(filter);
                if (openFileDialog.ShowDialog(this) == DialogResult.Ok)
                    txtOpenSongExecutablePath.Text = openFileDialog.FileName;
            }
        }

        private void CmdOpenSongSongsAndSetsPath_Click(object sender, EventArgs e)
        {
            var path = getFolderPath();
            if (path != null)
                txtOpenSongDataPath.Text = path;
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
            fileAndFolderSettings.PreferFlats = radioListSharpsOrFlats.SelectedIndex == 0;
            fileAndFolderSettings.KeyNotationLanguage = (ddlKeyNotationLanguage.SelectedIndex == 0) ? KeyNotationLanguageType.English : KeyNotationLanguageType.German;

            fileAndFolderSettings.saveSettings();
        }
    }
}
