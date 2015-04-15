using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.Entities;
using System.Diagnostics;

namespace OpenChords.CrossPlatform.SongEditor
{
    public class SongMetadataPanel : Panel
    {
        //controls
        protected TextBox txtTitle = new TextBox();
        protected TextBox txtOrder = new TextBox();
        protected TextBox txtAuther = new TextBox();
        protected TextBox txtCopyright = new TextBox();
        protected ComboBox cmbTempo = new ComboBox();
        protected ComboBox cmbSignature = new ComboBox();
        protected TextBox txtCCLI = new TextBox();
        protected TextBox txtReference = new TextBox();
        protected TextBox txtKey = new TextBox();
        protected TextBox txtCapo = new TextBox();
        protected RadioButtonList radioListSharpsOrFlats = new RadioButtonList();
        protected TextBox txtBpm = new TextBox();
        protected TextArea txtLyrics = new TextArea();
        protected TextArea txtNotes = new TextArea();
        protected Splitter splitterMetadata;
        protected Splitter splitterSongNotes;

        public Song CurrentSong { get; set; }
        public bool SongChanged { get; set; }

        public SongMetadataPanel()
        {
            //add radio button options
            radioListSharpsOrFlats.Items.Add("Flats");
            radioListSharpsOrFlats.Items.Add("Sharps");

            //build ui
            this.Content = new Splitter()
            {
                Position = 150,
                Orientation = SplitterOrientation.Vertical,
                //metadata
                Panel1 = new GroupBox()
                {
                    Text = "Song Metadata",
                    Content = splitterMetadata = new Splitter()
                    {
                        Position = Helpers.getScreenPercentageInPixels(60, this),
                        //main metadata
                        Panel1 = new TableLayout()
                        {
                            Style = "padded-table",
                            Rows = 
                            {
                                new TableRow(new Label() { Text = "Title" }, new TableCell(txtTitle) { ScaleWidth = true}, new Label() { Text = "Key" }, txtKey),
                                new TableRow(new Label() { Text = "Order" }, txtOrder, new Label() { Text = "Capo" }, txtCapo),
                                new TableRow(new Label() { Text = "Author" }, txtAuther, new Label(), radioListSharpsOrFlats ),
                                new TableRow(new Label() { Text = "Copyright" }, txtCopyright, new Label() {Text = "Bpm"}, txtBpm ),
                                null,
                            }
                        },
                        //extra metadata
                        Panel2 = new TableLayout()
                        {
                            Style = "padded-table",
                            Rows = 
                            {
                                new TableRow(new Label() { Text = "Tempo" }, cmbTempo),
                                new TableRow(new Label() { Text = "Signature" }, cmbSignature),
                                new TableRow(new Label() { Text = "ccli" }, txtCCLI),
                                new TableRow(new Label() { Text = "Reference" }, txtReference),
                                null,
                        
                            }
                        }
                        
                    }
                },
                //lyrics and notes
                Panel2 = new Panel()
                {
                    //song editor and notes
                    Content = splitterSongNotes = new Splitter()
                    {
                        Position = Helpers.getScreenPercentageInPixels(60, this),
                        Orientation = SplitterOrientation.Horizontal,
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
            txtLyrics.Font = new Font(FontFamilies.Monospace, 16);
            txtNotes.Font = new Font(FontFamilies.Monospace, 16);
			txtLyrics.Wrap = false;
			txtNotes.Wrap = false;
            CurrentSong = new Song();

            this.SizeChanged += SongMetadataPanel_SizeChanged;
        }

        void SongMetadataPanel_SizeChanged(object sender, EventArgs e)
        {
            splitterSongNotes.Position = Helpers.getScreenPercentageInPixels(80, this);
            splitterMetadata.Position = Helpers.getScreenPercentageInPixels(80, this);
        }

  

        private bool showConfirmation(string message = "Are you sure?")
        {
            message = message.Replace("{0}", CurrentSong.title);
            DialogResult result = MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxType.Question);
            return result == DialogResult.Yes;
        }


        public void refreshPanel()
        {
            if (SongChanged)
                if (showConfirmation("Save changes to current song first?"))


            //disable events
            txtTitle.TextChanged -= fieldTextChanged;
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
            updateGuiFromSongObject();

            SongChanged = false;

            //events
            txtTitle.TextChanged += fieldTextChanged;
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
        }

        private void updateGuiFromSongObject()
        {
            if (CurrentSong != null)
            {
                txtTitle.Text = CurrentSong.title;
                txtOrder.Text = CurrentSong.presentation;
                txtAuther.Text = CurrentSong.author;
                txtCopyright.Text = CurrentSong.copyright;
                cmbTempo.Text = CurrentSong.tempo;
                cmbSignature.Text = CurrentSong.time_sig;
                txtCCLI.Text = CurrentSong.ccli;
                txtReference.Text = CurrentSong.hymn_number;
                txtLyrics.Text = CurrentSong.lyrics;
                txtNotes.Text = CurrentSong.notes;
                radioListSharpsOrFlats.SelectedIndex = CurrentSong.PreferFlats ? 0 : 1;
                txtKey.Text = CurrentSong.key;
                txtCapo.Text = CurrentSong.Capo.ToString();
                txtBpm.Text = CurrentSong.BeatsPerMinute.ToString();
            }
            else
            {
                txtTitle.Text = "";
                txtOrder.Text = "";
                txtAuther.Text = "";
                txtCopyright.Text = "";
                cmbTempo.Text = "";
                cmbSignature.Text = "";
                txtCCLI.Text = "";
                txtReference.Text = "";
                txtLyrics.Text = "";
                txtNotes.Text = "";
                radioListSharpsOrFlats.SelectedIndex = 1;
                txtKey.Text = "";
                txtCapo.Text = "";
                txtBpm.Text = "20";
            }
        }


        private void updateSongObjectFromGui()
        {
            int capoInt = 0;
            int.TryParse(txtCapo.Text, out capoInt);

            CurrentSong.title = txtTitle.Text;
            CurrentSong.presentation = txtOrder.Text;
            CurrentSong.author = txtAuther.Text;
            CurrentSong.copyright = txtCopyright.Text;
            CurrentSong.tempo = (cmbTempo.SelectedIndex != 0) ? cmbTempo.Text : "";
            CurrentSong.time_sig = (cmbSignature.SelectedIndex != 0) ? cmbSignature.Text : "";
            CurrentSong.ccli = txtCCLI.Text;
            CurrentSong.hymn_number = txtReference.Text;
            CurrentSong.lyrics = txtLyrics.Text;
            CurrentSong.notes = txtNotes.Text;
            CurrentSong.PreferFlats = (radioListSharpsOrFlats.SelectedIndex == 0);
            CurrentSong.key = txtKey.Text;
            CurrentSong.Capo = capoInt;
            CurrentSong.BeatsPerMinute = int.Parse(txtBpm.Text);
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
                if (showConfirmation("Save changes to {0} before creating a new song?"))
                    this.SaveSong();
            }

            CurrentSong = new Song();
            refreshPanel();
            if (!this.HasFocus)
                txtTitle.Focus();
        }

        internal void SaveSong()
        {
            if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show("Song title cannot be blank", MessageBoxType.Error);
                return;
            }

            updateSongObjectFromGui();

            SongChanged = false;

            CurrentSong.saveSong();

        }


        internal void DeleteSong()
        {
            if (showConfirmation("Are you sure you want to delete song {0}?"))
                CurrentSong.deleteSong();
        }


        internal void PresentSong()
        {
            if (SongChanged)
            {
                if (showConfirmation("Save changes to {0} before presenting?"))
                    this.SaveSong();
            }
            new frmPresent(CurrentSong, DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings)).Show();
        }

        internal void TransposeKeyUp()
        {
            CurrentSong.transposeKeyUp();
            updateGuiFromSongObject();

        }

        internal void TransposeKeyDown()
        {
            CurrentSong.transposeKeyDown();
            updateGuiFromSongObject();

        }

        internal void TransposeCapoUp()
        {
            CurrentSong.capoUp();
            updateGuiFromSongObject();
        }

        internal void TransposeCapoDown()
        {
            CurrentSong.capoDown();
            updateGuiFromSongObject();
        }

        internal void Close()
        {
            if (SongChanged)
            {
                if (showConfirmation("Save changes to {0}?"))
                    this.SaveSong();
            }

            this.Visible = false;
        }

        internal void ExportToHtml(DisplayAndPrintSettings settings)
        {
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

        internal void SetReadOnlyMode()
        {
            txtTitle.ReadOnly = true;
            txtOrder.ReadOnly = true;
            txtAuther.ReadOnly = true;
            txtCopyright.ReadOnly = true;
            cmbTempo.ReadOnly = true;
            cmbSignature.ReadOnly = true;
            txtCCLI.ReadOnly = true;
            txtReference.ReadOnly = true;
            txtLyrics.ReadOnly = true;
            txtNotes.ReadOnly = true;
            txtCapo.ReadOnly = true;
            txtKey.ReadOnly = true;
            cmbSignature.ReadOnly = true;
            cmbTempo.ReadOnly = true;
            radioListSharpsOrFlats.Enabled = false;
        }
    }
}
