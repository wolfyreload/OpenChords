using Eto.Forms;
using OpenChords.CrossPlatform.Preferences;
using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform
{
    public class frmPreferences : Form
    {
        private GeneralSettingsPreferences generalSettingsPreferences = new Preferences.GeneralSettingsPreferences(FileAndFolderSettings.loadSettings());
        private DisplayAndPrintPreferences displayPreferences = new Preferences.DisplayAndPrintPreferences(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings));
        private DisplayAndPrintPreferences printPreferences = new Preferences.DisplayAndPrintPreferences(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.PrintSettings));
        private DisplayAndPrintPreferences tabletPreferences = new Preferences.DisplayAndPrintPreferences(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings));


        public frmPreferences()
        {
            this.Title = "OpenChords Preferences";
            this.Icon = Graphics.Icon;
            Width = 640;
            Height = 480;
            Content = new TabControl()
            {
                Pages = 
                 {
                     new TabPage() { Text = "General Settings", Content = generalSettingsPreferences },
                     new TabPage() { Text = "Display Settings", Content = displayPreferences },
                     new TabPage() { Text = "Print Settings", Content = printPreferences },
                     new TabPage() { Text = "Tablet Settings", Content = tabletPreferences }
                 }
            };

            this.Closing += frmPreferences_Closing;
        }

        void frmPreferences_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            generalSettingsPreferences.SavePreferences();
            displayPreferences.SavePreferences();
            printPreferences.SavePreferences();
            tabletPreferences.SavePreferences();
        }
    }
}
