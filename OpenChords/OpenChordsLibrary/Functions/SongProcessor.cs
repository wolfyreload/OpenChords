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
		/// splits the lyrics into a verseHeader string[]
		/// and a verse contents string[]
		/// determine which lines are chords and which lines are lyrics
		/// </summary>
		/// <param name="song"></param>
		/// <param name="verseHeader"></param>
		/// <param name="verse"></param>
		public static void lyricsProcessor(string lyrics, out string[] verseHeader, out List<string[]> verseContents, out List<bool[]> isChord)
		{
			string[] splitParameters = {"["};
			string[] tempVerses = lyrics.Split(splitParameters,StringSplitOptions.RemoveEmptyEntries);
			
			verseContents = new List<string[]>();
			isChord = new List<bool[]>();
			
			
			List<string> tempVerseHeaders = new List<string>();
			string[] tempContents;
			
			foreach (string verse in tempVerses)
			{
				int endOfHeaderIndex = verse.IndexOf("]");
				if (endOfHeaderIndex >= 0)
				{
					string header = verse.Substring(0, endOfHeaderIndex);
					string content = verse.Substring(endOfHeaderIndex+1);
					
					tempVerseHeaders.Add(header);
					tempContents = multiLineToStringArray(content, true);
					
					List<bool> tempIsChord = new List<bool>();
					List<string> tempContent2 = new List<string>();
					for (int j = 0; j < tempContents.Length; j++)
					{
						if (tempContents[j].Length > 0)
						{
							string test = tempContents[j].Substring(0,1);
							if (test == ".")
								tempIsChord.Add(true);
							else
								tempIsChord.Add(false);
							
							tempContent2.Add(tempContents[j].Substring(1));
						}
					}
					isChord.Add(tempIsChord.ToArray());
					verseContents.Add(tempContent2.ToArray());
				}
				
			}
			
			verseHeader = tempVerseHeaders.ToArray();
		}
		
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
			string[] verseHeader;
			List<string[]> verseContents;
			List<bool[]> isChord;
			
			lyricsProcessor(song.lyrics, out verseHeader, out verseContents, out isChord);
			
			//transpose the chords
			for (int i = 0; i < verseContents.Count; i++)
			{
				string[] currentVerse = verseContents[i];
				for (int j = 0; j < currentVerse.Length; j++)
				{
					if (isChord[i][j])
                        currentVerse[j] = tranposeLine(currentVerse[j], song.PreferFlats);
				}
				
			}
			
			StringBuilder lyrics = new StringBuilder();
			//rewrite the lyrics
			for (int i = 0; i < verseHeader.Length; i++)
			{
				lyrics.AppendLine("[" + verseHeader[i] + "]");
				for (int j = 0; j < verseContents[i].Length; j++)
				{
					if (isChord[i][j])
						lyrics.AppendLine("." + verseContents[i][j].TrimEnd());
					else
                        lyrics.AppendLine(" " + verseContents[i][j].TrimEnd());
					
				}
				lyrics.AppendLine();
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
			string[] verseHeader;
			List<string[]> verseContents;
			List<bool[]> isChord;

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
			
			lyricsProcessor(song.lyrics, out verseHeader, out verseContents, out isChord);
			
			//fix headers
			for (int i = 0; i < verseHeader.Length; i++)
			{
				verseHeader[i] = verseHeader[i].ToUpper();

				//readd the removed blacket
				verseHeader[i] = "[" + verseHeader[i];
				
				//fix naming
				verseHeader[i] = verseHeader[i].Replace("VERSE", "V");
				verseHeader[i] = verseHeader[i].Replace("PRECHORUS", "P");
				verseHeader[i] = verseHeader[i].Replace("PRE-CHORUS", "P");
				verseHeader[i] = verseHeader[i].Replace("PRE CHORUS", "P");
				verseHeader[i] = verseHeader[i].Replace("CHORUS", "C");
				verseHeader[i] = verseHeader[i].Replace("BRIDGE", "B");
				verseHeader[i] = verseHeader[i].Replace("INTRO", "I");
				verseHeader[i] = verseHeader[i].Replace("INTERLUDE", "I");
				verseHeader[i] = verseHeader[i].Replace("TAG", "T");
				verseHeader[i] = verseHeader[i].Replace("ENDING", "E");
				verseHeader[i] = verseHeader[i].Replace("END", "E");
				verseHeader[i] = verseHeader[i].Replace(":", "");
				verseHeader[i] = verseHeader[i].Replace(" ", "");

				//insert the "]" if it is not present
				if (verseHeader[i].IndexOf(']') == -1)
				{
					verseHeader[i] = verseHeader[i].TrimEnd().TrimStart();
					verseHeader[i] = verseHeader[i] + "]";
				}
			}
			
			//fix chord and lyric indicators
			for (int i = 0; i < isChord.Count; i++)
			{
				for (int j = 0; j < isChord[i].Length; j++)
				{
                    var isChordLine = checkIfChordsLine(verseContents[i][j]);
                    isChord[i][j] = isChordLine;
				}
			}
			
			//buffer verses
			for (int i = 0; i < verseContents.Count; i++)
			{
				for (int j = 0; j < verseContents[i].Length; j++)
				{
					verseContents[i][j] = verseContents[i][j].TrimEnd();
					verseContents[i][j] = verseContents[i][j].PadRight(55);
				}
			}
			
			//rebuild the lyrics
			tempLyrics = new StringBuilder();
			for (int i = 0; i < verseHeader.Length; i++)
			{
				tempLyrics.AppendLine(verseHeader[i]);
				for (int j = 0; j < verseContents[i].Length; j++)
				{
					if (isChord[i][j])
                        tempLyrics.Append("." + verseContents[i][j] + "\n");
					else
                        tempLyrics.Append(" " + verseContents[i][j] + "\n");
				}
                tempLyrics.Append("\n");
			}
			
			//assign rebuilt lyrics back to the song
			song.lyrics = tempLyrics.ToString();

			//generate the note headers if none are present
			bool noteHeadersPresent = song.notes.Contains("[");
			if (!noteHeadersPresent)
			{
				string[] sep = {" "};
				string[] splitOrder = song.presentation.Split(sep, StringSplitOptions.RemoveEmptyEntries);
				StringBuilder tempNotes = new StringBuilder();
				foreach (string element in splitOrder)
				{
					tempNotes.AppendLine("[" + element + "]");
					tempNotes.AppendLine(" ");
					tempNotes.AppendLine("");
				}
				
				//add general notes header
				tempNotes.AppendLine("[G]");
				tempNotes.AppendLine(" ");
								
				song.notes = tempNotes.ToString() + song.notes;
			}
			
			//indent notes if needed
			string[] splitNotes = multiLineToStringArray(song.notes, true);
			StringBuilder tempNotes2 = new StringBuilder();
			for (int i = 0; i < splitNotes.Length; i++)
			{
				char char1 = splitNotes[i][0];
				if (char1 != '[' && char1 != ' ')
					splitNotes[i] = " " + splitNotes[i];


                tempNotes2.Append(splitNotes[i] + "\n");
			}
			
			//update the notes field in the song
			song.notes = tempNotes2.ToString();
			
			//get rid of trailing spaces
			song.title = song.title.TrimEnd();
			song.author = song.author.TrimEnd();
			song.key = song.key.TrimEnd();
			song.presentation = song.presentation;
			
			

			
		}

        private static bool checkIfChordsLine(string line)
        {
            var regexPattern = @"\s+([CDEFGAB])(#|##|b|bb)?(m|maj|maj7|min|m7|m9|2|7|9|11|13|sus4|sus2|5|aug)?/?\s+";
            
            var appearsToBeChords = Regex.IsMatch(" " + line + " ", regexPattern);
            var appearsToBeWords = Regex.IsMatch(" " + line + " ", @"\s+[a-zA-z]{5,20}\s+");

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

            splitLine(lyricLines, lyricRowIndex, columnIndex);
            if (isChordRowAboveCurrentRow)
                splitLine(lyricLines, chordRowIndex, columnIndex);
            
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
        private static void splitLine(List<string> lyricLines, int rowIndex, int columnIndex)
        {
            lyricLines[rowIndex]
                = lyricLines[rowIndex].PadRight(columnIndex+1);

            //create new lines
            String newLyricLine;
            bool isChordsRow = lyricLines[rowIndex].StartsWith(".");
            if (isChordsRow)
                newLyricLine = "." + lyricLines[rowIndex].Substring(columnIndex);
            else
                newLyricLine = " " + lyricLines[rowIndex].Substring(columnIndex);

            //delete the remaining text on old line
            lyricLines[rowIndex] = lyricLines[rowIndex].Remove(columnIndex);

            //insert the new lines
            if (isChordsRow)
                lyricLines.Insert(rowIndex + 2, newLyricLine);
            else
                lyricLines.Insert(rowIndex + 1, newLyricLine);
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
            var orderTemp = song.presentation + " G";

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
                    var newVerse = "[" + verse + "]\r\n.\r\n";
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
