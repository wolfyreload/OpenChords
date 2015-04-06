using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChords.Export
{
    public class ExportToHtml
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DisplayAndPrintSettings _settings;
        private Set _set;
        public string Filename { get; private set; }
        private string Title { get; set; }

        public ExportToHtml(Set set, DisplayAndPrintSettings settings)
        {
            _set = set;
            _settings = settings;
            Title = set.setName;
            Filename = set.setName + ".html";
        }

        public ExportToHtml(Song song, DisplayAndPrintSettings settings)
        {
            _set = new Set();
            _set.addSongToSet(song);
            _settings = settings;
            Title = song.generateShortTitle();
            Filename = song.generateShortTitle() + ".html";
        }

        public string GenerateHtml()
        {
         
            //configure the stylesheet
            string baseStylesheet = HtmlResources.stylesheet;
            configureStylesheet(ref baseStylesheet);

            //get song html
            StringBuilder songBuilder = new StringBuilder();
            foreach (var song in _set.songList)
            {       
                songBuilder.AppendFormat("<div class=\"DisplaySongName\">{0}</div>\r\n", song.generateLongTitle());
                var verses = song.getSongVerses();
                foreach (var verse in verses)
                {
                    var htmlString = getHtmlFromVerse(verse);
                    songBuilder.AppendLine(htmlString);
                }
            }

            //add stylesheet
            StringBuilder sb = new StringBuilder(HtmlResources.BaseSongHtml);
            sb.Replace("<%StyleSheet%>", baseStylesheet);
            //add song body
            sb.Replace("<%SongBody%>", songBuilder.ToString());
            //set the title
            sb.Replace("<%Title%>", this.Title);

            return sb.ToString();
        }

        private void configureStylesheet(ref string baseStylesheet)
        {
            StringBuilder sb = new StringBuilder(baseStylesheet);
            configureStylesheetFonts(sb, _settings.HeadingsFormat, "Heading");
            configureStylesheetFonts(sb, _settings.ChordFormat, "Chord");
            configureStylesheetFonts(sb, _settings.LyricsFormat, "Lyric");
            configureStylesheetFonts(sb, _settings.NoteFormat, "Note");
            configureStylesheetFonts(sb, _settings.TitleFormat, "Title");
            
            configureStylesheetBackgrounColors(sb);
         
            baseStylesheet = sb.ToString();
        }

        private void configureStylesheetBackgrounColors(StringBuilder sb)
        {
            sb.Replace("<%BackgroundColor%>", _settings.BackgroundColorHex);
            sb.Replace("<%VerseBorderColor%>", _settings.VerseBorderColorHex);
            sb.Replace("<%HeadingBackgroundColor%>", _settings.VerseHeadingBackgroundColorHex);
            sb.Replace("<%VerseBackgroundColor1%>", _settings.VerseLyricsBackgroundColor1Hex);
            sb.Replace("<%VerseBackgroundColor2%>", _settings.VerseLyricsBackgroundColor2Hex);
        
        }

        private void configureStylesheetFonts(StringBuilder sb, Entities.SongElementFormat format, string elementName)
        {
            sb.Replace(string.Format("<%{0}FontSize%>", elementName), format.FontSize.ToString());
            sb.Replace(string.Format("<%{0}Weight%>", elementName), format.FontStyle.ToString());
            sb.Replace(string.Format("<%{0}Font%>", elementName), format.FontName);
            sb.Replace(string.Format("<%{0}Color%>", elementName), format.FontColorHex);
        }

        private string trimLine(string line)
        {
            return line.TrimEnd().Replace(" ", "&nbsp;");
        }

        private string getHtmlFromVerse(SongVerse verse)
        {
            StringBuilder sb = new StringBuilder();
            //add heading line
            sb.AppendLine("<div class=\"DisplaySongVerse\">");
            sb.AppendFormat("<div class=\"DisplayLineVerseHeading\">{0}</div>\r\n", trimLine(verse.FullHeaderName));
            //add lyrics
            sb.AppendLine("<div class=\"DisplayLineVerseLyrics\">");
            for (int i = 0; i < verse.Lyrics.Count(); i++)
            {
                if (verse.IsChord[i] && (_settings.ShowChords ?? false))
                {
                    sb.AppendFormat("<p class=\"DisplayLineChord\">{0}</p>\r\n", trimLine(verse.Lyrics[i]));
                }
                else if (!verse.IsChord[i] && (_settings.ShowLyrics ?? false))
                {
                    sb.AppendFormat("<p class=\"DisplayLineLyrics\">{0}</p>\r\n", trimLine(verse.Lyrics[i]));
                }
            }
            sb.AppendLine("</div>");

            //addNotes
            if (_settings.ShowNotes ?? false)
            {
                sb.AppendLine("<div class=\"DisplayLineVerseNotes\">");
                sb.AppendLine(verse.Notes.Replace("\r\n", "<br/>").TrimEnd().Replace(" ", "&nbsp;"));
                sb.AppendLine("</div>");
            }
            sb.AppendLine("</div>");

            return sb.ToString();
        }




    }
}
