/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/16
 * Time: 10:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using OpenChords.Entities;
using OpenChords.Settings;
using OpenChords.Functions;
using System.Text;

namespace OpenChords.Export
{
	/// <summary>
	/// Description of DisplayPanel.
	/// </summary>
    public class SongRenderer
	{
		Song currentSong;
        List<SongVerse> currentSongElements;
        Set currentSet;
		DisplayAndPrintSettings displaySettings;
		
		
		//graphics options
		System.Drawing.Graphics graphicsObj;

        SongElementFormat titleFormatter;
        SongElementFormat unSelectedOrderFormattor;
        SongElementFormat selectedOrderFormattor;
        SongElementFormat lyricsFormattor;
        SongElementFormat chordsFormattor;
        SongElementFormat notesFormattor;
        SongElementFormat headingFormattor;

		float orderHeightPosition;
		float heightPosition;
		
		int indexOfSongIndices = 0;
		int setIndex = 0;
		
		//allows tracking of where you are in the song
		List<int> songPageIndices = new List<int>();
		
		int versesDisplayed = 0;
		int virtualBottomPageMargin = 0;


        public SongRenderer(System.Drawing.Graphics graphicsObj, Song song, DisplayAndPrintSettings settings)
        {
            this.graphicsObj = graphicsObj;
            //currentSong = song;
            currentSet = new Set();
            currentSet.addSongToSet(song);
            displaySettings = settings;
            initializeFonts();
        }

        public SongRenderer(System.Drawing.Graphics graphicsObj, Set set, DisplayAndPrintSettings settings)
        {
            this.graphicsObj = graphicsObj;
            //currentSong = song;
            currentSet = set;
            displaySettings = settings;
            initializeFonts();
            
        }

		private void initializeFonts()
		{
            titleFormatter = displaySettings.TitleFormat;
            unSelectedOrderFormattor = displaySettings.Order1Format;
            selectedOrderFormattor = displaySettings.Order2Format;
            headingFormattor = displaySettings.HeadingsFormat;
            lyricsFormattor = displaySettings.LyricsFormat;
            chordsFormattor = displaySettings.ChordFormat;
            notesFormattor = displaySettings.NoteFormat;
         

			//fix bottom margin ensuring space for the please turn over text
            virtualBottomPageMargin = (int)(displaySettings.bottomPageMargin + 2 * (displaySettings.LyricsFormat.FontSize + displaySettings.contentLineSpacing));
		}
		
		void drawSongBox()
		{
			
			Pen pen = new Pen(lyricsFormattor.Brush);
			int x = displaySettings.leftPageMargin;
			int y = displaySettings.topPageMargin;
			int width =  displaySettings.pageWidth - displaySettings.rightPageMargin - displaySettings.leftPageMargin;
			int height = displaySettings.pageHeight - displaySettings.bottomPageMargin - displaySettings.topPageMargin;
			graphicsObj.DrawRectangle(pen, x, y, width, height);
		}
		
		#region songDrawing
		
		void drawSong()
		{
			List<bool[]> temp;
            currentSongElements = currentSong.getSongVerses();

			bool firstHalfOfPage = true;
			
			string[] serperators = {" "};
			string[] orderIndex = currentSong.presentation.Split(serperators,StringSplitOptions.RemoveEmptyEntries);
			
			drawHeader();

            bool partialDrawn = false;
			//drawGeneralNotes();
			
			versesDisplayed = 0;
            for (int j = 0; j < orderIndex.Length; j++)
            {
                var songVerse = currentSongElements[j];

                //if song falls within the area to display the song
                if (songPageIndices[indexOfSongIndices] <= j && j < orderIndex.Length)
                {
                    //move to new column
                    if (!verseFits(songVerse) && firstHalfOfPage && displaySettings.DualColumns.Value)
                    {
                        firstHalfOfPage = false;
                        heightPosition = displaySettings.topPageMargin + 15; //dont want to overwrite the press x to excape text
                    }

                    float notesPos = heightPosition;

                    //if the verse fits do the following
                    if (verseFits(songVerse) && partialDrawn != true)
                    {
                        songVerse.isElementDisplayed = true;

                        //draw verse
                        drawVerseHeader(songVerse, firstHalfOfPage);

                        //capture the current position for displaying song notes
                        
                        drawVerseContents(songVerse, firstHalfOfPage);
                        newLine();

                        drawNotes(songVerse, notesPos, firstHalfOfPage);
                        //System.Console.WriteLine(displaySettings.ToString());
                        versesDisplayed++;
                    }
                    else if (partialDrawn == false)
                    {
                        //draw partial verse
                        drawVerseHeader(songVerse, firstHalfOfPage);
                        //capture the current position for displaying song notes
                        
                        drawPartialVerseContents(songVerse, firstHalfOfPage);

                        drawNotes(songVerse, notesPos, firstHalfOfPage);

                        partialDrawn = true;
                        //break;
                    }


                }
                //make order list of selected verse, bold
                makeOrderBold(j);
            }
		}
		
		/// <summary>
		/// returns true if the verse fits on the page
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		bool verseFits(SongVerse verse)
		{
            var linesForVerse = 0;
            var linesThatFit = numberOfLinesThatFit();
            if (displaySettings.ShowChords.Value && displaySettings.ShowLyrics.Value)
                linesForVerse = verse.VerseLinesCount;
            else if (displaySettings.ShowChords.Value == false && displaySettings.ShowLyrics.Value == true)
                linesForVerse = verse.LyricsLinesCount;
            else if (displaySettings.ShowChords.Value == true && displaySettings.ShowLyrics.Value == false)
                linesForVerse = verse.ChordLinesCount;

            if (linesForVerse <= linesThatFit)
                return true;
            else
                return false;
		}

        /// <summary>
        /// returns number of lines that fit
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int numberOfLinesThatFit()
        {
            int lines = 0;

            bool fits = true;
            do
            {
                lines++;
                float paragraphEndPosition =
                    heightPosition + ((lines) * (displaySettings.LyricsFormat.FontSize + displaySettings.contentLineSpacing));

                fits = paragraphEndPosition < (displaySettings.pageHeight - virtualBottomPageMargin);

            } while (fits);

            return lines-1;
        }
		
		/// <summary>
		/// draws over the text of the previous order
		/// makes the currently selected verse bold
		/// </summary>
		/// <param name="headerIndex"></param>
		void makeOrderBold(int headerIndex)
		{
            var currentSongElement = currentSongElements[headerIndex];

            string tempstring = "".PadRight(7 + headerIndex * 4);
            tempstring = tempstring.Insert(7 + headerIndex * 4, currentSongElement.Header);

          
            //var brush = new SolidBrush(displaySettings.BackgroundColor);
            //graphicsObj.FillRectangle(brush, displaySettings.leftPageMargin, orderHeightPosition, tempstring.Length, displaySettings.orderSize);
            if (currentSongElement.isElementDisplayed)
			    graphicsObj.DrawString(tempstring ,
			                       selectedOrderFormattor.Font,
			                       selectedOrderFormattor.Brush,
			                       displaySettings.leftPageMargin,
			                       orderHeightPosition);
            else
                graphicsObj.DrawString(tempstring,
                                   unSelectedOrderFormattor.Font,
                                   unSelectedOrderFormattor.Brush,
                                   displaySettings.leftPageMargin,
                                   orderHeightPosition);

		}
		
		/// <summary>
		/// draws the title are order list to screen
		/// </summary>
		void drawHeader()
		{
			heightPosition = displaySettings.topPageMargin;
		
			//this.Text = SongProcessor.generateFileName(currentSong);
			
			//draw title
            graphicsObj.DrawString(SongProcessor.generateFileName(currentSong),
                                   titleFormatter.Font,
                                   titleFormatter.Brush,
                                   displaySettings.leftPageMargin,
                                   heightPosition);
			
			
			//draw order
            heightPosition +=
                displaySettings.TitleFormat.FontSize +
                displaySettings.paragraphSpacing;
			
			orderHeightPosition = heightPosition;
			graphicsObj.DrawString("Order: ",
			                       unSelectedOrderFormattor.Font,
			                       unSelectedOrderFormattor.Brush,
			                       displaySettings.leftPageMargin,
			                       orderHeightPosition);
			
			heightPosition +=
				displaySettings.Order1Format.FontSize +
				displaySettings.paragraphSpacing;
			
		}
		
		void drawVerseHeader(SongVerse songVerse, bool firstHalfOfPage)
		{
			float leftPosition;
			if (!firstHalfOfPage)
				leftPosition = displaySettings.pageWidth / 2 + displaySettings.leftPageMargin;
			else
				leftPosition = displaySettings.leftPageMargin;



            graphicsObj.DrawString(songVerse.FullHeaderName,
                                   headingFormattor.Font,
                                   headingFormattor.Brush,
			                       leftPosition,
			                       heightPosition);
			
			newLine();
		}
		
		void drawVerseContents(SongVerse songVerse, bool firstHalfOfPage)
		{
			float leftMargin = 0;

			if (firstHalfOfPage)
				leftMargin = displaySettings.leftPageMargin;
			else
				leftMargin = displaySettings.pageWidth / 2 + displaySettings.leftPageMargin;

            for (int j = 0; j < songVerse.Lyrics.Count; j++)
			{

                if (songVerse.IsChord[j] && displaySettings.ShowChords.Value)
				{
                    graphicsObj.DrawString(songVerse.Lyrics[j],
					                       chordsFormattor.Font,
					                       chordsFormattor.Brush,
					                       leftMargin,
					                       heightPosition);
                    newLine();
				}
                else if (!songVerse.IsChord[j] && displaySettings.ShowLyrics.Value)
				{
                    graphicsObj.DrawString(songVerse.Lyrics[j],
					                       lyricsFormattor.Font,
					                       lyricsFormattor.Brush,
					                       leftMargin,
					                       heightPosition);
                    newLine();
				}
				

				
			}
		}
		
		void writeToDebug(string text)
		{
			//txtDebug.Text += text + "\n";
		}
		
		/// <summary>
		/// draw first 2 lines of indicated verse
		/// </summary>
		/// <param name="index"></param>
        void drawPartialVerseContents(SongVerse songVerse, bool firstHalfOfPage)
		{
			float currentPosition = heightPosition;
			float spaceSize = displaySettings.LyricsFormat.FontSize + displaySettings.contentLineSpacing;
			int linesToFit = numberOfLinesThatFit();
            int currentLinesDrawn = 0;
            float leftPosition;
			if (firstHalfOfPage)
				leftPosition = displaySettings.leftPageMargin;
			else 
				leftPosition = displaySettings.pageWidth / 2 + displaySettings.leftPageMargin;
			
			writeToDebug(String.Format("currentPosition: {0}", currentPosition));
			writeToDebug(String.Format("linesToFit: {0}", linesToFit));

            for (int j = 0; j < songVerse.Lyrics.Count && currentLinesDrawn <= linesToFit; j++)
            {

                if (songVerse.IsChord[j] && displaySettings.ShowChords.Value)
                {
                    graphicsObj.DrawString(songVerse.Lyrics[j],
                                           chordsFormattor.Font,
                                           chordsFormattor.Brush,
                                           leftPosition,
                                           heightPosition);
                    newLine();
                    currentLinesDrawn++;
                }
                else if (!songVerse.IsChord[j] && displaySettings.ShowLyrics.Value)
                {
                    graphicsObj.DrawString(songVerse.Lyrics[j],
                                           lyricsFormattor.Font,
                                           lyricsFormattor.Brush,
                                           leftPosition,
                                           heightPosition);
                    newLine();
                    currentLinesDrawn++;
                }
            }
			
			graphicsObj.DrawString(".......PTO.......",
			                       lyricsFormattor.Font,
			                       lyricsFormattor.Brush,
			                       leftPosition,
			                       heightPosition);
		}


        private string makeStringFromArray(string[] list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var l in list) sb.AppendLine(l);
            return sb.ToString();
        }

		/// <summary>
		/// draws the per verse notes
		/// </summary>
		/// <param name="index"></param>
		/// <param name="startPos"></param>
        void drawNotes(SongVerse songVerse, float startPos, bool firstHalfOfPage)
        {
            if (!displaySettings.ShowNotes.Value) return; //just stop if we must not display notes
            float notesPos = startPos;
            float leftPosition;
            if (firstHalfOfPage)
                leftPosition = displaySettings.pageWidth / 2 - displaySettings.rightPageMargin;
            else
                leftPosition = displaySettings.pageWidth - displaySettings.rightPageMargin;

            var noteWidth = displaySettings.NoteWidth.Value;
            var noteRectangle = new RectangleF(leftPosition - noteWidth, notesPos, noteWidth, displaySettings.pageHeight);


            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Far;
            
            graphicsObj.DrawString(songVerse.Notes,
                notesFormattor.Font,
                notesFormattor.Brush,
                noteRectangle,
                strFormat);
        }
		
		/// <summary>
		/// draws the general notes
		/// </summary>
        //public void drawGeneralNotes()
        //{
        //    if (!displaySettings.ShowNotes.Value) return; //go away if no notes to be displayed
        //    List<string> templist = new List<string>(notesHeaders);
        //    int generalNotesIndex = templist.IndexOf("G");
        //    float notesPosition = displaySettings.pageHeight-200;
        //    float leftPositon = displaySettings.pageWidth-200;
        //    if (generalNotesIndex > -1)
        //    {
        //        StringFormat strFormat = new StringFormat();

        //        lblGeneralNotes.Text = "";
        //        lblGeneralNotes.Font = notesFormattor.Font;
        //        for (int j = 0; j < notesContent[generalNotesIndex].Length; j++)
        //        {
        //            lblGeneralNotes.Text += notesContent[generalNotesIndex][j] + Environment.NewLine;

        //            //graphicsObj.DrawString((notesContent[generalNotesIndex][j]),
        //            //                       notesFont,
        //            //                       myBrush,
        //            //                       leftPositon,
        //            //                       notesPosition,
        //            //                       strFormat);

        //            //notesPosition += displaySettings.notesSize + displaySettings.contentLineSpacing;
        //        }
        //    }
        //    else
        //    {
        //        lblGeneralNotes.Text = "";
        //    }
        //}
		
		void newLine()
		{
			heightPosition += displaySettings.LyricsFormat.FontSize + displaySettings.contentLineSpacing;
		}
		
		#endregion
		
		
		
		void initialiseSongIndicesList()
		{
			songPageIndices = new List<int>();
			songPageIndices.Add(0);
			indexOfSongIndices = 0;
		}
	}
}
