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
                        new TableRow(new ShortcutKeyPicker("Present Song", shortcutSettings.PresentSong )),
                        new TableRow(new ShortcutKeyPicker("Present Set", shortcutSettings.PresentSet )),
                        new TableRow(new ShortcutKeyPicker("New Song", shortcutSettings.NewSong )),
                        new TableRow(new ShortcutKeyPicker("Save Song", shortcutSettings.SaveSong )),
                        new TableRow(new ShortcutKeyPicker("Delete Song", shortcutSettings.DeleteSong )),
                        new TableRow(new ShortcutKeyPicker("Auto Format Song", shortcutSettings.AutoFormatSong )),
                        new TableRow(new ShortcutKeyPicker("Print Song Html", shortcutSettings.PrintSongHtml )),
                        new TableRow(new ShortcutKeyPicker("Export Set To OpenSong", shortcutSettings.ExportSetToOpenSong )),
                        new TableRow(new ShortcutKeyPicker("Add Song To Set", shortcutSettings.AddSongToSet )),
                        new TableRow(new ShortcutKeyPicker("Select Random Song", shortcutSettings.SelectRandomSong )),
                        new TableRow(new ShortcutKeyPicker("Refresh Song List", shortcutSettings.RefreshSongList )),
                        new TableRow(new ShortcutKeyPicker("Refresh Set List", shortcutSettings.RefreshSetList )),
                        new TableRow(new ShortcutKeyPicker("Move Song Up In Set", shortcutSettings.MoveSongUpInSet )),
                        new TableRow(new ShortcutKeyPicker("Move Song Down In Set", shortcutSettings.MoveSongDownInSet )),
                        new TableRow(new ShortcutKeyPicker("Show Help", shortcutSettings.ShowHelp )),
                        new TableRow(new ShortcutKeyPicker("Quit OpenChords", shortcutSettings.QuitOpenChords )),
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
                        new TableRow(new ShortcutKeyPicker("Refresh Presentation", shortcutSettings.RefreshPresentation )),
                        new TableRow(new ShortcutKeyPicker("Toggle Chords", shortcutSettings.ToggleChords )),
                        new TableRow(new ShortcutKeyPicker("Toggle Lyrics", shortcutSettings.ToggleLyrics )),
                        new TableRow(new ShortcutKeyPicker("Toggle Notes", shortcutSettings.ToggleNotes )),
                        new TableRow(new ShortcutKeyPicker("Toggle Metronome", shortcutSettings.ToggleMetronome )),
                        new TableRow(new ShortcutKeyPicker("Capo Down", shortcutSettings.CapoDown )),
                        new TableRow(new ShortcutKeyPicker("Capo Up", shortcutSettings.CapoUp )),
                        new TableRow(new ShortcutKeyPicker("Transpose Down", shortcutSettings.TransposeDown )),
                        new TableRow(new ShortcutKeyPicker("Transpose Up", shortcutSettings.TransposeUp )),
                        new TableRow(new ShortcutKeyPicker("Decrease Font Size", shortcutSettings.DecreaseFontSize )),
                        new TableRow(new ShortcutKeyPicker("Increase Font Size", shortcutSettings.IncreaseFontSize )),
                        new TableRow(new ShortcutKeyPicker("Go To Previous Song", shortcutSettings.GoToPreviousSong )),
                        new TableRow(new ShortcutKeyPicker("Go To Next Song", shortcutSettings.GoToNextSong )),
                        new TableRow(new ShortcutKeyPicker("Exit Presentation", shortcutSettings.ExitPresentation )),
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
