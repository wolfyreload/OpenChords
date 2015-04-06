using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChords.CrossPlatform.Preferences
{
    class FontAndColorPicker : TableRow
    {
        protected Label lblElementName = new Label();
        protected ComboBox cmbFont = new ComboBox() { AutoComplete = true };
        protected ComboBox cmbFontSize = new ComboBox() { AutoComplete = true };
        protected ColorPicker colorPicker = new ColorPicker();
        protected ComboBox cmbFontStyle = new ComboBox() { AutoComplete = true };

        public FontAndColorPicker(string label, Entities.SongElementFormat format, bool useMonospaceFontsOnly = false)
        {

            //fill dropdownlists
            if (useMonospaceFontsOnly)
                foreach (string font in Functions.FontList.ListOfAllMonospaceFonts())
                {
                    cmbFont.Items.Add(font);
                }
            else
                foreach (string font in Functions.FontList.ListOfAllFonts())
                {
                    cmbFont.Items.Add(font);
                }
            for (int i = 1; i<=100; i++)
            {
                cmbFontSize.Items.Add(i.ToString());
            }
            cmbFontStyle.Items.Add("Regular");
            cmbFontStyle.Items.Add("Bold");
            cmbFontStyle.Items.Add("Italic");


            lblElementName.Text = label;
            cmbFont.Text = format.FontName;
            cmbFontSize.Text = format.FontSize.ToString();
            colorPicker.Value = getColor(format.FontColor);
            cmbFontStyle.Text = format.FontStyle.ToString();


            Cells = new System.Collections.ObjectModel.Collection<TableCell>();
            Cells.Add(lblElementName);
            Cells.Add(new TableCell() { Control = cmbFont, ScaleWidth = true });
            Cells.Add(cmbFontSize);
            Cells.Add(colorPicker);
            Cells.Add(cmbFontStyle);
        }

        public FontAndColorPicker(string label, System.Drawing.Color color)
        {

            lblElementName.Text = label;

            Cells = new System.Collections.ObjectModel.Collection<TableCell>();
            Cells.Add(new Label() { Text = label });
            Cells.Add(new Label());
            Cells.Add(new Label());
            Cells.Add(colorPicker);
            Cells.Add(new Label());
        }

        private Entities.SongElementFormat getSongElementFormat()
        {
            return new Entities.SongElementFormat();
        }
        
        private static Eto.Drawing.Color getColor(System.Drawing.Color color)
        {
            return Eto.Drawing.Color.FromArgb(color.R, color.G, color.B, color.A);
        }

        internal Entities.SongElementFormat GetSongElementFormat()
        {
            var colorPickerValue = colorPicker.Value;
            string fontName = cmbFont.Text;
            float fontSize = float.Parse(cmbFontSize.Text);
            System.Drawing.Color color = System.Drawing.Color.FromArgb((int)colorPickerValue.Rb, (int)colorPickerValue.Gb, (int)colorPickerValue.Bb);
            System.Drawing.FontStyle fontStyle = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), cmbFontStyle.Text);
            return new Entities.SongElementFormat(fontName, fontSize, fontStyle, color);
        }

        internal System.Drawing.Color getColor()
        {
            var colorPickerValue = colorPicker.Value;
            System.Drawing.Color color = System.Drawing.Color.FromArgb((int)colorPickerValue.Rb, (int)colorPickerValue.Gb, (int)colorPickerValue.Bb);
            return color;
        }
    }
}
