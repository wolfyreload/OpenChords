using Eto.Forms;
using OpenChords.Entities;
using System;
using System.Collections.ObjectModel;

namespace OpenChords.CrossPlatform.Preferences
{
    internal class SongMetadataTemplatesPanel : Panel
    {

        private TextBox txtMetaDataTopLeft = new TextBox();
        private TextBox txtMetaDataTopMiddle = new TextBox();
        private TextBox txtMetaDataTopRight = new TextBox();
        private TextBox txtMetaDataMiddleLeft = new TextBox();
        private TextBox txtMetaDataMiddleMiddle = new TextBox();
        private TextBox txtMetaDataMiddleRight = new TextBox();
        private TextBox txtMetaDataBottomLeft = new TextBox();
        private TextBox txtMetaDataBottomMiddle = new TextBox();
        private TextBox txtMetaDataBottomRight = new TextBox();
        private DisplayAndPrintSettings displayAndPrintSettings;
        public event EventHandler ItemChanged;

        public SongMetadataTemplatesPanel()
        {
            Content = new TableLayout()
            {
                Style = "padded-table",
                Rows =
                    {
                        new TableRow(txtMetaDataTopLeft, txtMetaDataTopMiddle, txtMetaDataTopRight),
                        new TableRow(txtMetaDataMiddleLeft, txtMetaDataMiddleMiddle, txtMetaDataMiddleRight),
                        new TableRow(txtMetaDataBottomLeft, txtMetaDataBottomMiddle, txtMetaDataBottomRight),
                    }

            };
        }

        internal void updateGuiFromSettingsObject(DisplayAndPrintSettings displayAndPrintSettings)
        {
            this.displayAndPrintSettings = displayAndPrintSettings;

            txtMetaDataTopLeft.TextChanged -= MetaDataTemplate_TextChanged;
            txtMetaDataTopMiddle.TextChanged -= MetaDataTemplate_TextChanged;
            txtMetaDataTopRight.TextChanged -= MetaDataTemplate_TextChanged;

            txtMetaDataMiddleLeft.TextChanged -= MetaDataTemplate_TextChanged;
            txtMetaDataMiddleMiddle.TextChanged -= MetaDataTemplate_TextChanged;
            txtMetaDataMiddleRight.TextChanged -= MetaDataTemplate_TextChanged;

            txtMetaDataBottomLeft.TextChanged -= MetaDataTemplate_TextChanged;
            txtMetaDataBottomMiddle.TextChanged -= MetaDataTemplate_TextChanged;
            txtMetaDataBottomRight.TextChanged -= MetaDataTemplate_TextChanged;

            txtMetaDataTopLeft.Text = displayAndPrintSettings.SongMetaDataLayoutTop.LeftMetadata;
            txtMetaDataTopMiddle.Text = displayAndPrintSettings.SongMetaDataLayoutTop.MiddleMetadata;
            txtMetaDataTopRight.Text = displayAndPrintSettings.SongMetaDataLayoutTop.RightMetadata;

            txtMetaDataMiddleLeft.Text = displayAndPrintSettings.SongMetaDataLayoutMiddle.LeftMetadata;
            txtMetaDataMiddleMiddle.Text = displayAndPrintSettings.SongMetaDataLayoutMiddle.MiddleMetadata;
            txtMetaDataMiddleRight.Text = displayAndPrintSettings.SongMetaDataLayoutMiddle.RightMetadata;

            txtMetaDataBottomLeft.Text = displayAndPrintSettings.SongMetaDataLayoutBottom.LeftMetadata;
            txtMetaDataBottomMiddle.Text = displayAndPrintSettings.SongMetaDataLayoutBottom.MiddleMetadata;
            txtMetaDataBottomRight.Text = displayAndPrintSettings.SongMetaDataLayoutBottom.RightMetadata;

            txtMetaDataTopLeft.TextChanged += MetaDataTemplate_TextChanged;
            txtMetaDataTopMiddle.TextChanged += MetaDataTemplate_TextChanged;
            txtMetaDataTopRight.TextChanged += MetaDataTemplate_TextChanged;

            txtMetaDataMiddleLeft.TextChanged += MetaDataTemplate_TextChanged;
            txtMetaDataMiddleMiddle.TextChanged += MetaDataTemplate_TextChanged;
            txtMetaDataMiddleRight.TextChanged += MetaDataTemplate_TextChanged;

            txtMetaDataBottomLeft.TextChanged += MetaDataTemplate_TextChanged;
            txtMetaDataBottomMiddle.TextChanged += MetaDataTemplate_TextChanged;
            txtMetaDataBottomRight.TextChanged += MetaDataTemplate_TextChanged;
        }

        private void MetaDataTemplate_TextChanged(object sender, EventArgs e)
        {
            if (ItemChanged != null)
                ItemChanged(this, e);
        }

        internal void UpdateMetadataTemplates()
        {
            displayAndPrintSettings.SongMetaDataLayoutTop.LeftMetadata = txtMetaDataTopLeft.Text;
            displayAndPrintSettings.SongMetaDataLayoutTop.MiddleMetadata = txtMetaDataTopMiddle.Text;
            displayAndPrintSettings.SongMetaDataLayoutTop.RightMetadata = txtMetaDataTopRight.Text;

            displayAndPrintSettings.SongMetaDataLayoutMiddle.LeftMetadata = txtMetaDataMiddleLeft.Text;
            displayAndPrintSettings.SongMetaDataLayoutMiddle.MiddleMetadata = txtMetaDataMiddleMiddle.Text;
            displayAndPrintSettings.SongMetaDataLayoutMiddle.RightMetadata = txtMetaDataMiddleRight.Text;

            displayAndPrintSettings.SongMetaDataLayoutBottom.LeftMetadata = txtMetaDataBottomLeft.Text;
            displayAndPrintSettings.SongMetaDataLayoutBottom.MiddleMetadata = txtMetaDataBottomMiddle.Text;
            displayAndPrintSettings.SongMetaDataLayoutBottom.RightMetadata = txtMetaDataBottomRight.Text;
        
        }
    }
}