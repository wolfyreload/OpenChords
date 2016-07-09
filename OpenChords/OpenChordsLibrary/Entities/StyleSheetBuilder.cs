using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Entities
{
    public class StyleSheetBuilder
    {

        private DisplayAndPrintSettings _settings;
        private string _styleSheet;

        public StyleSheetBuilder(DisplayAndPrintSettings settings)
        {
            _settings = settings;
            string styleSheet = HtmlResources.stylesheet;
            _styleSheet = setupStyleSheetForDisplaySettings(styleSheet);
        }


        private string setupStyleSheetForDisplaySettings(string baseStylesheet)
        {
            StringBuilder sb = new StringBuilder(baseStylesheet);
            configureStylesheetFonts(sb, _settings.HeadingsFormat, "Heading");
            configureStylesheetFonts(sb, _settings.ChordFormat, "Chord");
            configureStylesheetFonts(sb, _settings.LyricsFormat, "Lyric");
            configureStylesheetFonts(sb, _settings.NoteFormat, "Note");
            configureStylesheetFonts(sb, _settings.TitleFormat, "Title");

            configureStylesheetBackgrounColors(sb);

            configureStylesheetOrientation(sb);

            return sb.ToString();
        }

        private void configureStylesheetOrientation(StringBuilder sb)
        {
            if (_settings.SongOrientation == null || _settings.SongOrientation == "Horizontal")
                sb.Replace("<%SongOrientation%>", "inline-block");
            else
                sb.Replace("<%SongOrientation%>", "table");
        }

        private void configureStylesheetBackgrounColors(StringBuilder sb)
        {
            sb.Replace("<%BackgroundColor%>", _settings.BackgroundColorHex);
            sb.Replace("<%VerseBorderColor%>", _settings.VerseBorderColorHex);
            sb.Replace("<%HeadingBackgroundColor%>", _settings.VerseHeadingBackgroundColorHex);
            sb.Replace("<%VerseBackgroundColor1%>", _settings.VerseLyricsBackgroundColor1Hex);
            sb.Replace("<%VerseBackgroundColor2%>", _settings.VerseLyricsBackgroundColor2Hex);
            sb.Replace("<%PartialHeadingBackgroundColor%>", _settings.PartialVerseHeadingBackgroundColorHex);
        }

        internal string GetStyleSheet()
        {
            return _styleSheet;
        }

        private void configureStylesheetFonts(StringBuilder sb, Entities.SongElementFormat format, string elementName)
        {
            sb.Replace(string.Format("<%{0}FontSize%>", elementName), format.FontSize.ToString());
            sb.Replace(string.Format("<%{0}Weight%>", elementName), format.FontStyle.ToString());
            sb.Replace(string.Format("<%{0}Font%>", elementName), format.FontName);
            sb.Replace(string.Format("<%{0}Color%>", elementName), format.FontColorHex);
        }


    }
}
