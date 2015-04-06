using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform.Preferences
{
    public class DisplayAndPrintPreferences : Panel
    {
        private Entities.DisplayAndPrintSettings displayAndPrintSettings;

        private FontAndColorPanel fontAndColorPanel;
        private CheckBox chkShowNotes = new CheckBox();
        private CheckBox chkShowLyrics = new CheckBox();
        private CheckBox chkShowChords = new CheckBox();
        private ComboBox cmbParagraphSpacing = new ComboBox() { AutoComplete = true };
        private ComboBox cmbContentLineSpacing = new ComboBox() { AutoComplete = true };
        private ComboBox cmbNoteWidth = new ComboBox() { AutoComplete = true };
        private Button cmdResetPreference = new Button() { Text = "Reset settings to defaults" };


        public DisplayAndPrintPreferences(Entities.DisplayAndPrintSettings displayAndPrintSettings)
        {
            //populate comboboxes
            for (int i = 0; i <= 200; i++)
            {
                cmbContentLineSpacing.Items.Add(i.ToString());
                cmbContentLineSpacing.Items.Add(i.ToString());
                cmbNoteWidth.Items.Add(i.ToString());
            }

            cmdResetPreference.Visible = false;

            //populate values
            this.displayAndPrintSettings = displayAndPrintSettings;
            chkShowChords.Checked = displayAndPrintSettings.ShowChords;
            chkShowLyrics.Checked = displayAndPrintSettings.ShowLyrics;
            chkShowNotes.Checked = displayAndPrintSettings.ShowNotes;
            cmbParagraphSpacing.Text = displayAndPrintSettings.paragraphSpacing.ToString();
            cmbContentLineSpacing.Text = displayAndPrintSettings.contentLineSpacing.ToString();
            cmbNoteWidth.Text = displayAndPrintSettings.NoteWidth.ToString();

            fontAndColorPanel = new FontAndColorPanel(displayAndPrintSettings);

            Content = new TableLayout()
            {
                
                Rows =
                {
                    new GroupBox() 
                    { 
                        Text = "Visibility",
                        Content = new TableLayout()
                        {
                            Rows = 
                            {
                                new TableRow() { Cells = { new Label() { Text = "Show Chords" }, chkShowChords }},
                                new TableRow() { Cells = { new Label() { Text = "Show Lyrics" }, chkShowLyrics }},
                                new TableRow() { Cells = { new Label() { Text = "Show Notes" }, chkShowNotes }},
                                null,
                            }
                        }
                    },
                    new GroupBox()
                    {
                        Text = "Fonts And Colors",
                        Content = fontAndColorPanel
                    },
                    new Panel()
                    {
                        Content = cmdResetPreference
                    },
                    null
                }
            };
        }

        public void SavePreferences()
        {
            fontAndColorPanel.UpdateColorAndFontPreferences();

            displayAndPrintSettings.ShowChords = chkShowChords.Checked ?? false;
            displayAndPrintSettings.ShowLyrics = chkShowLyrics.Checked ?? false;
            displayAndPrintSettings.ShowNotes = chkShowNotes.Checked ?? false;
            displayAndPrintSettings.paragraphSpacing = int.Parse(cmbParagraphSpacing.Text);
            displayAndPrintSettings.NoteWidth = int.Parse(cmbNoteWidth.Text);
            displayAndPrintSettings.contentLineSpacing = int.Parse(cmbContentLineSpacing.Text);

            displayAndPrintSettings.saveSettings();
        }
    }
}
