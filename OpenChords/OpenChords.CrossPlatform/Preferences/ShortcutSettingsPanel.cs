using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenChords.Entities;

namespace OpenChords.CrossPlatform.Preferences
{
    class ShortcutSettingsPreferences : Panel
    {
        private ShortcutSettings shortcutSettings;
        protected Button cmdResetToDefaults = new Button() { Text = "Reset" };

        public ShortcutSettingsPreferences(ShortcutSettings shortcutSettings)
        {
            this.shortcutSettings = shortcutSettings;
            refreshPanel();

            cmdResetToDefaults.Click += CmdResetToDefaults_Click;
        }

        private void refreshPanel()
        {
            Content = null;
            var editorShortcutsGroupBox =
            new GroupBox()
            {
                Text = "Editor Shortcuts",
                Content = new TableLayout()
                {
                    Style = "padded-table",
                    Rows =
                    {
                        new ShortcutKeyPicker("Present Song", shortcutSettings.PresentSong ),
                        new ShortcutKeyPicker("Present Set", shortcutSettings.PresentSet ),
                        new ShortcutKeyPicker("New Song", shortcutSettings.NewSong ),
                        new ShortcutKeyPicker("Save Song", shortcutSettings.SaveSong ),
                        new ShortcutKeyPicker("Delete Song", shortcutSettings.DeleteSong ),
                        new ShortcutKeyPicker("Auto Format Song", shortcutSettings.AutoFormatSong ),
                        new ShortcutKeyPicker("Print Song Html", shortcutSettings.PrintSongHtml ),
                        new ShortcutKeyPicker("Export Set To OpenSong", shortcutSettings.ExportSetToOpenSong ),
                        new ShortcutKeyPicker("Add Song To Set", shortcutSettings.AddSongToSet ),
                        new ShortcutKeyPicker("Select Random Song", shortcutSettings.SelectRandomSong ),
                        new ShortcutKeyPicker("Refresh Song List", shortcutSettings.RefreshSongList ),
                        new ShortcutKeyPicker("Refresh Set List", shortcutSettings.RefreshSetList ),
                        new ShortcutKeyPicker("Move Song Up In Set", shortcutSettings.MoveSongUpInSet ),
                        new ShortcutKeyPicker("Move Song Down In Set", shortcutSettings.MoveSongDownInSet ),
                        new ShortcutKeyPicker("Show Help", shortcutSettings.ShowHelp ),
                        new ShortcutKeyPicker("Quit OpenChords", shortcutSettings.QuitOpenChords ),
                        null,
                    }
                }
            };

            var presentationSettingsGroupBox =
            new GroupBox()
            {
                Text = "Presentation Shortcuts",
                Content = new TableLayout()
                {
                    Style = "padded-table",
                    Rows =
                    {
                        new ShortcutKeyPicker("Refresh Presentation", shortcutSettings.RefreshPresentation ),
                        new ShortcutKeyPicker("Toggle Chords", shortcutSettings.ToggleChords ),
                        new ShortcutKeyPicker("Toggle Lyrics", shortcutSettings.ToggleLyrics ),
                        new ShortcutKeyPicker("Toggle Notes", shortcutSettings.ToggleNotes ),
                        new ShortcutKeyPicker("Toggle Metronome", shortcutSettings.ToggleMetronome ),
                        new ShortcutKeyPicker("Capo Down", shortcutSettings.CapoDown ),
                        new ShortcutKeyPicker("Capo Up", shortcutSettings.CapoUp ),
                        new ShortcutKeyPicker("Transpose Down", shortcutSettings.TransposeDown ),
                        new ShortcutKeyPicker("Transpose Up", shortcutSettings.TransposeUp ),
                        new ShortcutKeyPicker("Decrease Font Size", shortcutSettings.DecreaseFontSize ),
                        new ShortcutKeyPicker("Increase Font Size", shortcutSettings.IncreaseFontSize ),
                        new ShortcutKeyPicker("Go To Previous Song", shortcutSettings.GoToPreviousSong ),
                        new ShortcutKeyPicker("Go To Next Song", shortcutSettings.GoToNextSong ),
                        new ShortcutKeyPicker("Exit Presentation", shortcutSettings.ExitPresentation ),
                        null,
                    }
                }
            };

            var buttonsGroupbox =
            new GroupBox()
            {
                Text = "Restore Settings",
                Content = new TableLayout()
                {
                    Style = "padded-table",
                    Rows =
                    {
                        new TableRow(cmdResetToDefaults, null),
                        null,
                    }
                }
            };

            Content = new TableLayout()
            {
                Rows = {
                    new TableRow(editorShortcutsGroupBox, presentationSettingsGroupBox),
                    new TableRow(buttonsGroupbox),
                    null
                }
            };
        }

        private void CmdResetToDefaults_Click(object sender, EventArgs e)
        {
            shortcutSettings = ShortcutSettings.RefreshSettings();
            refreshPanel();
        }

        internal void SavePreferences()
        {
            shortcutSettings.SaveSettings();
        }
    }
}
