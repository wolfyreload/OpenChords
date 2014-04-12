using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenChords.Entities;

namespace OpenChords.Forms.Custom_Controls
{


    public partial class SettingsPanel : UserControl
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private DisplayAndPrintSettings settings;

        public SettingsPanel()
        {
            InitializeComponent();
       
        }

        public SettingsPanel(Entities.DisplayAndPrintSettingsType displayAndPrintSettingsType)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            settings = DisplayAndPrintSettings.loadSettings(displayAndPrintSettingsType);
       
            updateGui();
        }

        public void loadSettings(Entities.DisplayAndPrintSettingsType displayAndPrintSettingsType)
        {
             settings = DisplayAndPrintSettings.loadSettings(displayAndPrintSettingsType);
       
            updateGui();
        }

        private void SettingsPanel_Load(object sender, EventArgs e)
        {

        }

        void updateGui()
        {
            //settings
            txtWidth.Text = settings.pageWidth.ToString();
            txtHeight.Text = settings.pageHeight.ToString();

            txtTopMargin.Text = settings.topPageMargin.ToString();
            txtLeftMargin.Text = settings.leftPageMargin.ToString();
            txtBottomMargin.Text = settings.bottomPageMargin.ToString();
            txtRightMargin.Text = settings.rightPageMargin.ToString();

            txtContentLineSpacing.Text = settings.contentLineSpacing.ToString();
            txtParagraphSpacing.Text = settings.paragraphSpacing.ToString();

            chkDualColumns.Checked = settings.DualColumns.Value;
            chkShowNotes.Checked = settings.ShowNotes.Value;
            chkShowLyrics.Checked = settings.ShowLyrics.Value;
            chkShowChords.Checked = settings.ShowChords.Value;
            chkPleaseTurnOver.Checked = settings.ShowPleaseTurnOver.Value;
            //chkGeneralNotes.Checked = settings.ShowGeneralNotes.Value;
            txtNoteWidth.Text = settings.NoteWidth.Value.ToString();

            picBackgroundColor.BackColor = settings.BackgroundColor;
            setSettingsToFlowLayout(settings.TitleFormat, flowTitle);
            setSettingsToFlowLayout(settings.Order1Format, flowOrder1);
            setSettingsToFlowLayout(settings.Order2Format, flowOrder2);
            setSettingsToFlowLayout(settings.HeadingsFormat, flowHeadings);
            setSettingsToFlowLayout(settings.ChordFormat, flowChords);
            setSettingsToFlowLayout(settings.LyricsFormat, flowLyrics);
            setSettingsToFlowLayout(settings.NoteFormat, flowNotes);
            setSettingsToFlowLayout(settings.NextPageFormat, flowNextPage);
        }

        private int getInt(string input)
        {
            int output = 0;
            int.TryParse(input, out output);
            return output;
        }

        private float getFloat(string input)
        {
            float output = 0;
            float.TryParse(input, out output);
            return output;
        }

        void updateSettingsObject()
        {
            //settings
            settings.pageWidth = getInt(txtWidth.Text);
            settings.pageHeight = getInt(txtHeight.Text);

            settings.topPageMargin = getInt(txtTopMargin.Text);
            settings.leftPageMargin = getInt(txtLeftMargin.Text);
            settings.rightPageMargin = getInt(txtRightMargin.Text);
            settings.bottomPageMargin = getInt(txtBottomMargin.Text);

            settings.contentLineSpacing = getFloat(txtContentLineSpacing.Text);
            settings.paragraphSpacing = getFloat(txtParagraphSpacing.Text);

            settings.DualColumns = chkDualColumns.Checked;
            settings.ShowNotes = chkShowNotes.Checked;
            settings.ShowLyrics = chkShowLyrics.Checked;
            settings.ShowChords = chkShowChords.Checked;
            settings.ShowPleaseTurnOver = chkPleaseTurnOver.Checked;
            settings.NoteWidth = getInt(txtNoteWidth.Text);

            settings.BackgroundColor = picBackgroundColor.BackColor;

            settings.TitleFormat = getSettingsFromFlowLayout(flowTitle);
            settings.Order1Format = getSettingsFromFlowLayout(flowOrder1);
            settings.Order2Format = getSettingsFromFlowLayout(flowOrder2);
            settings.HeadingsFormat = getSettingsFromFlowLayout(flowHeadings);
            settings.ChordFormat = getSettingsFromFlowLayout(flowChords);
            settings.LyricsFormat = getSettingsFromFlowLayout(flowLyrics);
            settings.NoteFormat = getSettingsFromFlowLayout(flowNotes);
            settings.NextPageFormat = getSettingsFromFlowLayout(flowNextPage);

        }

        private SongElementFormat getSettingsFromFlowLayout(FlowLayoutPanel flowLayout)
        {
            var ddlFont = (ComboBox)flowLayout.Controls[1];
            var sizeBox = (TextBox)flowLayout.Controls[2];
            var color = (PictureBox)flowLayout.Controls[3];
            var style = (ComboBox)flowLayout.Controls[4];

            FontStyle fontStyle;
            if (style.Text == "Bold")
                fontStyle = FontStyle.Bold;
            else if (style.Text == "Regular")
                fontStyle = FontStyle.Regular;
            else 
                fontStyle = FontStyle.Italic;

            var element = new SongElementFormat(ddlFont.Text, float.Parse(sizeBox.Text), fontStyle, color.BackColor);
            return element;

        }



        private void setSettingsToFlowLayout(SongElementFormat songElement, FlowLayoutPanel flowLayout)
        {
            var ddlFont = (ComboBox)flowLayout.Controls[1];
            var sizeBox = (TextBox)flowLayout.Controls[2];
            var colorPickerBox = (PictureBox)flowLayout.Controls[3];
            var style = (ComboBox)flowLayout.Controls[4];

            ddlFont.DataSource = new string[] { "Helvetica", "Courier New" };
            style.DataSource = new string[] { "Regular", "Bold", "Italic" };
          

            sizeBox.Text = songElement.FontSize.ToString();
            colorPickerBox.BackColor = songElement.FontColor;
            ddlFont.Text = songElement.FontName;
            style.Text = songElement.FontStyle.ToString();

            //add event for changing color
            colorPickerBox.Click += new System.EventHandler(this.ColorPicker_Click);
        }
        
        public void saveSettings()
        {
            updateSettingsObject();
            settings.saveSettings();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            settings = new DisplayAndPrintSettings(settings.settingsType);
            updateGui();
        }

        private void ColorPicker_Click(object sender, EventArgs e)
        {
            var colorPickBox = (PictureBox)sender;

            colorPicker.Color = colorPickBox.BackColor;

            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;

            var selectedColor = colorPicker.Color;
            
            colorPickBox.BackColor = selectedColor;
        }






















    }
}
