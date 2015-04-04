using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.Entities;

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
        protected TextArea txtLyrics = new TextArea();
        protected TextArea txtNotes = new TextArea();

        public Song CurrentSong { get; set; }
        public bool SongChanged { get; set; }
        int ScreenWidth;
        int ScreenHeight;

        public SongMetadataPanel()
        {
            this.Visible = false;
            ScreenWidth = (int)Screen.DisplayBounds.Width;
            ScreenHeight = (int)Screen.DisplayBounds.Height;

            //add a style to the table layout
            Eto.Style.Add<TableLayout>("padded-table", table => {
                table.Padding = new Padding(10);
                table.Spacing = new Size(5, 5);
            });

            //add radio button options
            radioListSharpsOrFlats.Items.Add("Flats");
            radioListSharpsOrFlats.Items.Add("Sharps");

            //build ui
            this.Content = new Splitter()
            {
                Orientation = SplitterOrientation.Vertical,
                //metadata
                Panel1 = new Splitter()
                {
                    Position = Helpers.getScreenPercentageInPixels(75),
                    Panel1 = new Splitter()
                    {
                        Position = Helpers.getScreenPercentageInPixels(40),
                        //column 1 of metadata
                        Panel1 = new TableLayout()
                        {
                            Style = "padded-table",
                            Rows = 
                            {
                                new TableRow(new Label() { Text = "Title" }, txtTitle),
                                new TableRow(new Label() { Text = "Order" }, txtOrder),
                                new TableRow(new Label() { Text = "Author" }, txtAuther),
                                new TableRow(new Label() { Text = "Copyright" }, txtCopyright),
                                null,
                            },
                        },
                        //row 2 of metadata
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
                        },
                    },
                    Panel2 = new Splitter()
                    {
                        //row 3 of metadata
                        Panel1 = new TableLayout()
                        {
                            Style = "padded-table",
                            Rows = 
                            {
                                new TableRow(new Label() { Text = "Key" }, txtKey),
                                new TableRow(new Label() { Text = "Capo" }, txtCapo),
                                new TableRow( null, radioListSharpsOrFlats ),
                                null,
                            }
                        },
                        //row 4 with buttons
                        Panel2 = null,
                        FixedPanel = SplitterFixedPanel.Panel2
                    },

                },
                //lyrics and notes
                Panel2 = new Panel()
                {
                    //song editor and notes
                    Content = new Splitter()
                    {
                        Position = Helpers.getScreenPercentageInPixels(70),
                        Orientation = SplitterOrientation.Horizontal,
                        //lyrics editor
                        Panel1 = new GroupBox()
                        {
                            Text = "Chords/Lyrics Editor",
                            Content = txtLyrics,

                        },
                        //notes editor
                        Panel2 = new GroupBox()
                        {
                            Text = "Notes Editor",
                            Content = txtNotes,
                        },
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

        }

        private bool showConfirmation(string message = "Are you sure?")
        {
            message = message.Replace("{0}", CurrentSong.title);
            DialogResult result = MessageBox.Show(message, MessageBoxButtons.YesNo, MessageBoxType.Question);
            return result == DialogResult.Yes;
        }

        
        public void refreshPanel()
        {
            if (SongChanged)
                if (showConfirmation("Save changes to {0}?"))


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

            this.Visible = true;
        }

        private void updateGuiFromSongObject()
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

        internal void ExportToHtml()
        {
            string html = CurrentSong.getHtml(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings));
            string fileName = string.Format("{0}/{1}.html", Settings.ExtAppsAndDir.printFolder, CurrentSong.title);
            File.WriteAllText(fileName, html);
        }
    }
}
