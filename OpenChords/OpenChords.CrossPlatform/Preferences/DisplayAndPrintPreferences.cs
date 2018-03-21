﻿using Eto.Forms;
using OpenChords.Entities;
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
        private SongMetadataTemplatesPanel songMetadataTemplatesPanel;
        private CheckBox chkShowNotes = new CheckBox();
        private CheckBox chkShowLyrics = new CheckBox();
        private CheckBox chkShowChords = new CheckBox();
        private CheckBox chkDisplayEachSongSectionOnce = new CheckBox();
        private ComboBox cmbSongOrientation = new ComboBox() { AutoComplete = false, ReadOnly = true };
        private Splitter splitter1;
        private WebView webPreview = new WebView();
        private Song _currentSong;
        private Button cmdBackupSettings = new Button() { Text = "Backup", ToolTip = "Backup settings to file" };
        private Button cmdRestoreSettings = new Button() { Text = "Restore", ToolTip = "Restore settings from file" };
        private Button cmdRevertSettings = new Button() { Text = "Revert", ToolTip = "Revert settings to previous saved state" };
        private Button cmdResetSettings = new Button() { Text = "Reset", ToolTip = "Reset settings to default" };

        FileDialogFilter FILTER = new FileDialogFilter("Openchords settings file", new string[] { ".oc" } );

        public DisplayAndPrintPreferences(Entities.DisplayAndPrintSettings displayAndPrintSettings, Song currentSong)
        {
            this._currentSong = currentSong;
            //populate values
            this.displayAndPrintSettings = displayAndPrintSettings;

            fontAndColorPanel = new FontAndColorPanel();

            songMetadataTemplatesPanel = new SongMetadataTemplatesPanel();

            cmbSongOrientation.Items.Add("Horizontal");
            cmbSongOrientation.Items.Add("Vertical");


            updateGuiFromSettingsObject(this.displayAndPrintSettings);

            Content = splitter1 = new Splitter()
            {
                Position = Helpers.FormHelper.getScreenXPercentageInPixels(30),
                Panel1 = new TableLayout()
                {

                    Rows =
                    {
                        new GroupBox()
                        {
                            Text = "Visibility",
                            Content = new TableLayout()
                            {
                                Style = "padded-table",
                                Rows =
                                {
                                    new TableRow() { Cells = { new Label() { Text = "Show Chords" }, chkShowChords }},
                                    new TableRow() { Cells = { new Label() { Text = "Show Lyrics" }, chkShowLyrics }},
                                    new TableRow() { Cells = { new Label() { Text = "Show Notes" }, chkShowNotes }},
                                    new TableRow() { Cells = { new Label() { Text = "Display Each Song Section Once" }, chkDisplayEachSongSectionOnce }},
                                    new TableRow() { Cells = { new Label() { Text = "Song Orientation" }, cmbSongOrientation, null }},
                                    null,
                                }
                            }
                        },
                        new GroupBox()
                        {
                            Text = "Fonts And Colors",
                            Content = fontAndColorPanel
                        },
                        new GroupBox()
                        {
                            Text = "Song Metadata Templates",
                            Content = songMetadataTemplatesPanel
                        },
                        new GroupBox()
                        {
                            Text = "Backup/Restore Settings",
                            Content = new TableLayout()
                            {
                                Style = "padded-table",
                                Rows =
                                {
                                    new TableRow() { Cells = { cmdBackupSettings, cmdRestoreSettings, cmdRevertSettings, cmdResetSettings, null }}
                                }
                            }
                        },
                        null
                    }
                },
                Panel2 = new GroupBox() { Text = "Preview", Content = webPreview }
            };

            updatePreview();
            chkShowChords.CheckedChanged += FieldChanged;
            chkShowLyrics.CheckedChanged += FieldChanged;
            chkShowNotes.CheckedChanged += FieldChanged;
            chkDisplayEachSongSectionOnce.CheckedChanged += FieldChanged;
            fontAndColorPanel.ItemChanged += FieldChanged;
            songMetadataTemplatesPanel.ItemChanged += FieldChanged;
            cmbSongOrientation.SelectedIndexChanged += FieldChanged;

            cmdBackupSettings.Click += cmdBackupSettings_Click;
            cmdRestoreSettings.Click += cmdRestoreSettings_Click;
            cmdRevertSettings.Click += cmdRevertSettings_Click;
            cmdResetSettings.Click += cmdResetSettings_Click;
        }

        void cmdBackupSettings_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new SaveFileDialog() { FileName = displayAndPrintSettings.settingsType.ToString() })
            {
                fileDialog.Filters.Add(FILTER);
                if (fileDialog.ShowDialog(this) == DialogResult.Ok)
                {
                    displayAndPrintSettings.saveSettings(fileDialog.FileName);
                }
            }

        }

        void cmdRestoreSettings_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog() { FileName = displayAndPrintSettings.settingsType.ToString() })
            {
                fileDialog.Filters.Add(FILTER);
                if (fileDialog.ShowDialog(this) == DialogResult.Ok)
                {
                    displayAndPrintSettings = DisplayAndPrintSettings.loadSettings(displayAndPrintSettings.settingsType, fileDialog.FileName);
                    updateGuiFromSettingsObject(displayAndPrintSettings);
                    updatePreview();
                }
            }
        }

        void cmdRevertSettings_Click(object sender, EventArgs e)
        {
            displayAndPrintSettings = DisplayAndPrintSettings.loadSettings(displayAndPrintSettings.settingsType);
            updateGuiFromSettingsObject(displayAndPrintSettings);
            updatePreview();
        }

        void cmdResetSettings_Click(object sender, EventArgs e)
        {
            displayAndPrintSettings = new DisplayAndPrintSettings(displayAndPrintSettings.settingsType);
            updateGuiFromSettingsObject(displayAndPrintSettings);
            updatePreview();
        }


        private void updatePreview()
        {
            string songHtml = _currentSong.getHtml(displayAndPrintSettings);
            webPreview.LoadHtml(songHtml);
        }

        private void FieldChanged(object sender, EventArgs e)
        {
            updateSettingsObjectFromGui();
            updatePreview();
            
        }

        public void updateGuiFromSettingsObject(DisplayAndPrintSettings displayAndPrintSettings)
        {
            chkShowChords.Checked = displayAndPrintSettings.ShowChords;
            chkShowLyrics.Checked = displayAndPrintSettings.ShowLyrics;
            chkShowNotes.Checked = displayAndPrintSettings.ShowNotes;
            chkDisplayEachSongSectionOnce.Checked = displayAndPrintSettings.DisplayEachSongSectionOnce;
            if (displayAndPrintSettings.SongOrientation == null || displayAndPrintSettings.SongOrientation == "Horizontal")
                cmbSongOrientation.SelectedIndex = 0;
            else
                cmbSongOrientation.SelectedIndex = 1;
            fontAndColorPanel.updateGuiFromSettingsObject(displayAndPrintSettings);
            songMetadataTemplatesPanel.updateGuiFromSettingsObject(displayAndPrintSettings);
        }

        public void updateSettingsObjectFromGui()
        {
            fontAndColorPanel.UpdateColorAndFontPreferences();
            songMetadataTemplatesPanel.UpdateMetadataTemplates();

            displayAndPrintSettings.ShowChords = chkShowChords.Checked ?? false;
            displayAndPrintSettings.ShowLyrics = chkShowLyrics.Checked ?? false;
            displayAndPrintSettings.ShowNotes = chkShowNotes.Checked ?? false;
            displayAndPrintSettings.DisplayEachSongSectionOnce = chkDisplayEachSongSectionOnce.Checked ?? false;

            if (cmbSongOrientation.SelectedIndex == 0)
                displayAndPrintSettings.SongOrientation = "Horizontal";
            else
                displayAndPrintSettings.SongOrientation = "Vertical";
        }

        public void SavePreferences()
        {

            updateSettingsObjectFromGui();
            displayAndPrintSettings.saveSettings();
        }
    }
}
