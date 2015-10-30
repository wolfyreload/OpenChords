/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/12
 * Time: 11:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OpenChords.Entities;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace OpenChords.Functions
{
	/// <summary>
	/// Description of SongProcessor.
	/// </summary>
	public static class SongProcessor
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		/// <summary>
		/// convert a multiline string into a string array
		/// one line per element in the array
		/// </summary>
		/// <param name="multiLineString"></param>
		/// <returns></returns>
		public static string[] multiLineToStringArray(string multiLineString, bool removeEmptyEntries)
		{
            if (multiLineString == null)
                return new string[0];

			multiLineString = multiLineString.Replace("\r", "");
			
			string[] splitParameters = { "\n" };

            string[] split;
            if (removeEmptyEntries)
			    split = multiLineString.Split(splitParameters,StringSplitOptions.RemoveEmptyEntries);
            else
                split = multiLineString.Split(splitParameters, StringSplitOptions.None);
			return split;
		}
		
		/// <summary>
		/// convert the verse headers into actual song section names
		/// </summary>
		/// <param name="oldHeader"></param>
		/// <returns></returns>
		public static string verseHeaderConvertion(string oldHeader)
		{
			oldHeader = oldHeader.Replace("V", "Verse ");
			oldHeader = oldHeader.Replace("C", "Chorus ");
			oldHeader = oldHeader.Replace("E", "Ending ");
			oldHeader = oldHeader.Replace("I", "Interlude ");
			oldHeader = oldHeader.Replace("B", "Bridge ");
			oldHeader = oldHeader.Replace("P", "Pre-Chorus ");
			
			return oldHeader;
		}
		
		/// <summary>
		/// generate the filename
		/// </summary>
		/// <param name="song"></param>
		/// <returns></returns>
		public static string generateFileName(Song song)
		{
            return song.generateShortTitle();
		}

        public static string generateSongTitleInPresentation(Song song)
        {
            return song.generateLongTitle();
        }
		
		/// <summary>
		/// transpose the entire song up by a semitone
		/// </summary>
		/// <param name="song"></param>
		/// <returns></returns>
		public static void transposeKeyUp(Song song)
		{
	        var songVerses = SongVerse.getSongVersesNoOrder(song);

            //transpose each chord line
            foreach (var songVerse in songVerses)
            {
                for (int i = 0; i < songVerse.VerseLinesCount; i++)
                {
                    var isChord = songVerse.IsChord[i];
                    if (isChord)
                        songVerse.Lyrics[i] = tranposeLine(songVerse.Lyrics[i], song.PreferFlats);
                }
            }

            //rewrite the lyrics
            StringBuilder lyrics = new StringBuilder();
            foreach (var songVerse in songVerses)
            {
                lyrics.AppendFormat("[{0}]\n", songVerse.Header);
                for (int i = 0; i < songVerse.VerseLinesCount; i++)
                {
                    var isChord = songVerse.IsChord[i];
                    if (isChord)
                        lyrics.AppendFormat(".{0}\n", songVerse.Lyrics[i].TrimEnd());
                    else
                        lyrics.AppendFormat(" {0}\n", songVerse.Lyrics[i].TrimEnd());
                }
                lyrics.Append("\n");
            }
			song.lyrics = lyrics.ToString();
		}
		
		/// <summary>
		/// transposes the key of a single line of the song up by semi-tone
		/// </summary>
		/// <param name="line">this is a chord line of one of the lines of the song</param>
		/// <returns></returns>
		private static String tranposeLine(String line, bool preferFlats)
		{
			Char[] Seperator = { ' ' };
			String[] temp = line.Split(Seperator, StringSplitOptions.None);

			StringBuilder buildChord = new StringBuilder();
			StringBuilder buildLine = new StringBuilder();
			String chord;

			if (temp.Length > 0)
			{
				//start building first chord
				buildChord.Append(temp[0]);

				for (int i = 1; i < temp.Length; i++)
				{
					if (temp[i].Length == 0)
					{
						//continue building current chord
						buildChord.Append(" ");
					}
					else
					{
						//transpose the chord
						chord = buildChord.ToString();
                        chord = transposeChord(chord, preferFlats);
						
						//add the transposed chord to the line
						buildLine.Append(chord);
						
						//reset the chord to blank
						buildChord = new StringBuilder();
						
						//start building next chord
						buildChord.Append(temp[i]);
					}

				}
				//add the final chord
				chord = buildChord.ToString();
                chord = transposeChord(chord, preferFlats);
				buildLine.Append(chord);
			}

            return buildLine.ToString();

		}
		
		/// <summary>
		/// transposes the key of a single chord of the song up by semi-tone
		/// </summary>
		/// <param name="line">this is a chord in one of the lines of the song</param>
		/// <returns></returns>
		public static string transposeChord(string chord, bool preferFlats)
		{
			int size = chord.Length;

            //fix flats breaking
            String[] fromFlatsArray = { 
                "Bb",
				"Ab",
				"Gb",
				"Eb",
				"Db"};

            String[] toFlatsArray = {   
				"A#",
				"G#",
				"F#",
				"D#",
				"C#"};

			String[] fromSharpsArray = { "A#",
				"A",
				"G#",
				"G",
				"F#",
				"F",
				"E",
				"D#",
				"D",
				"C#",
				"C",
				"B",
				"*#*"};

			String[] toSharpsArray = {   "*#*",
				"A#",
				"A",
				"G#",
				"G",
				"F#",
				"F",
				"E",
				"D#",
				"D",
				"C#",
				"C",
				"B"};
            
            //if we have flats get rid of them
            if (chord.Contains("b"))
                for (int i = 0; i < fromFlatsArray.Length; i++)
                    chord = chord.Replace(fromFlatsArray[i], toFlatsArray[i]);
            
            

                for (int i = 0; i < fromSharpsArray.Length; i++)
                    chord = chord.Replace(fromSharpsArray[i], toSharpsArray[i]);
            
            //if we prefer flats
            if (preferFlats)
                for (int i = 0; i < fromFlatsArray.Length; i++)
                    chord = chord.Replace(toFlatsArray[i], fromFlatsArray[i]);
			

			//make sure that there are the same number
			//of characters in the string before and after
			//the transposing
			chord = chord.TrimEnd();
			//needed coz of the split operation
			chord = chord.PadRight(size+1);

			return chord;
		}
		
		public static string orderFormat(string order)
		{
			string[] sep = {" "};
			string[] orderlist = order.Split(sep,StringSplitOptions.RemoveEmptyEntries);
			StringBuilder list = new StringBuilder();
			foreach (string element in orderlist)
				list.Append(element.PadRight(3) + " ");
			
			return list.ToString();
		}
		


		/// <summary>
		/// attempt to fix the formatting of the song
		/// </summary>
		/// <param name="song"></param>
		public static void fixFormatting(Song song)
        {
            //check if one or more lines has lines that dont start with [, ' ' or .
            rebuildLyricsIfNeeded(song);

            //get song in split form
            var verses = SongVerse.getSongVersesNoOrder(song);

            //fix headers, chord and lyric indicators and rebuild the lyrics string
            var tempLyrics = new StringBuilder();
            for (int i = 0; i < verses.Count; i++)
            {
                var verse = verses[i];
                verse.Header = fixHeader(verse.Header);
                tempLyrics.AppendFormat("[{0}]\n", verse.Header);
                for (int j = 0; j < verse.VerseLinesCount; j++)
                {
                    verse.IsChord[j] = CheckIfChordsLine(verse.Lyrics[j]);
                    if (verse.IsChord[j])
                        tempLyrics.AppendFormat(".{0}\n", verse.Lyrics[j].TrimEnd());
                    else
                        tempLyrics.AppendFormat(" {0}\n", verse.Lyrics[j].TrimEnd());
                }
                tempLyrics.Append("\n");
                replaceMsWordQuotes(tempLyrics);

            }
            song.lyrics = tempLyrics.ToString().Trim();

            //format the notes
            song.notes = formatNotes(song);

            //get rid of trailing spaces
            song.title = song.title.TrimEnd();
            song.author = song.author.TrimEnd();
            song.key = song.key.TrimEnd();
        }

        //replace msword quotes as they don't render properly
        private static void replaceMsWordQuotes(StringBuilder tempLyrics)
        {
            tempLyrics.Replace("’", "'");
            tempLyrics.Replace("‘", "'"); 
            tempLyrics.Replace("“", "\""); 
            tempLyrics.Replace("”", "\""); 
        }

        private static string formatNotes(Song song)
        {
            StringBuilder tempNotes = new StringBuilder();
            bool noteHeadersPresent = song.notes.Contains("[");
            if (noteHeadersPresent) return fixNoteIndenting(song.notes);

            string[] sep = { " " };
            string[] splitOrder = song.presentation.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            foreach (string element in splitOrder)
            {
                tempNotes.AppendFormat("[{0}]\n", element);
                tempNotes.Append(" \n");
                tempNotes.Append("\n");
            }
            if (!string.IsNullOrWhiteSpace(song.notes))
            {
                tempNotes.AppendFormat("[O]\n{0}", song.notes);
                return fixNoteIndenting(tempNotes.ToString());
            }
            else
                return tempNotes.ToString();
        }

        private static string fixNoteIndenting(string notes)
        {
            //indent notes if needed
            string[] splitNotes = multiLineToStringArray(notes, true);
            StringBuilder tempNotes2 = new StringBuilder();
            for (int i = 0; i < splitNotes.Length; i++)
            {
                char char1 = splitNotes[i][0];
                if (char1 != '[' && char1 != ' ')
                    splitNotes[i] = " " + splitNotes[i];
                tempNotes2.Append(splitNotes[i] + "\n");
            }
            return tempNotes2.ToString();
        }

        private static string fixHeader(string header)
        {
            header = header.ToUpper();

            //fix naming
            header = header.Replace(" ", "");
            header = header.Replace(":", "");
            header = header.Replace("-", "");
            header = header.Replace("VERSE", "V");
            header = header.Replace("PRECHORUS", "P");
            header = header.Replace("CHORUS", "C");
            header = header.Replace("BRIDGE", "B");
            header = header.Replace("INTRO", "I");
            header = header.Replace("INTERLUDE", "I");
            header = header.Replace("TAG", "T");
            header = header.Replace("ENDING", "E");
            header = header.Replace("END", "E");
            return header;
        }

        private static StringBuilder rebuildLyricsIfNeeded(Song song)
        {
            song.lyrics = song.lyrics.Replace("\t", "   ");

            string[] splitLyrics = multiLineToStringArray(song.lyrics, true);
            bool rebuildNeeded = false;
            for (int i = 0; i < splitLyrics.Length; i++)
            {
                //check for unexpected characters
                if (!splitLyrics[i].StartsWith(".") && !splitLyrics[i].StartsWith(" ") && !splitLyrics[i].StartsWith("["))
                {
                    rebuildNeeded = true;
                    break;
                }
            }

            StringBuilder tempLyrics = new StringBuilder();
            if (rebuildNeeded)
            {
                for (int i = 0; i < splitLyrics.Length; i++)
                {
                    if (!splitLyrics[i].StartsWith(".") && !splitLyrics[i].StartsWith(" ") && !splitLyrics[i].StartsWith("["))
                        tempLyrics.AppendLine(" " + splitLyrics[i]);
                    else
                        tempLyrics.AppendLine(splitLyrics[i]);
                }
                song.lyrics = tempLyrics.ToString();
            }

            return tempLyrics;
        }

        public static bool CheckIfChordsLine(string line)
        {
            var regexPattern = @"\b([CDEFGAB])(#|##|b|bb)?(m|maj|maj7|maj9|maj11|maj13|min|m7|m9|m11|m13|m6|madd9|m6add9|mmaj7|mmaj9|m7b5|7sus4|add9|6add9|dim|dim7|2|7|9|11|13|sus4|sus2|5|aug)?/?\b";
            
            var appearsToBeChords = Regex.IsMatch(string.Format(" {0} ", line), regexPattern);
            var appearsToBeWords = Regex.IsMatch(string.Format(" {0} ", line), @"\s+[a-zA-z]{5,20}\s+");

            if (appearsToBeWords) 
                return false;
            else
                return appearsToBeChords;
  
        }

		/// <summary>
		/// split a lyrics line in two while preserving the 
		/// positions of the chords in the line above
		/// returns a new caret position
		/// </summary>
		/// <param name="splitPosition"></param>
		public static int BreakSongLine(ref string lyrics, int splitPosition)
        {
            //split each line of the song into an array entry
            //use a list instead
            List<String> lyricLines =
                new List<string>(multiLineToStringArray(lyrics, false));

            //calculate the row and col of the caret position of the split
            int rowIndex, columnIndex;
            getRowAndColumnIndexOfSplitPosition(splitPosition, lyricLines, out rowIndex, out columnIndex);

            //do nothing if cant split 
            if (rowIndex <= 0 || columnIndex <= 0) return splitPosition;

            //do nothing if verse line
            if (lyricLines[rowIndex].StartsWith("[")) return splitPosition;

            //do nothing if chord line
            if (lyricLines[rowIndex].StartsWith(".")) return splitPosition;


            int chordRowIndex = rowIndex - 1;
            int lyricRowIndex = rowIndex;
            bool isChordRowAboveCurrentRow = lyricLines[chordRowIndex].StartsWith(".");

            splitLyricLine(lyricLines, lyricRowIndex, columnIndex);
            if (isChordRowAboveCurrentRow)
                splitChordLine(lyricLines, chordRowIndex, columnIndex);
            
            //put all the lyrics together again
            StringBuilder buildLyrics = new StringBuilder();
            for (int i = 0; i < lyricLines.Count; i++)
            {
                buildLyrics.Append(lyricLines[i] + "\n");
            }
            lyrics = buildLyrics.ToString();

            //determine the new caret position
            int pos = getNewCarrotPosition(lyricLines, rowIndex, isChordRowAboveCurrentRow);

            return pos;
        }

        private static int getNewCarrotPosition(List<string> lyricLines, int rowIndex, bool isChordRowAboveCurrentRow)
        {
            int pos = 0;
            int row = 0;
            if (isChordRowAboveCurrentRow)
                row = rowIndex + 2;
            else
                row = rowIndex + 1;
            for (int i = 0; i < row; i++)
            {
                pos += lyricLines[i].Length + 1;
            }
            pos += 1; //move over to the next line
            return pos;
        }

        /// <summary>
        /// split lyricsLines at rowIndex and split at columnIndex
        /// </summary>
        /// <param name="lyricLines"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        private static void splitLyricLine(List<string> lyricLines, int rowIndex, int columnIndex)
        {
            //buffer the row encase its too narrow
            lyricLines[rowIndex] = lyricLines[rowIndex].PadRight(columnIndex + 1);

            //create new lines
            String newLyricLine = " " + lyricLines[rowIndex].Substring(columnIndex);

            //delete the remaining text on old line
            lyricLines[rowIndex] = lyricLines[rowIndex].Remove(columnIndex);

            //insert the new lines
            lyricLines.Insert(rowIndex + 1, newLyricLine);
        }

        /// <summary>
        /// split chord line at rowIndex and split at column index
        /// </summary>
        /// <param name="lyricLines"></param>
        /// <param name="chordRowIndex"></param>
        /// <param name="columnIndex"></param>
        private static void splitChordLine(List<string> lyricLines, int rowIndex, int columnIndex)
        {
            var currentLine = lyricLines[rowIndex];

            //buffer the row encase its too narrow
            currentLine = currentLine.PadRight(columnIndex + 1);

            //move column index if slicing chord in half
            columnIndex = moveIndexUntilNotSlicingChordInHalf(ref currentLine, columnIndex);

            //create new lines
            String newLyricLine = "." + currentLine.Substring(columnIndex);
     
            //delete the remaining text on old line
            lyricLines[rowIndex] = currentLine.Remove(columnIndex);

            //insert the new lines
            lyricLines.Insert(rowIndex + 2, newLyricLine);
        }

        private static int moveIndexUntilNotSlicingChordInHalf(ref string currentLine, int columnIndex)
        {
            int bufferSize = 0;
            while (columnIndex < currentLine.Length && currentLine[columnIndex] != ' ')
            {
                columnIndex++;
                bufferSize++;
            }
            //buffer the line with the number of extra characters moved so we dont move the chord that follows
            currentLine = currentLine.Insert(columnIndex, "".PadRight(bufferSize));
            return columnIndex;
        }

        private static int getRowAndColumnIndexOfSplitPosition(int splitPosition, List<string> lyricLines, out int rowIndex, out int columnIndex)
        {
            int lineLength = -1;
            rowIndex = -1;
            columnIndex = -1;
            for (int i = 0; i < lyricLines.Count; i++)
            {
                lineLength = lyricLines[i].Length + 1; //cater for the \n
                if (splitPosition - lineLength > 0)
                {
                    splitPosition -= lineLength;
                }
                else
                {
                    rowIndex = i;
                    columnIndex = splitPosition;
                    break;
                }
            }

            return splitPosition;
        }


        /// <summary>
        /// makes the song lyrics the same order as the order of the song
        /// </summary>
        /// <param name="song"></param>
        public static void fixNoteOrdering(Song song)
        {
            //add a General Note tab
            var orderTemp = song.presentation;

            //split all the notes into verses
            var verses = new List<string>(song.notes.Split(new char[] { '[' }, StringSplitOptions.RemoveEmptyEntries));

            //the new list in which to move all the order info
            var newVerses = new List<string>();

            //get the song order
            var order = new List<string>(orderTemp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            //fix the ordering of the notes
            foreach (string verse in order)
            {
                var matchFound = false;
                for (int i = 0; i < verses.Count; i++)
                {
                    var verseLine = verses[i];
                    //remove matching verse and add it to the new verses list
                    if (verseLine.Contains(verse + "]"))
                    {
                        int index = verses.IndexOf(verseLine);
                        var newVerse = '[' + verses[index];
                        verses.RemoveAt(index);
                        newVerses.Add(newVerse);
                        matchFound = true;
                        break;
                    }
                }
                //if there is no matching verse add one
                if (!matchFound)
                {
                    var newVerse = "[" + verse + "]\r\n \r\n";
                    newVerses.Add(newVerse);
                }
            }

            //add everything thats left
            foreach (string verse in verses)
            {
                if (verse.Contains("]"))
                {
                    var newVerse = '[' + verse;
                    var notePart = newVerse.Substring(newVerse.IndexOf(']'));
                    if (notePart.Trim(new char[] { ' ', '\r', '\n' }).Length >= 2)
                        newVerses.Add(newVerse);
                }
            }

            //fix the original notes in Song
            var result = new StringBuilder();
            foreach (string verse in newVerses)
                result.Append(verse);
            song.notes = result.ToString();


            return;
        }

        public static void fixLyricsOrdering(Song song)
        {
            //split all the notes into verses
            var verses = new List<string>(song.lyrics.Split(new char[] { '[' }, StringSplitOptions.RemoveEmptyEntries));

            //the new list in which to move all the order info
            var newVerses = new List<string>();

            //get the song order
            var order = new List<string>(song.presentation.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            //get a distinct list of the order
            order = order.Distinct<string>().ToList();

            //fix the ordering of the notes
            foreach (string verse in order)
            {
                var matchFound = false;
                for (int i = 0; i < verses.Count; i++)
                {
                    var verseLine = verses[i];
                    //remove matching verse and add it to the new verses list
                    if (verseLine.Contains(verse + "]"))
                    {
                        int index = verses.IndexOf(verseLine);
                        var newVerse = '[' + verses[index];
                        verses.RemoveAt(index);
                        newVerses.Add(newVerse);
                        matchFound = true;
                        break;
                    }
                }
               
                //if there is no matching verse add one
                if (!matchFound)
                {
                    var newVerse = "[" + verse + "]\n.\n";
                    newVerses.Add(newVerse);
                }
            }

            //add everything thats left
            foreach (string verse in verses)
            {
                if (verse.Contains("]"))
                {
                    var newVerse = '[' + verse;
                    var notePart = newVerse.Substring(newVerse.IndexOf(']'));
                    if (notePart.Trim(new char[] { ' ', '\r', '\n' }).Length > 3)
                        newVerses.Add(newVerse);
                }
            }

            //fix the original lyrics in Song
            var result = new StringBuilder();
            foreach (string verse in newVerses)
                result.Append(verse);
            song.lyrics = result.ToString();


            return;
        }
    }
}
