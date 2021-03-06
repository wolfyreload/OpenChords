﻿using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenChords.CrossPlatform.Preferences
{
    class FontAndColorPicker : Collection<TableCell>
    {
        public enum FontAndColorPickerType { FontAndColor, Color, Font }
        protected Label lblElementName = new Label();
        protected ComboBox cmbFont = new ComboBox() { ReadOnly = true, AutoComplete = true };
        protected ComboBox cmbFontSize = new ComboBox() { AutoComplete = true, Width = 60 };
        protected ColorPicker colorPicker = new ColorPicker();
        protected ComboBox cmbFontStyle = new ComboBox() { ReadOnly = true, AutoComplete = true, Width = 75};
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
                this.Add(lblElementName);
                this.Add(new TableCell() { Control = cmbFont, ScaleWidth = true });
                this.Add(cmbFontSize);
                this.Add(colorPicker);
                this.Add(cmbFontStyle);
                this.Add(new Label());
            }
            else if (colorPickerType == FontAndColorPickerType.Color)
            {
                this.Add(new Label() { Text = label });
                this.Add(new Label());
                this.Add(new Label());
                this.Add(colorPicker);
                this.Add(new Label());
            }
            else if (colorPickerType == FontAndColorPickerType.Font)
            {
                this.Add(new Label() { Text = label });
                this.Add(new TableCell() { Control = cmbFont, ScaleWidth = true });
                this.Add(cmbFontSize);
           
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
