using System;
using System.Text;
using System.Collections.Generic;
using sharpPDF;
using sharpPDF.Enumerators;
using System.Linq;

using OpenChords.Entities;
using OpenChords.Functions;


namespace OpenChords.Export
{
    public static class ExportToPdf
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static int pageNumber;
        static int heightPosition;
        static pdfDocument doc;
        static pdfPage page;
        static string[] verseHeader;
        static List<string[]> verseContent;
        static List<bool[]> isChord;

        static string[] notesHeaders;
        static List<string[]> notesContent;

        static Song currentSong;
        static DisplayAndPrintSettings printSettings;

        static int songStartPosition;

        private static pdfColor BackgroundColor;



        public static string exportSet(Set set, DisplayAndPrintSettingsType settingsType)
        {
            //initialize
            doc = new pdfDocument(set.setName, "OpenChords");


            String Filename = set.setName;
            printSettings = DisplayAndPrintSettings.loadSettings(settingsType);
            BackgroundColor = new pdfColor(printSettings.BackgroundColor.R, printSettings.BackgroundColor.G, printSettings.BackgroundColor.B);
            pageNumber = 0;

            foreach (Song song in set.songList)
            {
                writeSong(song, true);
            }
            doc.createPDF(Settings.ExtAppsAndDir.printFolder + Filename + ".pdf");
            return Filename + ".pdf";
        }

        public static string exportSet(Set set, DisplayAndPrintSettingsType settingsType, string settingsPath)
        {
            //initialize
            doc = new pdfDocument(set.setName, "OpenChords");


            String Filename = set.setName;
            printSettings = DisplayAndPrintSettings.loadSettings(settingsType, settingsPath);
            BackgroundColor = new pdfColor(printSettings.BackgroundColor.R, printSettings.BackgroundColor.G, printSettings.BackgroundColor.B);
            pageNumber = 0;

            foreach (Song song in set.songList)
            {
                writeSong(song, true);
            }
            doc.createPDF(Settings.ExtAppsAndDir.printFolder + Filename + ".pdf");
            return Filename + ".pdf";
        }

        /// <summary>
        /// exports the song to pdf
        /// returns the title of the file
        /// </summary>
        /// <param name="song"></param>
        public static string exportSong(Song song, DisplayAndPrintSettingsType settingsType)
        {
            //initialize
            doc = new pdfDocument(song.title, song.author);
            //page = doc.addPage(PrintSettings.pageHeight, PrintSettings.pageWidth)
            String Filename = SongProcessor.generateFileName(song);
            printSettings = DisplayAndPrintSettings.loadSettings(settingsType);
            BackgroundColor = new pdfColor(printSettings.BackgroundColor.R, printSettings.BackgroundColor.G, printSettings.BackgroundColor.B);
            pageNumber = 0;
            writeSong(song, true);

            doc.createPDF(Settings.ExtAppsAndDir.printFolder + Filename + ".pdf");
            return Filename + ".pdf";
        }

        /// <summary>
        /// exports the song to pdf
        /// returns the title of the file
        /// using a settingspath rather than a settings type
        /// </summary>
        /// <param name="song"></param>
        public static string exportSong(Song song, DisplayAndPrintSettingsType settingsType, string settingsPath)
        {
            //initialize
            doc = new pdfDocument(song.title, song.author);
            //page = doc.addPage(PrintSettings.pageHeight, PrintSettings.pageWidth)
            String Filename = SongProcessor.generateFileName(song);
            printSettings = DisplayAndPrintSettings.loadSettings(settingsType, settingsPath);
            BackgroundColor = new pdfColor(printSettings.BackgroundColor.R, printSettings.BackgroundColor.G, printSettings.BackgroundColor.B);
            pageNumber = 0;
            writeSong(song, true);

            doc.createPDF(Settings.ExtAppsAndDir.printFolder + Filename + ".pdf");
            return Filename + ".pdf";
        }

        public static void writeSong(Song song, bool inOrder)
        {
            List<bool[]> temp;
            SongProcessor.lyricsProcessor(song.lyrics, out verseHeader, out verseContent, out isChord);
            SongProcessor.lyricsProcessor(song.notes, out notesHeaders, out notesContent, out temp);

            currentSong = song;
            var songVerses = song.getSongVerses();
            newPage();
            writeFooter();

            writeHeader();

            //if (printSettings.ShowNotes)
            //    writeGeneralNotes();

            //writeFooter();

            songStartPosition = heightPosition;

            int column = 1;
            int max = songVerses.Count();
            int count = 0;
            bool drawnPartialVerse = false;
            while (count < max)
            {

                var verse = songVerses[count];

                //move to new column
                if (!verseFits(verse) && column == 1 && printSettings.DualColumns.Value)
                {
                    column = 2;
                    heightPosition = songStartPosition;
                }

                //capture the current position for displaying song notes
                int notesPos = heightPosition;
                if (verseFits(verse))
                {
                    //draw verse
                    writeVerseHeader(verse, column);
                    
                    //write notes
                    if (printSettings.ShowNotes.Value)
                        writeNotes(verse, notesPos, column);

                    //write the verse contents
                    writeVerseContents(verse, column);
                    newLine();

                    count++;
                }
                else if (drawnPartialVerse == false)
                {
                    //draw verse
                    writeVerseHeader(verse, column);

                    //write notes
                    //if (printSettings.ShowNotes.Value)
                    //    writeNotes(verse, notesPos, column);

                    //write the verse contents
                    writePartialVerseContents(verse, column);
                    newLine();

                    drawnPartialVerse = true;
                }
                else //make a new page
                {
                    newPage();

                    writeFooter();

                    column = 1;

                    drawnPartialVerse = false;
                }
            }
        }

        /// <summary>
        /// returns true if the verse fits on the page
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        static bool verseFits(SongVerse verse)
        {
            var linesForVerse = 0;
            var linesThatFit = numberOfLinesThatFit();
            if (printSettings.ShowChords.Value && printSettings.ShowLyrics.Value)
                linesForVerse = verse.VerseLinesCount;
            else if (printSettings.ShowChords.Value == false && printSettings.ShowLyrics.Value == true)
                linesForVerse = verse.LyricsLinesCount;
            else if (printSettings.ShowChords.Value == true && printSettings.ShowLyrics.Value == false)
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
        static int numberOfLinesThatFit()
        {
            int lines = 0;

            bool fits = true;
            do
            {
                lines++;
                float paragraphEndPosition =
                    heightPosition - ((lines) * (printSettings.LyricsFormat.FontSize));

                fits = paragraphEndPosition > printSettings.bottomPageMargin;
                
            } while (fits);
            
            //return paragraphEndPosition < (DisplaySettings.pageHeight - virtualBottomPageMargin);
            return lines-1;
        }

        /// <summary>
        /// write the title, auther and order and other header information
        /// </summary>
        /// <param name="page"></param>
        /// <param name="song"></param>
        private static void writeHeader()
        {
            heightPosition = printSettings.pageHeight - printSettings.topPageMargin;

            String songTitleLine = SongProcessor.generateSongTitleInPresentation(currentSong);
            var x1 = printSettings.leftPageMargin;
            var y1 = heightPosition;
            AddTextToPage(songTitleLine, printSettings.TitleFormat, x1, y1);
            newParagragh();

            var x2 = printSettings.leftPageMargin;
            var y2 = heightPosition;
            AddTextToPage("Order: " + currentSong.presentation, printSettings.Order1Format, x2, y2);
            newParagragh();
        }

        static void newParagragh()
        {
            heightPosition -= (int)printSettings.paragraphSpacing + (int)printSettings.TitleFormat.FontSize;
        }

        static void newLine()
        {
            heightPosition -= (int)printSettings.LyricsFormat.FontSize;
        }

        static void newPage()
        {
            page = doc.addPage(printSettings.pageHeight, printSettings.pageWidth);
            pageNumber++;
            heightPosition = page.height - printSettings.topPageMargin;

            page.drawRectangle(0, 0, printSettings.pageWidth, printSettings.pageHeight, BackgroundColor, BackgroundColor);
        }


        static void writeVerseHeader(SongVerse verse, int column)
        {
            if (column == 1)
            {
                var x = printSettings.leftPageMargin;
                var y = heightPosition;
                AddTextToPage(verse.FullHeaderName, printSettings.HeadingsFormat, x, y);
            }
            else
            {
                var x = printSettings.pageWidth / 2 + printSettings.leftPageMargin;
                var y = heightPosition;
                AddTextToPage(verse.FullHeaderName, printSettings.HeadingsFormat, x, y);
            }

            newLine();
        }

        /// <summary>
        /// print text to the screen
        /// </summary>
        /// <param name="text">the actial text to display</param>
        /// <param name="format">formatter</param>
        /// <param name="x">cordinate</param>
        /// <param name="y">coordinate</param>
        private static void AddTextToPage(string text, SongElementFormat format, int x, int y)
        {
            page.addText(text, x, y, format.PdfFont, (int)format.FontSize, format.PdfColor);
        }

        /// <summary>
        /// print text to the screen
        /// </summary>
        /// <param name="text">the actial text to display</param>
        /// <param name="format">formatter</param>
        /// <param name="x">cordinate</param>
        /// <param name="y">coordinate</param>
        private static void AddTextToParagraph(string text, SongElementFormat format, int x, int y)
        {
            var width = printSettings.NoteWidth.Value;
            x = x - width;
            var lineHeight = (int)format.FontSize;
            try
            {
                page.addParagraph(text, x, y, format.PdfFont, (int)format.FontSize, width, lineHeight, predefinedAlignment.csRight, format.PdfColor);
            }
            catch (Exception ex)
            {
                logger.Error("Error printing " + text, ex);
            }
        }

        static void writeVerseContents(SongVerse verse, int column)
        {
            for (int j = 0; j < verse.Lyrics.Count(); j++)
            {
                if (column == 1)
                {
                    if (verse.IsChord[j] && printSettings.ShowChords.Value)
                    {
                        var x = printSettings.leftPageMargin;
                        var y = heightPosition;
                        AddTextToPage(verse.Lyrics[j], printSettings.ChordFormat, x, y);
                        newLine();
                    }
                    else if (!verse.IsChord[j] && printSettings.ShowLyrics.Value)
                    {
                        var x = printSettings.leftPageMargin;
                        var y = heightPosition;
                        AddTextToPage(verse.Lyrics[j], printSettings.LyricsFormat, x, y);
                        newLine();
                    }
                }
                else
                {
                    if (verse.IsChord[j] && printSettings.ShowChords.Value)
                    {
                        var x = printSettings.pageWidth / 2 + printSettings.leftPageMargin;
                        var y = heightPosition;
                        AddTextToPage(verse.Lyrics[j], printSettings.ChordFormat, x, y);
                        newLine();
                    }
                    else if (!verse.IsChord[j] && printSettings.ShowLyrics.Value)
                    {
                        var x = printSettings.pageWidth / 2 + printSettings.leftPageMargin;
                        var y = heightPosition;
                        AddTextToPage(verse.Lyrics[j], printSettings.LyricsFormat, x, y);
                        newLine();
                    }
                }



            }
        }

        static void writePartialVerseContents(SongVerse verse, int column)
        {
            var linesThatFit = numberOfLinesThatFit()-1;
            var currentLines = 0;
            for (int j = 0; j < verse.Lyrics.Count() && currentLines < linesThatFit; j++)
            {
                if (column == 1)
                {
                    if (verse.IsChord[j] && printSettings.ShowChords.Value)
                    {
                        var x = printSettings.leftPageMargin;
                        var y = heightPosition;
                        AddTextToPage(verse.Lyrics[j], printSettings.ChordFormat, x, y);
                        newLine();
                        currentLines++;
             
                    }
                    else if (!verse.IsChord[j] && printSettings.ShowLyrics.Value)
                    {
                        var x = printSettings.leftPageMargin;
                        var y = heightPosition;
                        AddTextToPage(verse.Lyrics[j], printSettings.LyricsFormat, x, y);
                        newLine();
                        currentLines++;
             
                    }
                }
                else
                {
                    if (verse.IsChord[j] && printSettings.ShowChords.Value)
                    {
                        var x = printSettings.pageWidth / 2 + printSettings.leftPageMargin;
                        var y = heightPosition;
                        AddTextToPage(verse.Lyrics[j], printSettings.ChordFormat, x, y);
                        newLine();
                        currentLines++;
                    }
                    else if (!verse.IsChord[j] && printSettings.ShowLyrics.Value)
                    {
                        var x = printSettings.pageWidth / 2 + printSettings.leftPageMargin;
                        var y = heightPosition;
                        AddTextToPage(verse.Lyrics[j], printSettings.LyricsFormat, x, y);
                        newLine();
                        currentLines++;
             
                    }
                }
            }
            if (column == 1)
            {
                var x = printSettings.leftPageMargin;
                var y = heightPosition;
                AddTextToPage(".......PTO.......", printSettings.LyricsFormat, x, y);
            }
            else
            {
                var x = printSettings.pageWidth / 2 + printSettings.leftPageMargin;
                var y = heightPosition;
                AddTextToPage(".......PTO.......", printSettings.LyricsFormat, x, y);
            }

        }

        static void writeGeneralNotes()
        {
            List<string> templist = new List<string>(notesHeaders);
            int generalNotesIndex = templist.IndexOf("G");
            int notesPosition = page.height - printSettings.topPageMargin;
            if (generalNotesIndex > -1)
            {

                for (int j = 0; j < notesContent[generalNotesIndex].Length; j++)
                {
                    var x = printSettings.pageWidth - printSettings.rightPageMargin - 500;
                    var y = notesPosition;
                    AddTextToParagraph(notesContent[generalNotesIndex][j], printSettings.NoteFormat, x, y);  
                    notesPosition -= (int)(printSettings.NoteFormat.FontSize + printSettings.contentLineSpacing);
                }








            }
        }

        /// <summary>
        /// draws the per verse notes
        /// </summary>
        /// <param name="index"></param>
        /// <param name="startPos"></param>
        static void writeNotes(SongVerse verse, int startPos, int column)
        {
            int notesPos = startPos;

            if (printSettings.DualColumns == false)
            {
                var x = printSettings.pageWidth - printSettings.rightPageMargin;
                var y = notesPos;
                AddTextToParagraph(verse.Notes, printSettings.NoteFormat, x, y);
            }
            else
            {
                if (column == 1)
                {
                    var x = printSettings.pageWidth / 2 - printSettings.rightPageMargin;
                    var y = notesPos;
                    AddTextToParagraph(verse.Notes, printSettings.NoteFormat, x, y);
                }
                else if (column == 2)
                {
                    var x = printSettings.pageWidth - printSettings.rightPageMargin;
                    var y = notesPos;
                    AddTextToParagraph(verse.Notes, printSettings.NoteFormat, x, y);

                }
            }
        }

        static void writeFooter()
        {
            //var x1 =  printSettings.leftPageMargin;
            //var y1 = printSettings.bottomPageMargin;
            //AddTextToPage(currentSong.title, printSettings.LyricsFormat, x1, y1);

            var x2 = (int)(page.width - printSettings.LyricsFormat.FontSize * 2);
            var y2 = printSettings.bottomPageMargin;
            AddTextToPage(pageNumber.ToString(), printSettings.LyricsFormat, x2, y2);
        }


        /*
    /// <summary>
    /// write the contents of the song to the pdf document
    /// </summary>
    /// <param name="page"></param>
    /// <param name="song"></param>
    static void writeContents(Song song)
    {
        bool footerWritten = false;
		
        heightPosition = page.height - PrintSettings.lyricsStartPosition;
		
        string[] verseHeader;
        List<string[]> verseContent;
        List<bool[]> isChord;
		
        SongProcessor.lyricsProcessor(song.lyrics, out verseHeader, out verseContent, out isChord);
		
        for (int i = 0; i < verseHeader.Length; i++)
        {
            if (!footerWritten)
            {
                writeFooter(song);
                footerWritten = false;
            }
			
			
            int paragraphEndPosition =
                heightPosition - ((verseContent[i].Length + 1) * 
                                  (PrintSettings.contentSize + PrintSettings.contentLineSpacing));
            if (paragraphEndPosition < PrintSettings.bottomPageMargin)
            {
                page = doc.addPage(PrintSettings.pageHeight, PrintSettings.pageWidth);
				
                pageNumber++;
				
                heightPosition = page.height - PrintSettings.lyricsStartPosition;
				
                footerWritten = false;
            }
			
            //write verse header
            page.addText(SongProcessor.verseHeaderConvertion(verseHeader[i]),
                         PrintSettings.leftPageMargin,
                         heightPosition,
                         predefinedFont.csHelvetivaBoldOblique,
                         PrintSettings.contentSize);
			
            heightPosition -= PrintSettings.contentSize + PrintSettings.contentLineSpacing;
			
            //write rest of verse
            for (int j = 0; j < verseContent[i].Length; j++)
            {
                if (isChord[i][j])
                {
                    page.addText(verseContent[i][j],
                                 PrintSettings.leftPageMargin,
                                 heightPosition,
                                 predefinedFont.csCourierBold,
                                 PrintSettings.contentSize);
                }
                else
                {
                    page.addText(verseContent[i][j],
                                 PrintSettings.leftPageMargin,
                                 heightPosition,
                                 predefinedFont.csCourier,
                                 PrintSettings.contentSize);
                }
				
                heightPosition -= PrintSettings.contentSize + PrintSettings.contentLineSpacing;
				
            }
			
            //space between paragraphs
            heightPosition -= PrintSettings.paragraphSpacing + PrintSettings.contentLineSpacing;
			
			
        }
		
    }
         */


    }
}
