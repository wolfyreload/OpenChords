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
			StringBuilder songTitleLine = new StringBuilder(song.title + " ");

			if (song.key != "")
			{
				songTitleLine.Append("(Key - " + song.key);
				
				if (song.Capo != 0)
				{
					songTitleLine.Append(" Capo - " + song.Capo);
				}

				songTitleLine.Append(")");
			}
			
			return songTitleLine.ToString();
		}

        public static string generateSongTitleInPresentation(Song song)
        {
            StringBuilder songTitleLine = new StringBuilder(song.title + " ");

            //fill in key and capo
            if (!string.IsNullOrEmpty(song.key))
            {
                songTitleLine.Append("(Key - " + song.key);

                if (song.Capo != 0)
                {
                    songTitleLine.Append(" Capo - " + song.Capo);
                }

                songTitleLine.Append(")");
            }
            if (!string.IsNullOrEmpty(song.time_sig))
                songTitleLine.Append(" " + song.time_sig);
            if (!string.IsNullOrEmpty(song.tempo))
                songTitleLine.Append(" " + song.tempo);

            return songTitleLine.ToString();
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
						lyrics.AppendLine("." + verseContents[i][j]);
					else
						lyrics.AppendLine(" " + verseContents[i][j]);
					
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

			return buildLine.ToString(); ;

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
					if (j % 2 == 0) //chord line
						if (!isChord[i][j])
							isChord[i][j] = true;
					if (j % 2 == 1) //lyrics line
						if (isChord[i][j])
							isChord[i][j] = false;
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
			song.presentation = song.presentation.TrimEnd();
			
			

			
		}


        /// <summary>
        /// split a lyrics line in two while preserving the 
        /// positions of the chords in the line above
        /// returns a new caret position
        /// </summary>
        /// <param name="splitPosition"></param>
        public static int BreakSongLine(RichTextBox richTextBox, int splitPosition)
        {
            int PADDING = 60;
            try
            {
                //get the 2 lines that need to be split
                int lyricsRowIndex = richTextBox.GetLineFromCharIndex(splitPosition);
                int chordsRowIndex = lyricsRowIndex - 1;
                string lyricsRow = richTextBox.Lines[lyricsRowIndex];
                string chordsRow = richTextBox.Lines[chordsRowIndex];


                if (lyricsRow.Length == 0 || lyricsRow[0] != ' ')
                    return splitPosition; //return if lyrics line is not selected

                //get the position of the split
                var y = splitPosition - (richTextBox.GetFirstCharIndexFromLine(chordsRowIndex) + chordsRow.Length);

                //construct all the pieces
                var newLyricsRow = " " + lyricsRow.Substring(y - 1).PadRight(PADDING);
                var newChordsRow = "." + chordsRow.Substring(y - 1).PadRight(PADDING);
                var oldLyricsRow = " " + lyricsRow.Substring(1, y - 2).PadRight(PADDING);
                var oldChordsRow = "." + chordsRow.Substring(1, y - 2).PadRight(PADDING);

                //construct the output string
                string outputSegment = oldChordsRow + "\n" + oldLyricsRow + "\n" + newChordsRow + "\n" + newLyricsRow;
 
                //rebuild richtextbox
                var start = richTextBox.GetFirstCharIndexFromLine(chordsRowIndex);
                var end = richTextBox.GetFirstCharIndexFromLine(lyricsRowIndex + 1) - 1;
                richTextBox.Select(start, end - start);
                richTextBox.SelectedText = outputSegment;
                
                //fix carot position
                var endOfSelection = richTextBox.GetLineFromCharIndex(richTextBox.SelectionStart + richTextBox.SelectionLength);
                splitPosition = richTextBox.GetFirstCharIndexFromLine(endOfSelection) + 1;

            }
            catch (Exception ex)
            {
                logger.Error("Error breaking song line", ex);
                return splitPosition;
            }

            return splitPosition;


        }


		/// <summary>
		/// split a lyrics line in two while preserving the 
		/// positions of the chords in the line above
		/// returns a new caret position
		/// </summary>
		/// <param name="splitPosition"></param>
		public static int BreakSongLine(Song song, int splitPosition)
        {
            //split each line of the song into an array entry
            //use a list instead
            List<String> lyricLines =
                new List<string>(multiLineToStringArray(song.lyrics, true));
            
            int lineLength = -1;

            int rowIndex = -1;
            int columnIndex = -1;

            //calculate the row and col of the caret position of the split
            for (int i = 0; i < lyricLines.Count; i++)
            {
                lineLength = lyricLines[i].Length + 2; //cater for the /r/n
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

            if (rowIndex > 0 && columnIndex > 0)
            {
                int chordRowIndex = rowIndex - 1;
                int lyricRowIndex = rowIndex;

                int chordRowLength = lyricLines[chordRowIndex].Length;
                int lyricRowLength = lyricLines[lyricRowIndex].Length;

                //pad the rows
                lyricLines[chordRowIndex]
                    = lyricLines[chordRowIndex].PadRight(lyricRowLength);
                lyricLines[lyricRowIndex]
                    = lyricLines[lyricRowIndex].PadRight(chordRowLength);

                //create new lines
                String newChordLine
                    = "." + lyricLines[chordRowIndex].Substring(columnIndex);
                String newLyricLine
                    = " " + lyricLines[lyricRowIndex].Substring(columnIndex);

                //delete the remaining text
                lyricLines[chordRowIndex] = lyricLines[chordRowIndex].Remove(columnIndex);
                lyricLines[lyricRowIndex] = lyricLines[lyricRowIndex].Remove(columnIndex);

                //insert the new lines
                lyricLines.Insert(lyricRowIndex + 1, newLyricLine);
                lyricLines.Insert(lyricRowIndex + 1, newChordLine);

            }

            StringBuilder buildLyrics = new StringBuilder();
            for (int i = 0; i < lyricLines.Count; i++)
            {
                buildLyrics.AppendLine(lyricLines[i]);
            }

            song.lyrics = buildLyrics.ToString();
            
            //determine the new caret position
            int pos = 0;
            int row = rowIndex + 2;
            for (int i = 0; i < row; i++)
            {
            	pos += lyricLines[i].Length + 2;
            }
            pos += 1; //move over to the next line
            
            return pos;
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
                    if (notePart.Trim(new char[] { ' ', '\r', '\n' }).Length >= 2)
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
