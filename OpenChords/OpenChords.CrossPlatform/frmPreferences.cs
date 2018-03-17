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
        private Song _songToPreview;

        public frmPreferences(Song songToPreview)
        {
            _songToPreview = songToPreview;

            this.Title = "Preferences";
            this.Icon = Graphics.Icon;
            this.Width = 1024;
            this.Height = 768;
            this.WindowState = WindowState.Maximized;

            var tabControl = new TabControl()
            {
                Pages =
                 {
                     new TabPage() { Text = "General Settings" },
                     new TabPage() { Text = "Display Settings" },
                     new TabPage() { Text = "Print Settings" },
                     new TabPage() { Text = "Tablet Settings" },
                     new TabPage() { Text = "Shortcut Settings" },
                     new TabPage() { Text = "User Interface Settings" }

                 }
            };

            Content = tabControl;

            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            //TabControl_SelectedIndexChanged runs without initial invocation add exception for windows system
            if (Eto.Platform.Instance.IsWpf)   
                loadSelectedTab(tabControl);

            this.Closing += frmPreferences_Closing;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSelectedTab(sender as TabControl);
        }

        private void loadSelectedTab(TabControl tabControl)
        {
            var selectedTabPage = tabControl.SelectedPage;
            var selectedTabControl = tabControl.SelectedPage.Content;

            //if the control is already loaded we have nothing that we need to do
            if (selectedTabControl != null) return;

            //load the tab
            switch (selectedTabPage.Text)
            {
                case "General Settings":
                    selectedTabPage.Content = new Preferences.GeneralSettingsPreferences(FileAndFolderSettings.loadSettings());
                    break;
                case "Display Settings":
                    selectedTabPage.Content = new Preferences.DisplayAndPrintPreferences(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings), _songToPreview);
                    break;
                case "Print Settings":
                    selectedTabPage.Content = new Preferences.DisplayAndPrintPreferences(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.PrintSettings), _songToPreview);
                    break;
                case "Tablet Settings":
                    selectedTabPage.Content = new Preferences.DisplayAndPrintPreferences(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings), _songToPreview);
                    break;
                case "Shortcut Settings":
                    selectedTabPage.Content = new ShortcutSettingsPreferences(Entities.ShortcutSettings.LoadSettings());
                    break;
                case "User Interface Settings":
                    selectedTabPage.Content = new UserInterfacePreferences();
                    break;
            }
        }

        void frmPreferences_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var tabControl = this.Content as TabControl;
            foreach (var control in tabControl.Pages)
            {
                var content = control.Content;

                if (content is DisplayAndPrintPreferences)
                    (content as DisplayAndPrintPreferences).SavePreferences();
                else if (content is GeneralSettingsPreferences)
                    (content as GeneralSettingsPreferences).SavePreferences();
                else if (content is ShortcutSettingsPreferences)
                    (content as ShortcutSettingsPreferences).SavePreferences();
                else if (content is UserInterfacePreferences)
                    (content as UserInterfacePreferences).SavePreferences();
            }
        }
    }
}
