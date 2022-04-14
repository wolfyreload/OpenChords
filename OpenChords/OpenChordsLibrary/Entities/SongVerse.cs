using OpenChords.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenChords.Entities
{
    public class SongVerse
    {
        public string HtmlId { get; set; }
        public string Header { get; set; }
        public string Notes { get; set; }
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
                return count;
            }
        }

        public SongVerse(string header, string lyrics, string notes, int index)
        {
            _SongElement(header, lyrics, notes, index);
        }

        public SongVerse()
        {
            _SongElement("", "", "", 0);
        }

        private void _SongElement(string header, string lyrics, string notes, int index)
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
            HtmlId = "SongElement" + index;
        }

        public SongVerse(string header, string lyrics)
        {
            _SongElement(header, lyrics, "", 0);
            
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
                oldHeader.Replace("I", "Intro ");
                oldHeader.Replace("B", "Bridge ");
                oldHeader.Replace("P", "Pre-Chorus ");
                oldHeader.Replace("S", "Solo ");
                oldHeader.Replace("T", "Tag ");
                return oldHeader.ToString();
            }
        }

        /// <summary>
        /// splits a song up into song elements
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public static List<SongVerse> getSongVerses(Song song, bool displayEachSongSectionOnce)
        {
            string[] splitParameters2 = { " " };
            string[] presentationOrder = song.presentation.Split(splitParameters2, StringSplitOptions.RemoveEmptyEntries);

            // Remove duplicate song sections (tipically choruses and interludes)
            if (displayEachSongSectionOnce)
            {
                presentationOrder = new HashSet<string>(presentationOrder).ToArray();
            }

            List<SongVerse> elementsInOrder = new List<SongVerse>();

            //split lyrics lines on pipe character
            var splitLyics = splitLyricsOnPipeCharacter(song.lyrics);

            //split the song lyrics up
            var newLyrics = splitLyricsIntoPieces(splitLyics);

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
                var newElement = new SongVerse(header, lyrics, notes, i+1);
                elementsInOrder.Add(newElement);   
            }

            return elementsInOrder;
        }

        public static List<SongVerse> getSongVersesNoOrder(Song song)
        {
            List<SongVerse> songElements = new List<SongVerse>();

            //split lyrics lines on pipe character
            var splitLyics = splitLyricsOnPipeCharacter(song.lyrics);

            //split the song lyrics up
            List<Tuple <string, string>> songSegments = splitLyricsIntoSegmentsNoOrder(splitLyics);

            //put all the elements in order of the song presentation
            foreach (Tuple<string,string> songSegment in songSegments)
            {
                var header = songSegment.Item1;
                var lyrics = songSegment.Item2;
                var newElement = new SongVerse(header, lyrics, "", songElements.Count() + 1);
                songElements.Add(newElement);
            }

            return songElements;
        }

        private static string splitLyricsOnPipeCharacter(string lyrics)
        {
            //replace the double pipe characters so we dont split on them
            lyrics = lyrics.Replace("||", "###DOUBLEPIPE###");

            //iterate through all the pipe characters and split the lyrics lines on them
            int pipeIndex = -1;
            do
            {
                pipeIndex = lyrics.IndexOf("|");
                if (pipeIndex > 0)
                {
                    lyrics = lyrics.Remove(pipeIndex, 1).Insert(pipeIndex, " "); //remove the pipe
                    SongProcessor.BreakSongLine(ref lyrics, pipeIndex+1); //break song line in pipe position
                }
            } while (pipeIndex > 0);

            //restore the double pipes
            lyrics = lyrics.Replace("###DOUBLEPIPE###", "||");

            return lyrics;
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

        private static List<Tuple<string, string>> splitLyricsIntoSegmentsNoOrder(string lyrics)
        {
            string[] splitParameters = { "[" };
            string[] tempVerses = lyrics.Split(splitParameters, StringSplitOptions.RemoveEmptyEntries);
            List<Tuple<string, string>> newLyrics = new List<Tuple<string, string>>();
            for (int i = 0; i < tempVerses.Count(); i++)
            {
                var verse = tempVerses[i];

                int endOfHeaderIndex = verse.IndexOf("]");
                if (endOfHeaderIndex >= 0)
                {
                    string header = verse.Substring(0, endOfHeaderIndex);
                    verse = verse.Substring(endOfHeaderIndex + 1);
                    newLyrics.Add(new Tuple<string, string>(header, verse));
                }
            }

            return newLyrics;
        }

        private static Dictionary<string, string> splitLyricsIntoPieces(string lyrics)
        {
            List<Tuple<string, string>> tuppleLyrics = splitLyricsIntoSegmentsNoOrder(lyrics);
            Dictionary<string, string> lyricsDictionary = new Dictionary<string, string>();
            for (int i = 0; i < tuppleLyrics.Count(); i++)
            {
                var header = tuppleLyrics[i].Item1;
                var verse = tuppleLyrics[i].Item2;
                
                //ensure that there is only one of every chord piece
                if (!lyricsDictionary.ContainsKey(header))
                    lyricsDictionary[header] = verse;
            }
            return lyricsDictionary;
        }




    }
}
