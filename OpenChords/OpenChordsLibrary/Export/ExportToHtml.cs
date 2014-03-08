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

        public ExportToHtml(Set set, DisplayAndPrintSettings settings)
        {
            _set = set;
            _settings = settings;
        }

        public ExportToHtml(Song song, DisplayAndPrintSettings settings)
        {
            _set = new Set();
            _set.addSongToSet(song);
            _settings = settings;
        }


        public List<SongHtml> GenerateHtml()
        {
            StringBuilder sb = new StringBuilder();

            List<SongHtml> htmlSongs = new List<SongHtml>();
            foreach (var song in _set.songList)
            {
                SongHtml html = new SongHtml();

                html.Name = song.generateLongTitle();
                html.Order = song.presentation;
                var verses = song.getSongVerses();
                foreach (var verse in verses)
                {
                    var htmlString = getHtmlFromVerse(verse);
                    sb.AppendLine(htmlString);
                }
                html.Html = sb.ToString();
                htmlSongs.Add(html);
                sb.Length = 0;
            }
            return htmlSongs;
        }

        private string trimLine(string line)
        {
            return line.TrimEnd().Replace(" ", "&nbsp;");
        }

        public string getHtmlFromVerse(SongVerse verse)
        {
            StringBuilder sb = new StringBuilder();
            //add heading line
            sb.AppendLine("<div class=\"DisplaySongVerse\">");
            sb.AppendFormat("<div class=\"DisplayLineVerseHeading\">{0}</div>\r\n", trimLine(verse.FullHeaderName));
            //add lyrics
            sb.AppendLine("<div class=\"DisplayLineVerseLyrics\">");
            for (int i=0; i<verse.Lyrics.Count(); i++)
            {
                if (verse.IsChord[i])
                {
                    sb.AppendFormat("<p class=\"DisplayLineChord\">{0}</p>\r\n", trimLine(verse.Lyrics[i]));
                }
                else
                {
                    sb.AppendFormat("<p class=\"DisplayLineLyrics\">{0}</p>\r\n", trimLine(verse.Lyrics[i]));
                }
            }
            sb.AppendLine("</div>");

            //addNotes
            sb.AppendLine("<div class=\"DisplayLineVerseNotes\">");
            sb.AppendLine(verse.Notes.Replace("\r\n", "<br/>").TrimEnd().Replace(" ", "&nbsp;"));
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            return sb.ToString();
        }




    }
}
