using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                updateGui();         
            }
        }


        void updateGui()
        {
            var settings = OpenChords.Entities.DisplayAndPrintSettings.loadSettings(Entities.DisplayAndPrintSettingsType.TabletSettings, App_Code.Global.SettingsFileName);



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
            //chkGeneralNotes.Checked = settings.ShowGeneralNotes.Value;
            txtNoteWidth.Text = settings.NoteWidth.Value.ToString();

            txtBackgroundColor.Text = settings.BackgroundColorHex;
            setSettingsToFlowLayout(settings.TitleFormat, flowTitle);
            setSettingsToFlowLayout(settings.Order1Format, flowOrder1);
            setSettingsToFlowLayout(settings.Order2Format, flowOrder2);
            setSettingsToFlowLayout(settings.HeadingsFormat, flowHeadings);
            setSettingsToFlowLayout(settings.ChordFormat, flowChords);
            setSettingsToFlowLayout(settings.LyricsFormat, flowLyrics);
            setSettingsToFlowLayout(settings.NoteFormat, flowNotes);
        }

        void updateSettingsObject()
        {
            var settings = OpenChords.Entities.DisplayAndPrintSettings.loadSettings(Entities.DisplayAndPrintSettingsType.TabletSettings, App_Code.Global.SettingsFileName);

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
            //settings.ShowGeneralNotes = chkGeneralNotes.Checked;
            settings.NoteWidth = getInt(txtNoteWidth.Text);

            settings.BackgroundColorHex = txtBackgroundColor.Text;

            //settings.TitleFormat = getSettingsFromFlowLayout(flowTitle);
            //settings.Order1Format = getSettingsFromFlowLayout(flowOrder1);
            //settings.Order2Format = getSettingsFromFlowLayout(flowOrder2);
            //settings.HeadingsFormat = getSettingsFromFlowLayout(flowHeadings);
            //settings.ChordFormat = getSettingsFromFlowLayout(flowChords);
            //settings.LyricsFormat = getSettingsFromFlowLayout(flowLyrics);
            //settings.NoteFormat = getSettingsFromFlowLayout(flowNotes);
            settings.saveSettings();
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

        private SongElementFormat getSettingsFromFlowLayout(Panel flowLayout)
        {
            //var ddlFont = (ComboBox)flowLayout.Controls[1];
            //var sizeBox = (TextBox)flowLayout.Controls[2];
            //var color = (PictureBox)flowLayout.Controls[3];
            //var style = (ComboBox)flowLayout.Controls[4];

            //FontStyle fontStyle;
            //if (style.Text == "Bold")
            //    fontStyle = FontStyle.Bold;
            //else if (style.Text == "Regular")
            //    fontStyle = FontStyle.Regular;
            //else
            //    fontStyle = FontStyle.Italic;

            //var element = new SongElementFormat(ddlFont.Text, float.Parse(sizeBox.Text), fontStyle, color.BackColor);
            //return element;
            return null;
        }



        private void setSettingsToFlowLayout(SongElementFormat songElement, Panel flowLayout)
        {
            //var ddlFont = (ComboBox)flowLayout.Controls[1];
            //var sizeBox = (TextBox)flowLayout.Controls[2];
            //var colorPickerBox = (PictureBox)flowLayout.Controls[3];
            //var style = (ComboBox)flowLayout.Controls[4];

            //ddlFont.DataSource = new string[] { "Helvetica", "Courier New" };
            //style.DataSource = new string[] { "Regular", "Bold", "Italic" };


            //sizeBox.Text = songElement.FontSize.ToString();
            //colorPickerBox.BackColor = songElement.FontColor;
            //ddlFont.Text = songElement.FontName;
            //style.Text = songElement.FontStyle.ToString();

            ////add event for changing color
            //colorPickerBox.Click += new System.EventHandler(this.ColorPicker_Click);
        }


        protected void cmdSave_Click(object sender, EventArgs e)
        {
            updateSettingsObject();
            Response.Redirect("~/");
        }
    }
}