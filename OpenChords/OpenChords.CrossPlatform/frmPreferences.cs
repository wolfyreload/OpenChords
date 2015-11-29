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
        private DisplayAndPrintPreferences displayPreferences;
        private DisplayAndPrintPreferences printPreferences;
        private DisplayAndPrintPreferences tabletPreferences;
        private ShortcutSettingsPreferences shortcutPreferences;


        public frmPreferences(Song songToPreview)
        {
            this.Title = "Preferences";
            this.Icon = Graphics.Icon;
            this.WindowState = Eto.Forms.WindowState.Maximized;

            displayPreferences = new Preferences.DisplayAndPrintPreferences(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings), songToPreview);
            printPreferences = new Preferences.DisplayAndPrintPreferences(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.PrintSettings), songToPreview);
            tabletPreferences = new Preferences.DisplayAndPrintPreferences(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings), songToPreview);
            shortcutPreferences = new ShortcutSettingsPreferences(Entities.ShortcutSettings.LoadSettings());

            Content = new TabControl()
            {
                Pages = 
                 {
                     new TabPage() { Text = "General Settings", Content = generalSettingsPreferences },
                     new TabPage() { Text = "Display Settings", Content = displayPreferences },
                     new TabPage() { Text = "Print Settings", Content = printPreferences },
                     new TabPage() { Text = "Tablet Settings", Content = tabletPreferences },
                     new TabPage() { Text = "Shortcut Settings", Content = shortcutPreferences }
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
            shortcutPreferences.SavePreferences();
        }
    }
}
