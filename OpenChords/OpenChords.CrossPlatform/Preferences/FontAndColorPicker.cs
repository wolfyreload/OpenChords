using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChords.CrossPlatform.Preferences
{
    class FontAndColorPicker : TableRow
    {
        public enum FontAndColorPickerType { FontAndColor, Color }
        protected Label lblElementName = new Label();
        protected ComboBox cmbFont = new ComboBox() { ReadOnly = true, AutoComplete = true };
        protected ComboBox cmbFontSize = new ComboBox() { AutoComplete = true, Width = 40 };
        protected ColorPicker colorPicker = new ColorPicker();
        protected ComboBox cmbFontStyle = new ComboBox() { ReadOnly = true, AutoComplete = true, Width = 65};
        public event EventHandler ItemChanged;
        public FontAndColorPicker(string label, FontAndColorPickerType colorPickerType, bool useMonospaceFontsOnly = false)
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

            if (colorPickerType == FontAndColorPickerType.FontAndColor)
            {
                Cells = new System.Collections.ObjectModel.Collection<TableCell>();
                Cells.Add(lblElementName);
                Cells.Add(new TableCell() { Control = cmbFont, ScaleWidth = true });
                Cells.Add(cmbFontSize);
                Cells.Add(colorPicker);
                Cells.Add(cmbFontStyle);
                Cells.Add(new Label());
            }
            else
            {
                Cells = new System.Collections.ObjectModel.Collection<TableCell>();
                Cells.Add(new Label() { Text = label });
                Cells.Add(new Label());
                Cells.Add(new Label());
                Cells.Add(colorPicker);
                Cells.Add(new Label());
            
            }
        }

        public void setFontFormat(Entities.SongElementFormat format)
        {
            cmbFont.TextChanged -= ValueChanged;
            cmbFontSize.TextChanged -= ValueChanged;
            cmbFontStyle.TextChanged -= ValueChanged;
            colorPicker.ValueChanged -= ValueChanged;
            
            cmbFont.Text = format.FontName;
            cmbFontSize.Text = format.FontSize.ToString();
            colorPicker.Value = getEtoColor(format.FontColor);
            cmbFontStyle.Text = format.FontStyle.ToString();

            cmbFont.TextChanged += ValueChanged;
            cmbFontSize.TextChanged += ValueChanged;
            cmbFontStyle.TextChanged += ValueChanged;
            colorPicker.ValueChanged += ValueChanged;
       
        }

        public void setColor(System.Drawing.Color color)
        {
            colorPicker.ValueChanged -= ValueChanged;
            colorPicker.Value = getEtoColor(color);
            colorPicker.ValueChanged += ValueChanged;
      
        }

    

        private void ValueChanged(object sender, EventArgs e)
        {
            if (ItemChanged != null)
                ItemChanged(this, e);
        }

        
        
        private static Eto.Drawing.Color getEtoColor(System.Drawing.Color color)
        {
            return Eto.Drawing.Color.FromArgb(color.R, color.G, color.B, color.A);
        }

        internal Entities.SongElementFormat GetSongElementFormat()
        {
            var colorPickerValue = colorPicker.Value;
            string fontName = cmbFont.Text;
            float fontSize = 1;
            float.TryParse(cmbFontSize.Text, out fontSize);
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
