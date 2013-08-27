using OpenChords.Entities;
using OpenChords.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OpenChords.Controller
{
    public class SongController
    {
        Graphics graphicsObj;
        Song currentSong;
        Set currentSet;
        DisplayAndPrintSettings displaySettings;

        string[] verseHeader;
        List<string[]> verseContent;
        List<bool[]> isChord;
        string[] notesHeaders;
        List<string[]> notesContent;

        Brush lyricsBrush = new SolidBrush(System.Drawing.Color.Black);
        Brush chordsBrush = new SolidBrush(System.Drawing.Color.Black);
        Brush notesBrush = new SolidBrush(System.Drawing.Color.Black);
        Font titleFont;
        Font OrderFont;
        Font verseHeaderFont;
        Font verseChordFont;
        Font verseLyricFont;
        Font presentHeadersInOrderFont;

        Font notesFont;

        float orderHeightPosition;
        float heightPosition;

        int indexOfSongIndices = 0;
        int setIndex = 0;

        //allows tracking of where you are in the song
        List<int> songPageIndices = new List<int>();

        int versesDisplayed = 0;
        int virtualBottomPageMargin = 0;

        public SongController(Song song, DisplayAndPrintSettings settings, Graphics graphicsObj)
        {
            this.graphicsObj = graphicsObj;
            this.currentSong = song;
            displaySettings = settings;
            //setupDisplayWindow(displaySettings);
            initializeFonts();
        }




        private void initializeFonts()
        {
            titleFont = new System.Drawing.Font("Helvetica", displaySettings.titleSize, FontStyle.Bold);
            OrderFont = new System.Drawing.Font("Courier New", displaySettings.orderSize, FontStyle.Regular);
            verseHeaderFont = new System.Drawing.Font("Helvetica", displaySettings.contentSize, FontStyle.Italic);
            verseChordFont = new System.Drawing.Font("Courier New", displaySettings.contentSize, FontStyle.Bold);
            verseLyricFont = new System.Drawing.Font("Courier New", displaySettings.contentSize, FontStyle.Regular);
            presentHeadersInOrderFont = new System.Drawing.Font("Courier New", displaySettings.orderSize, FontStyle.Bold);

            notesFont = new System.Drawing.Font("Helvetica", displaySettings.notesSize, FontStyle.Regular);

            //fix bottom margin ensuring space for the please turn over text
            virtualBottomPageMargin = (int)(displaySettings.bottomPageMargin + 2 * (displaySettings.contentSize + displaySettings.contentLineSpacing));

            lyricsBrush = new SolidBrush(displaySettings.ForegroundColor);
            chordsBrush = new SolidBrush(displaySettings.ChordColor);
            notesBrush = new SolidBrush(displaySettings.NoteColor);
        }


        #region songDrawing

        public void drawSong()
        {
            List<bool[]> temp;
            SongProcessor.lyricsProcessor(currentSong.lyrics, out verseHeader, out verseContent, out isChord);
            SongProcessor.lyricsProcessor(currentSong.notes, out notesHeaders, out notesContent, out temp);
            bool firstHalfOfPage = true;

            string[] serperators = { " " };
            string[] orderIndex = currentSong.presentation.Split(serperators, StringSplitOptions.RemoveEmptyEntries);

            drawHeader();
            drawGeneralNotes();

            versesDisplayed = 0;

            for (int i = songPageIndices[indexOfSongIndices]; i < orderIndex.Length; i++)
            {
                //draw the currently selected order
                List<string> tempHeaderlist = new List<string>(verseHeader);
                int headerIndex = tempHeaderlist.IndexOf(orderIndex[i]);

                if (headerIndex == -1)
                    continue;
                if (!verseFits(headerIndex) && firstHalfOfPage && displaySettings.DualColumns)
                {
                    firstHalfOfPage = false;
                    heightPosition = displaySettings.topPageMargin;
                    drawHeader();
                }



                if (verseFits(headerIndex))
                {
                    //make order list of selected verse, bold
                    makeOrderBold(i, headerIndex);
                    //draw verse
                    drawVerseHeader(headerIndex, firstHalfOfPage);
                    //capture the current position for displaying song notes
                    float notesPos = heightPosition;
                    drawVerseContents(headerIndex, firstHalfOfPage);
                    newLine();

                    drawNotes(i, notesPos, firstHalfOfPage);
                    //System.Console.WriteLine(displaySettings.ToString());
                    versesDisplayed++;
                }
                else
                {
                    //draw partial verse
                    drawVerseHeader(headerIndex, firstHalfOfPage);
                    //capture the current position for displaying song notes
                    float notesPos = heightPosition;
                    drawPartialVerseContents(headerIndex, firstHalfOfPage);

                    drawNotes(i, notesPos, firstHalfOfPage);

                    break;
                }
            }
        }

        /// <summary>
        /// returns true if the verse fits on the page
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool verseFits(int index)
        {
            float paragraphEndPosition =            //+1 for header
                heightPosition + (verseContent[index].Length + 1) *
                (displaySettings.contentSize + displaySettings.contentLineSpacing);
            return paragraphEndPosition < (displaySettings.pageHeight - virtualBottomPageMargin);// - displaySettings.topPageMargin);
        }

        /// <summary>
        /// draws over the text of the previous order
        /// makes the currently selected verse bold
        /// </summary>
        /// <param name="positionIndex"></param>
        /// <param name="headerIndex"></param>
        void makeOrderBold(int positionIndex, int headerIndex)
        {
            string tempstring = "".PadRight(7 + positionIndex * 4);
            tempstring = tempstring.Insert(7 + positionIndex * 4, verseHeader[headerIndex]);
            graphicsObj.DrawString(tempstring,
                                   presentHeadersInOrderFont,
                                   lyricsBrush,
                                   displaySettings.leftPageMargin,
                                   orderHeightPosition);
        }

        /// <summary>
        /// draws the title are order list to screen
        /// </summary>
        void drawHeader()
        {
            heightPosition = displaySettings.topPageMargin;

            //draw title
            graphicsObj.DrawString(SongProcessor.generateFileName(currentSong),
                                   titleFont,
                                   lyricsBrush,
                                   displaySettings.leftPageMargin,
                                   heightPosition);


            //draw order
            heightPosition +=
                displaySettings.titleSize +
                displaySettings.paragraphSpacing;

            orderHeightPosition = heightPosition;
            graphicsObj.DrawString("Order: " + SongProcessor.orderFormat(currentSong.presentation),
                                   OrderFont,
                                   lyricsBrush,
                                   displaySettings.leftPageMargin,
                                   orderHeightPosition);

            heightPosition +=
                displaySettings.orderSize +
                displaySettings.paragraphSpacing;

        }

        void drawVerseHeader(int index, bool firstHalfOfPage)
        {
            float leftPosition;
            if (!firstHalfOfPage)
                leftPosition = displaySettings.pageWidth / 2 + displaySettings.leftPageMargin;
            else
                leftPosition = displaySettings.leftPageMargin;


            graphicsObj.DrawString(SongProcessor.verseHeaderConvertion(verseHeader[index]),
                                   verseHeaderFont,
                                   lyricsBrush,
                                   leftPosition,
                                   heightPosition);

            newLine();
        }

        void drawVerseContents(int index, bool firstHalfOfPage)
        {
            float leftMargin = 0;

            if (firstHalfOfPage)
                leftMargin = displaySettings.leftPageMargin;
            else
                leftMargin = displaySettings.pageWidth / 2 + displaySettings.leftPageMargin;


            for (int j = 0; j < verseContent[index].Length; j++)
            {
                if (isChord[index][j])
                {
                    graphicsObj.DrawString(verseContent[index][j],
                                           verseChordFont,
                                           chordsBrush,
                                           leftMargin,
                                           heightPosition);
                }
                else
                {
                    graphicsObj.DrawString(verseContent[index][j],
                                           verseLyricFont,
                                           lyricsBrush,
                                           leftMargin,
                                           heightPosition);
                }

                newLine();

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
        void drawPartialVerseContents(int index, bool firstHalfOfPage)
        {
            float currentPosition = heightPosition;
            float spaceSize = displaySettings.contentSize + displaySettings.contentLineSpacing;
            int linesToFit = (int)((displaySettings.pageHeight - virtualBottomPageMargin - currentPosition - displaySettings.topPageMargin) / spaceSize);
            float leftPosition;
            if (firstHalfOfPage)
                leftPosition = displaySettings.leftPageMargin;
            else
                leftPosition = displaySettings.pageWidth / 2 + displaySettings.leftPageMargin;

            writeToDebug(String.Format("currentPosition: {0}", currentPosition));
            writeToDebug(String.Format("linesToFit: {0}", linesToFit));


            for (int j = 0; j < verseContent[index].Length && j <= linesToFit; j++)
            {
                if (isChord[index][j])
                {
                    graphicsObj.DrawString(verseContent[index][j],
                                           verseChordFont,
                                           chordsBrush,
                                           leftPosition,
                                           heightPosition);
                }
                else
                {
                    graphicsObj.DrawString(verseContent[index][j],
                                           verseLyricFont,
                                           lyricsBrush,
                                           leftPosition,
                                           heightPosition);
                }

                newLine();


            }

            graphicsObj.DrawString(".......PTO.......",
                                   verseLyricFont,
                                   lyricsBrush,
                                   leftPosition,
                                   heightPosition);
        }

        /// <summary>
        /// draws the per verse notes
        /// </summary>
        /// <param name="index"></param>
        /// <param name="startPos"></param>
        void drawNotes(int index, float startPos, bool firstHalfOfPage)
        {
            if (!displaySettings.ShowNotes) return; //just stop if we must not display notes
            float notesPos = startPos;
            float leftPosition;
            if (firstHalfOfPage)
                leftPosition = displaySettings.pageWidth / 2 - displaySettings.rightPageMargin;
            else
                leftPosition = displaySettings.pageWidth - displaySettings.rightPageMargin;


            if (notesContent.Count > index && notesHeaders[index] != "G")
            {

                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Far;

                for (int j = 0; j < notesContent[index].Length; j++)
                {

                    graphicsObj.DrawString(notesContent[index][j],
                                           notesFont,
                                           notesBrush,
                                           new RectangleF(leftPosition - 500, notesPos, 500, 25),
                                           strFormat);

                    notesPos += displaySettings.notesSize + displaySettings.contentLineSpacing;
                }
            }
        }

        List<List<int>> generateIndexiesVariable()
        {
            List<bool[]> temp;
            List<List<int>> indices = new List<List<int>>();

            SongProcessor.lyricsProcessor(currentSong.lyrics, out verseHeader, out verseContent, out isChord);
            SongProcessor.lyricsProcessor(currentSong.notes, out notesHeaders, out notesContent, out temp);

            string[] serperators = { " " };
            string[] orderIndex = currentSong.presentation.Split(serperators, StringSplitOptions.RemoveEmptyEntries);

            int versePosition = 0;

            //space from from draw header
            heightPosition = displaySettings.topPageMargin;
            heightPosition +=
                displaySettings.titleSize +
                displaySettings.paragraphSpacing;
            heightPosition +=
                displaySettings.orderSize +
                displaySettings.paragraphSpacing;

            versesDisplayed = 0;

            while (versePosition < orderIndex.Length)
            {
                //draw the currently selected order
                List<string> tempHeaderlist = new List<string>(verseHeader);
                int headerIndex = tempHeaderlist.IndexOf(orderIndex[versePosition]);

                if (headerIndex == -1)
                    break;

                if (verseFits(headerIndex))
                {
                    //draw verse                                    //+1 for header +1 for space
                    heightPosition += (isChord[headerIndex].Length + 1 + 1) *
                        (displaySettings.contentSize + displaySettings.contentLineSpacing);



                    versesDisplayed++;

                    versePosition += versesDisplayed;
                }
                else
                {
                    break;
                }
            }

            return new List<List<int>>();
        }

        /// <summary>
        /// draws the general notes
        /// </summary>
        public void drawGeneralNotes()
        {
            if (!displaySettings.ShowNotes) return; //go away if no notes to be displayed
            List<string> templist = new List<string>(notesHeaders);
            int generalNotesIndex = templist.IndexOf("G");
            float notesPosition = displaySettings.pageHeight - 200;
            float leftPositon = displaySettings.pageWidth - 200;
            //if (generalNotesIndex > -1)
            //{
            //    StringFormat strFormat = new StringFormat();

            //    lblGeneralNotes.Text = "";
            //    lblGeneralNotes.Font = notesFont;
            //    for (int j = 0; j < notesContent[generalNotesIndex].Length; j++)
            //    {
            //        lblGeneralNotes.Text += notesContent[generalNotesIndex][j] + Environment.NewLine;

            //        //graphicsObj.DrawString((notesContent[generalNotesIndex][j]),
            //        //                       notesFont,
            //        //                       myBrush,
            //        //                       leftPositon,
            //        //                       notesPosition,
            //        //                       strFormat);

            //        //notesPosition += displaySettings.notesSize + displaySettings.contentLineSpacing;
            //    }
            //}
            //else
            //{
            //    lblGeneralNotes.Text = "";
            //}
        }

        void newLine()
        {
            heightPosition += displaySettings.contentSize + displaySettings.contentLineSpacing;
        }

        #endregion
		



    }
}
