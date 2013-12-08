using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenChords.Entities
{
    public class SongVerse
    {
        public string Header { get; private set; }
        public string Notes { get; private set; }
        public List<string> Lyrics { get; private set; }
        public List<bool> IsChord { get; private set; }
        public bool isElementDisplayed { get; set; }

        /// <summary>
        /// count number of lines are chords
        /// </summary>
        public int ChordLinesCount
        {
            get
            {
                var count = 0;
                foreach (bool line in IsChord)
                    if (line == true)
                        count++;
                return count + 1;
            }
        }

        /// <summary>
        /// count number of lines are lyrics
        /// </summary>
        public int LyricsLinesCount
        {
            get
            {
                var count = 0;
                foreach (bool line in IsChord)
                    if (line != true)
                        count++;
                return count + 1;
            }
        }

        /// <summary>
        /// count all verse lines (both lyrics and chord lines)
        /// </summary>
        public int VerseLinesCount
        {
            get
            {
                var count = Lyrics.Count();
                return count + 1;
            }
        }

        public SongVerse(string header, string lyrics, string notes)
        {
            _SongElement(header, lyrics, notes);
        }

        private void _SongElement(string header, string lyrics, string notes)
        {
            Header = header;
            var split = lyrics.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var newLyrics = new List<string>();
            var newIsChord = new List<bool>();

            //determine if line is lyrics or chords
            foreach (var line in split)
            {
                if (line.Length > 0 && line.StartsWith("."))
                {
                    newIsChord.Add(true);
                    newLyrics.Add(line.Substring(1));
                }
                else if (line.Length > 0)
                {
                    newIsChord.Add(false);
                    newLyrics.Add(line.Substring(1));
                }
            }

            Lyrics = newLyrics;
            IsChord = newIsChord;
            Notes = notes;
        }

        public SongVerse(string header, string lyrics)
        {
            _SongElement(header, lyrics, "");
            
        }

        /// <summary>
        /// convert the verse headers into actual song section names
        /// </summary>
        /// <param name="oldHeader"></param>
        /// <returns></returns>
        public string FullHeaderName
        {
            get
            {
            StringBuilder oldHeader = new StringBuilder(Header);
            oldHeader.Replace("V", "Verse ");
            oldHeader.Replace("C", "Chorus ");
            oldHeader.Replace("E", "Ending ");
            oldHeader.Replace("I", "Interlude ");
            oldHeader.Replace("B", "Bridge ");
            oldHeader.Replace("P", "Pre-Chorus ");
            return oldHeader.ToString();
            }
        }

        /// <summary>
        /// splits a song up into song elements
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public static List<SongVerse> getSongVerses(Song song)
        {
            string[] splitParameters2 = { " " };
            string[] presentationOrder = song.presentation.Split(splitParameters2, StringSplitOptions.RemoveEmptyEntries);
            List<SongVerse> elementsInOrder = new List<SongVerse>();
            
            //split the song lyrics up
            var newLyrics = splitLyricsIntoPieces(song.lyrics);

            //split the notes up
            var newNotes = splitNotesIntoPieces(song.notes);

            //put all the elements in order of the song presentation
            for (int i = 0; i < presentationOrder.Count(); i ++)
            {
                var header = presentationOrder[i];
                //if the notes dont match up add blanks if needed
                if (newNotes.Count() - 1 < i)
                    newNotes.Add("");

                //if lyrics dont match up... make them
                if (!newLyrics.ContainsKey(header))
                    newLyrics[header] = "";

                var lyrics = newLyrics[header];
                var notes = newNotes[i];
                var newElement = new SongVerse(header, lyrics, notes);
                elementsInOrder.Add(newElement);   
            }

            return elementsInOrder;
        }

        private static List<string> splitNotesIntoPieces(string notes)
        {
            string[] splitParameters = { "[" };
            string[] tempNotes = notes.Split(splitParameters, StringSplitOptions.RemoveEmptyEntries);
            List<string> newNotes = new List<string>();
            for (int i = 0; i < tempNotes.Count(); i++)
            {
                var note = cleanNotes(tempNotes[i]);

                int endOfHeaderIndex = note.IndexOf("]");
                if (endOfHeaderIndex >= 0)
                {
                    string header = note.Substring(0, endOfHeaderIndex);
                    newNotes.Add(note.Substring(endOfHeaderIndex + 1));
                }
            }

            return newNotes;
        }

        /// <summary>
        /// try cleanup the notes
        /// </summary>
        /// <param name="tempNotes"></param>
        /// <returns></returns>
        private static string cleanNotes(string tempNotes)
        {
            //Regex regexRemoveExcessiveSpaces = new Regex(@"(\s{2,200})");

            //tempNotes = regexRemoveExcessiveSpaces.Replace(tempNotes, " ");
            var split = tempNotes.Split(new char[] { '\n' }).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var s in split) sb.AppendLine(s.Trim());
            sb.Replace("]\r\n", "]");
            var result = sb.ToString();
            return result;
        }

        private static Dictionary<string, string> splitLyricsIntoPieces(string lyrics)
        {
            string[] splitParameters = { "[" };
            string[] tempVerses = lyrics.Split(splitParameters, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> newLyrics = new Dictionary<string, string>();
            for (int i = 0; i < tempVerses.Count(); i++)
            {
                var verse = tempVerses[i];

                int endOfHeaderIndex = verse.IndexOf("]");
                if (endOfHeaderIndex >= 0)
                {
                    string header = verse.Substring(0, endOfHeaderIndex);
                    //ensure that there is only one of every chord piece
                    if (!newLyrics.ContainsKey(header))
                        newLyrics[header] = verse.Substring(endOfHeaderIndex + 1);
                }
            }

            return newLyrics;
        }




    }
}
