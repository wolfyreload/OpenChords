using sharpPDF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OpenChords.Entities
{

    public class SongElementFormat
    {

        public string FontName {get;set;}
        public float FontSize {get;set;}
        public FontStyle FontStyle { get; set; }
        public string FontColorHex { get; set; }

        [XmlIgnore]
        public Color FontColor
        {
            get { return ColorTranslator.FromHtml(FontColorHex); }
            set { FontColorHex = ColorTranslator.ToHtml(value); }
        }

        [XmlIgnore]
        public pdfColor PdfColor
        {
            get
            {
                var p = new pdfColor(FontColor.R, FontColor.G, FontColor.B);
                return p;
            }
        }

        [XmlIgnore]
        public sharpPDF.Enumerators.predefinedFont PdfFont
        {
            get
            {
                if (FontName == "Courier New" && FontStyle == System.Drawing.FontStyle.Regular)
                    return sharpPDF.Enumerators.predefinedFont.csCourier;
                else if (FontName == "Courier New" && FontStyle == System.Drawing.FontStyle.Bold)
                    return sharpPDF.Enumerators.predefinedFont.csCourierBold;
                else if (FontName == "Courier New" && FontStyle == System.Drawing.FontStyle.Italic)
                    return sharpPDF.Enumerators.predefinedFont.csCourierOblique;
                if (FontName == "Helvetica" && FontStyle == System.Drawing.FontStyle.Regular)
                    return sharpPDF.Enumerators.predefinedFont.csHelvetica;
                else if (FontName == "Helvetica" && FontStyle == System.Drawing.FontStyle.Bold)
                    return sharpPDF.Enumerators.predefinedFont.csHelveticaBold;
                else if (FontName == "Helvetica" && FontStyle == System.Drawing.FontStyle.Italic)
                    return sharpPDF.Enumerators.predefinedFont.csHelveticaOblique;

                return sharpPDF.Enumerators.predefinedFont.csTimes;
            }
        }

        private Font _Font;
        [XmlIgnore]
        public Font Font
        {
            get
            {
                if (_Font == null)
                    _Font = new Font(FontName, FontSize, FontStyle);
                if (_Font.Size != FontSize)
                    _Font = new Font(FontName, FontSize, FontStyle);
                return _Font;

            }
        }

        private Brush _Brush;
 
        [XmlIgnore]
        public Brush Brush
        {
            get
            {
                if (_Brush == null)
                    _Brush = new SolidBrush(FontColor);
                return _Brush;
            }
        }

        public SongElementFormat()
        {

        }

        public SongElementFormat(string FontName, float FontSize, FontStyle Style, Color FontColor)
        {
            this.FontName = FontName;
            this.FontSize = FontSize;
            this.FontStyle = Style;
            this.FontColor = FontColor;
        }

        public SongElementFormat(string FontName, float FontSize, Color FontColor, bool? IsBold)
        {
            this.FontName = FontName;
            this.FontSize = FontSize;
            this.FontColor = FontColor;
            if (IsBold ?? true)
                this.FontStyle = FontStyle.Bold;
            else
                this.FontStyle = FontStyle.Regular;
        }

        public SongElementFormat Clone()
        {
            var SongElementFormat = new SongElementFormat()
            {
                FontName = this.FontName,
                FontSize = this.FontSize,
                FontColor = this.FontColor,
                FontStyle = this.FontStyle
            };
            return SongElementFormat;
        }
    }
}
