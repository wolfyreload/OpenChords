using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenChords.Functions
{
    public class FontList
    {
        private static List<string> _listOfAllFonts;
        private static List<string> _listOfMonoSpaceFonts;
        
        /// <summary>
        /// return a list of all fonts available on the system
        /// </summary>
        /// <returns></returns>
        public static List<string> ListOfAllFonts()
        {
            if (_listOfAllFonts != null) return new List<string>(_listOfAllFonts);

            _listOfAllFonts = new List<string>();
            //get a list of all font families
            FontFamily[] families;
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            families = installedFontCollection.Families;
           

            //get a list of all the fonts
            for (int i = 0; i < families.Length; i++)
            {
                FontFamily f = families[i];
                _listOfAllFonts.Add(f.Name);
            }
            return _listOfAllFonts;
        }

        /// <summary>
        /// list of all monospace fonts
        /// </summary>
        /// <returns></returns>
        public static List<string> ListOfAllMonospaceFonts()
        {
            if (_listOfMonoSpaceFonts != null) return new List<string>(_listOfMonoSpaceFonts);

            _listOfMonoSpaceFonts = new List<string>();
            
            FontFamily[] families;
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            families = installedFontCollection.Families;

            foreach (FontFamily ff in families)
            {
                if (ff.IsStyleAvailable(FontStyle.Regular))
                {
                    float diff;
                    using (Font font = new Font(ff, 16))
                    {
                        diff = TextRenderer.MeasureText("ABCDEF", font).Width - TextRenderer.MeasureText(".,;' i", font).Width;
                    }
                    if (Math.Abs(diff) < float.Epsilon * 2)
                    {
                        _listOfMonoSpaceFonts.Add(ff.Name);
                    }
                }

            }
            return _listOfMonoSpaceFonts;
        }






    }
}
