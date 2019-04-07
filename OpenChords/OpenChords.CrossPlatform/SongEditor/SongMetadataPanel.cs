using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.Entities;
using System.Diagnostics;
using OpenChords.Functions;

namespace OpenChords.CrossPlatform.SongEditor
{
    public class SongMetadataPanel : Panel
    {
        //controls
        protected TextBox txtTitle = new TextBox();
        protected TextBox txtAlternativeTitle = new TextBox();
        protected TextBox txtSubFolder = new TextBox();
        protected TextBox txtOrder = new TextBox();
        protected TextBox txtAuther = new TextBox();
        protected TextBox txtCopyright = new TextBox();
        protected ComboBox cmbTempo = new ComboBox();
        protected ComboBox cmbSignature = new ComboBox();
        protected TextBox txtCCLI = new TextBox();
        protected TextBox txtReference = new TextBox();
        protected TextBox txtKey = new TextBox();
        protected TextBox txtCapo = new TextBox();
        protected TextBox txtBpm = new TextBox();
        protected TextArea txtLyrics = new TextArea();
        protected TextArea txtNotes = new TextArea();
        protected Splitter splitterMetadata;
        protected Splitter splitterSongNotes;

        protected Button cmdShowExtraMetadata = new Button() { Text = "Toggle Metadata" };
        protected Button cmdShowNormalMetadata = new Button() { Text = "Toggle Metadata" };

        private bool showNormalMetadata = true;
        private TableLayout normalMetadataPanel1;
        private TableLayout normalMetadataPanel2;
        private TableLayout additionalMetadataPanel1;
        private TableLayout additionalMetadataPanel2;

        public Song CurrentSong { get; private set; }
        public bool SongChanged { get; set; }

        public SongMetadataPanel()
        {

            //build ui
            this.Content = new Splitter()
            {
                Position = 160,
                Orientation = Orientation.Vertical,
                //metadata
                Panel1 = buildMetadataPanel(),
                //lyrics and notes
                Panel2 = new Panel()
                {
                    //song editor and notes
                    Content = splitterSongNotes = new Splitter()
                    {
                        Position = Helpers.FormHelper.getScreenXPercentageInPixels(60, this),
                        Orientation = Orientation.Horizontal,
                        //lyrics editor
                        Panel1 = new GroupBox() { Text = "Chords/Lyrics Editor", Content = txtLyrics },
                        //notes editor
                        Panel2 = new GroupBox() { Text = "Notes Editor", Content = txtNotes }
                    },

                },
            };

            foreach (var sig in Song.TimeSignatureOptions())
            {
                cmbSignature.Items.Add(sig);
            }
            foreach (var tempo in Song.TempoOptions())
            {
                cmbTempo.Items.Add(tempo);
            }


            //set font size for notes and lyrics
            setUserInterfaceFonts();
            CurrentSong = new Song();

            this.SizeChanged += SongMetadataPanel_SizeChanged;
            cmdShowExtraMetadata.Click += cmdToggleMetadata_Click;
            cmdShowNormalMetadata.Click += cmdToggleMetadata_Click;
        }

        private Panel buildMetadataPanel()
        {
            normalMetadataPanel1 = new TableLayout()
            {
                Style = "padded-table",
                Rows =
                {
                    new TableRow(new Label() { Text = "Title" }, txtTitle),
                    new TableRow(new Label() { Text = "Author" }, txtAuther),
                    new TableRow(new Label() { Text = "Order" }, txtOrder),
                    null,
                }
            };
            normalMetadataPanel2 = new TableLayout()
            {
                Style = "padded-table",
                Rows =
                {
                    new TableRow(new Label() { Text = "Key" }, txtKey),
                    new TableRow(new Label() { Text = "Capo" }, txtCapo),
                    new TableRow(new Label() { Text = "Reference" }, txtReference),
                    new TableRow(new Label() { Height = 20 },  cmdShowExtraMetadata),
                    null,
                }
            };
            additionalMetadataPanel1 = new TableLayout()
            {
                Style = "padded-table",
                Rows =
                {
                    new TableRow(new Label() { Text = "Alternative Title" }, txtAlternativeTitle),
                    new TableRow(new Label() { Text = "Sub-Folder" }, txtSubFolder),
                    new TableRow(new Label() { Text = "Copyright" }, txtCopyright),
                    new TableRow(new Label() { Text = "ccli" }, txtCCLI),
                    null,

                }
            };
            additionalMetadataPanel2 = new TableLayout()
            {
                Style = "padded-table",
                Rows =
                {
                    new TableRow(new Label() { Text = "Bpm"}, txtBpm ),
                    new TableRow(new Label() { Text = "Signature" }, cmbSignature),
                    new TableRow(new Label() { Text = "Tempo" }, cmbTempo),
                    new TableRow(new Label() { Height = 20 }, cmdShowNormalMetadata),
                    null,
                }
            };

            var groupBox = new GroupBox()
            {
                Text = "Song Metadata",
                Content = splitterMetadata = new Splitter()
                {
                    Position = Helpers.FormHelper.getScreenXPercentageInPixels(60, this),
                    Panel1 = normalMetadataPanel1,
                    Panel2 = normalMetadataPanel2
                }
            };

            return groupBox;
        }

        private void cmdToggleMetadata_Click(object sender, EventArgs e)
        {
            showNormalMetadata = !showNormalMetadata;
            if (showNormalMetadata)
            {
                splitterMetadata.Panel1 = normalMetadataPanel1;
                splitterMetadata.Panel2 = normalMetadataPanel2;
            }
            else
            {
                splitterMetadata.Panel1 = additionalMetadataPanel1;
                splitterMetadata.Panel2 = additionalMetadataPanel2;
            };
        }

        private void setUserInterfaceFonts()
        {
            txtLyrics.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.LyricsFormat);
            txtNotes.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.NoteFormat);
            txtTitle.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtAlternativeTitle.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtSubFolder.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtAuther.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtOrder.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtCopyright.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtKey.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtCapo.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtBpm.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            cmbSignature.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            cmbTempo.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtCCLI.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtReference.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            txtLyrics.Wrap = false;
            txtNotes.Wrap = false;
        }



        void SongMetadataPanel_SizeChanged(object sender, EventArgs e)
        {
            splitterSongNotes.Position = Helpers.FormHelper.getScreenXPercentageInPixels(80, this);
            splitterMetadata.Position = Helpers.FormHelper.getScreenXPercentageInPixels(80, this);
        }

        public void refreshPanel()
        {
            //disable events
            txtTitle.TextChanged -= fieldTextChanged;
            txtAlternativeTitle.TextChanged -= fieldTextChanged;
            txtSubFolder.TextChanged -= fieldTextChanged;
            txtOrder.TextChanged -= fieldTextChanged;
            txtAuther.TextChanged -= fieldTextChanged;
            txtCopyright.TextChanged -= fieldTextChanged;
            cmbTempo.TextChanged -= fieldTextChanged;
            cmbSignature.TextChanged -= fieldTextChanged;
            txtCCLI.TextChanged -= fieldTextChanged;
            txtReference.TextChanged -= fieldTextChanged;
            txtLyrics.TextChanged -= fieldTextChanged;
            txtNotes.TextChanged -= fieldTextChanged;
            txtCapo.TextChanged -= fieldTextChanged;
            txtKey.TextChanged -= fieldTextChanged;
            txtBpm.TextChanged -= fieldTextChanged;
            txtOrder.LostFocus -= TxtOrder_LostFocus;
            updateGuiFromSongObject();

            SongChanged = false;

            //events
            txtTitle.TextChanged += fieldTextChanged;
            txtAlternativeTitle.TextChanged += fieldTextChanged;
            txtSubFolder.TextChanged += fieldTextChanged;
            txtOrder.TextChanged += fieldTextChanged;
            txtAuther.TextChanged += fieldTextChanged;
            txtCopyright.TextChanged += fieldTextChanged;
            cmbTempo.SelectedIndexChanged += comboSelectedIndexChanged;
            cmbSignature.SelectedIndexChanged += comboSelectedIndexChanged;
            txtCCLI.TextChanged += fieldTextChanged;
            txtReference.TextChanged += fieldTextChanged;
            txtLyrics.TextChanged += fieldTextChanged;
            txtNotes.TextChanged += fieldTextChanged;
            txtCapo.TextChanged += fieldTextChanged;
            txtKey.TextChanged += fieldTextChanged;
            txtBpm.TextChanged += fieldTextChanged;
            txtOrder.LostFocus += TxtOrder_LostFocus;

            // Disable the tab key in our text areas
            txtLyrics.KeyDown += fieldKeyDown;
            txtNotes.KeyDown += fieldKeyDown;
            
            txtLyrics.TextChanged += TxtLyrics_TextChanged;
        }

        private void TxtLyrics_TextChanged(object sender, EventArgs e)
        {
            // If we paste a song with Tabs in it replace the tabs with the default 4 spaces
            if (txtLyrics.Text.Contains("\t"))
            {
                txtLyrics.Text = txtLyrics.Text.Replace("\t", "    ");
            }
        }

        private void fieldKeyDown(object sender, KeyEventArgs e)
        {
            // Disable the tab key
            if (e.Key == Keys.Tab)
                e.Handled = true;
        }

        internal void SplitSongLine()
        {
            if (txtLyrics.Selection.Start == 0 || txtLyrics.Selection.Length() > 0) return;
            int splitPosition = txtLyrics.Selection.Start;
            string lyrics = txtLyrics.Text;
            int newPosition = SongProcessor.BreakSongLine(ref lyrics, splitPosition);
            txtLyrics.Text = lyrics;
            txtLyrics.CaretIndex = newPosition;

        }

        private void TxtOrder_LostFocus(object sender, EventArgs e)
        {
            txtOrder.Text = txtOrder.Text.ToUpper();
        }

        private void updateGuiFromSongObject()
        {
            if (CurrentSong != null)
            {
                txtTitle.Text = CurrentSong.title;
                txtAlternativeTitle.Text = CurrentSong.aka;
                txtOrder.Text = CurrentSong.presentation;
                txtAuther.Text = CurrentSong.author;
                txtCopyright.Text = CurrentSong.copyright;
                cmbTempo.Text = CurrentSong.tempo;
                cmbSignature.Text = CurrentSong.time_sig;
                txtCCLI.Text = CurrentSong.ccli;
                txtReference.Text = CurrentSong.hymn_number;
                txtLyrics.Text = CurrentSong.lyrics;
                txtNotes.Text = CurrentSong.notes;
                txtKey.Text = CurrentSong.key;
                txtCapo.Text = CurrentSong.Capo.ToString();
                txtBpm.Text = CurrentSong.BeatsPerMinute.ToString();
                txtSubFolder.Text = CurrentSong.SongSubFolder;
            }
            else
            {
                txtTitle.Text = "";
                txtAlternativeTitle.Text = "";
                txtOrder.Text = "";
                txtAuther.Text = "";
                txtCopyright.Text = "";
                cmbTempo.Text = "";
                cmbSignature.Text = "";
                txtCCLI.Text = "";
                txtReference.Text = "";
                txtLyrics.Text = "";
                txtNotes.Text = "";
                txtKey.Text = "";
                txtCapo.Text = "";
                txtSubFolder.Text = "";
                txtBpm.Text = "20";
            }
        }


        private void updateSongObjectFromGui()
        {
            int capoInt = 0;
            int.TryParse(txtCapo.Text, out capoInt);

            CurrentSong.title = txtTitle.Text;
            CurrentSong.aka = txtAlternativeTitle.Text;
            CurrentSong.presentation = txtOrder.Text.ToUpper();
            CurrentSong.author = txtAuther.Text;
            CurrentSong.copyright = txtCopyright.Text;
            CurrentSong.tempo = (cmbTempo.SelectedIndex != 0) ? cmbTempo.Text : "";
            CurrentSong.time_sig = (cmbSignature.SelectedIndex != 0) ? cmbSignature.Text : "";
            CurrentSong.ccli = txtCCLI.Text;
            CurrentSong.hymn_number = txtReference.Text;
            CurrentSong.lyrics = txtLyrics.Text;
            CurrentSong.notes = txtNotes.Text;
            CurrentSong.key = txtKey.Text;
            CurrentSong.Capo = capoInt;
            CurrentSong.SongSubFolder = txtSubFolder.Text;
            int tempBpm = 100;
            if (int.TryParse(txtBpm.Text, out tempBpm))
                CurrentSong.BeatsPerMinute = tempBpm;
        }

        private void comboSelectedIndexChanged(object sender, EventArgs e)
        {
            SongChanged = true;
        }

        private void fieldTextChanged(object sender, EventArgs e)
        {
            SongChanged = true;
        }

        internal void NewSong()
        {
            if (SongChanged)
            {
                if (Helpers.PopupMessages.ShowConfirmationMessage(this, "Save changes to {0} before creating a new song?", CurrentSong.title))
                    this.SaveSong();
            }

            CurrentSong = new Song();
            refreshPanel();
            if (!this.HasFocus)
                txtTitle.Focus();
        }

        /// <summary>
        /// returns true if it is a new song
        /// </summary>
        /// <returns></returns>
        internal bool SaveSong()
        {
            if (txtTitle.Text.Trim() == "")
            {
                Helpers.PopupMessages.ShowErrorMessage(this, "Song title cannot be blank");
                return false;
            }

            string originalSongName = (string.IsNullOrWhiteSpace(CurrentSong.SongSubFolder)) 
                                        ? CurrentSong.title : $"{CurrentSong.SongSubFolder}/{CurrentSong.title}";
            string newSongName = (string.IsNullOrWhiteSpace(txtSubFolder.Text))
                                    ? txtTitle.Text : $"{txtSubFolder.Text}/{txtTitle.Text}";
            bool wasSongNameChanged = !string.Equals(originalSongName, newSongName, StringComparison.InvariantCultureIgnoreCase);
            
            //Check if there is already a song with the name chosen
            bool doesSongExist = Song.Exists(newSongName);
            if (doesSongExist && wasSongNameChanged)
            {
                Helpers.PopupMessages.ShowErrorMessage(this, $"Song '{newSongName}' already exists please choose another name");
                return false;
            }

            // If the song is a rename delete the old song
            if (wasSongNameChanged)
                CurrentSong.deleteSong();

            // Save the song and 
            updateSongObjectFromGui();
            bool isNewSong = CurrentSong.saveSong();

            //notify the user
            Helpers.PopupMessages.ShowInformationMessage(this, $"Song '{CurrentSong.SongTitleIncludingSubFolder}' saved");

            SongChanged = false;

            return isNewSong;
        }

        internal void ChangeToSong(Song newSong)
        {
            if (SongChanged)
            {
                if (Helpers.PopupMessages.ShowConfirmationMessage(this, "Save changes to current song '{0}' before changing to another song?", CurrentSong.title))
                {
                    this.SaveSong(); //save the song if we want the changes if the user clicks Yes
                }
                else
                {
                    CurrentSong.revertToSaved(); //otherwise we want to revert the song to its original state
                }
            }
            CurrentSong = newSong;
            refreshPanel();
        }

        internal bool DeleteSong()
        {
            if (Helpers.PopupMessages.ShowConfirmationMessage(this, "Are you sure you want to delete song '{0}'?", CurrentSong.SongFileName))
            {
                CurrentSong.deleteSong();
                return true;
            }
            else
            {
                return false;
            }
        }


        internal void PresentSong()
        {
            if (SongChanged)
            {
                updateSongObjectFromGui();
                if (Helpers.PopupMessages.ShowConfirmationMessage(this, "Save changes to song '{0}' before presenting?", CurrentSong.title))
                    this.SaveSong();
            }
            if (CurrentSong == null || CurrentSong.title == "")
            {
                Helpers.PopupMessages.ShowErrorMessage(this, "No song selected");
                return;
            }
            var frmPresent = new frmPresent(CurrentSong, DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings));
            frmPresent.Closing += FrmPresent_Closing;
            frmPresent.Show();
        }

        private void FrmPresent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((frmPresent)sender).Closing -= FrmPresent_Closing;
            updateGuiFromSongObject();
            SongChanged = false;
        }

        internal void TransposeKeyUp()
        {
            updateSongObjectFromGui();
            CurrentSong.transposeKeyUp();
            updateGuiFromSongObject();

        }

        internal void TransposeKeyDown()
        {
            updateSongObjectFromGui();
            CurrentSong.transposeKeyDown();
            updateGuiFromSongObject();

        }

        internal void TransposeCapoUp()
        {
            updateSongObjectFromGui();
            CurrentSong.capoUp();
            updateGuiFromSongObject();
        }

        internal void TransposeCapoDown()
        {
            updateSongObjectFromGui();
            CurrentSong.capoDown();
            updateGuiFromSongObject();
        }

        internal void Close()
        {
            if (SongChanged)
            {
                if (Helpers.PopupMessages.ShowConfirmationMessage(this, "Save changes to song '{0}'?", CurrentSong.title))
                    this.SaveSong();
            }

            this.Visible = false;
        }

        internal void ExportToHtml(DisplayAndPrintSettings settings)
        {
            if (CurrentSong.title == "")
            {
                Helpers.PopupMessages.ShowErrorMessage(this, "Please select a song");
                return;
            }
            string destination = CurrentSong.ExportToHtml(settings);
            Process.Start(destination);
        }

        internal void FixFormatting()
        {
            updateSongObjectFromGui();
            CurrentSong.fixFormatting();
            CurrentSong.fixLyricsOrdering();
            CurrentSong.fixNoteOrdering();
            updateGuiFromSongObject();
        }
    }
}
