/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 11/22/2010
 * Time: 2:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


using OpenChords.Entities;
using OpenChords.Functions;

namespace OpenChords.Export
{
	/// <summary>
	/// Description of ExportToRtf.
	/// </summary>
	public class ExportToRtf
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static RichTextBox richTextBox;
		private static Song currentSong;
		private static DisplayAndPrintSettings printSettings;
		
		private static Font titleFont;
		private static Font orderFont;
		private static Font verseHeaderFont;
		private static Font verseChordFont;
		private static Font verseLyricFont;
		private static Font paragraphSpacingFont;
		private static Font lineSpacingFont;
		
		static string[] verseHeader;
		static List<string[]> verseContent;
		static List<bool[]> isChord;
		
		static string[] notesHeaders;
		static List<string[]> notesContent;
		
		
		
		public ExportToRtf()
		{
			richTextBox.AppendText("Hello World");
			
			
			richTextBox.SaveFile("test.rtf");
			
			
		}
		
		public static string exportSong(Song song, DisplayAndPrintSettingsType settingsType)
		{
			printSettings = DisplayAndPrintSettings.loadSettings(settingsType);
			currentSong = song;
			String Filename = SongProcessor.generateFileName(currentSong);
			
			richTextBox = new RichTextBox();
			
			setupFonts();
			
			
			pageSetup();
			
			writeSong();
			
			richTextBox.SaveFile(Settings.ExtAppsAndDir.printFolder + Filename + ".rtf");
			
			return Filename + ".rtf";
			
			
		}
		
		private static void setupFonts()
		{
            titleFont = printSettings.TitleFormat.Font;
			orderFont = new Font("Courier New", printSettings.Order1Format.FontSize, FontStyle.Regular);
            verseHeaderFont = printSettings.HeadingsFormat.Font;
            verseChordFont = printSettings.ChordFormat.Font;
            verseLyricFont = printSettings.LyricsFormat.Font;
			paragraphSpacingFont = new Font("Courier New", printSettings.paragraphSpacing, FontStyle.Regular);
			lineSpacingFont = new Font("Courier New", printSettings.contentLineSpacing, FontStyle.Regular);
		}
		
		private static void pageSetup()
		{
			richTextBox.Size = new Size(printSettings.pageWidth, printSettings.pageHeight);
			
			int x = printSettings.leftPageMargin;
			int y = printSettings.topPageMargin;
			int width = printSettings.pageWidth - printSettings.leftPageMargin - printSettings.rightPageMargin;
			int height = printSettings.pageHeight - printSettings.topPageMargin - printSettings.bottomPageMargin;
					
			richTextBox.Bounds = new Rectangle(x, y, width, height);
			
		
			
			
		
		}
		
		private static void writeSong()
		{
			List<bool[]> temp;
			SongProcessor.lyricsProcessor(currentSong.lyrics, out verseHeader, out verseContent, out isChord);
			SongProcessor.lyricsProcessor(currentSong.notes, out notesHeaders, out notesContent, out temp);
			
			//newPage();
			
			writeHeader();
			//writeGeneralNotes();
			
			
			
			
			for (int i = 0; i < verseHeader.Length; i++)
			{
				if (verseFits(i))
				{
					//make order list of selected verse, bold
					//makeOrderBold(i, headerIndex);
					//draw verse
					writeVerseHeader(i);
					//capture the current position for displaying song notes
					//int notesPos = heightPosition;
					writeVerseContents(i);
					newline();
					
					//writeNotes(i, notesPos);
				}
			}
		}
		
		/// <summary>
		/// returns true if the verse fits on the page
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		static bool verseFits(int index)
		{
			return true;
			
			/*
			float paragraphEndPosition =
				heightPosition - ((verseContent[index].Length + 1) * 
				                  (printSettings.contentSize + printSettings.contentLineSpacing));
			//return paragraphEndPosition < (DisplaySettings.pageHeight - virtualBottomPageMargin);
			return paragraphEndPosition > printSettings.bottomPageMargin;*/
		}
		
		/// <summary>
		/// write the title, auther and order and other header information
		/// </summary>
		/// <param name="page"></param>
		/// <param name="song"></param>
		private static void writeHeader()
		{
			String songTitleLine = SongProcessor.generateFileName(currentSong);
			appendText(songTitleLine + "\r\n", titleFont);
			newparagraph();
			
			appendText("Order: " + currentSong.presentation + "\r\n", orderFont);
			newparagraph();
		}
		
		static void writeVerseHeader(int index)
		{
			appendText(SongProcessor.verseHeaderConvertion(verseHeader[index]) + "\r\n",
			           verseHeaderFont);
			
			newline();
		}
		
		static void writeVerseContents(int index)
		{
			for (int j = 0; j < verseContent[index].Length; j++)
			{
				if (isChord[index][j])
				{
					appendText(verseContent[index][j],
					           verseChordFont);
				}
				else
				{
					appendText(verseContent[index][j],
					           verseLyricFont);
				}
				
				newline();
				
			}
		}
		
		
		
		private static void newline()
		{
			appendText("\r\n",lineSpacingFont);
		}
		
		private static void newparagraph()
		{
			appendText("\r\n",paragraphSpacingFont);
		}
		
		// Append text of the given fontname, size, and style
		private static void appendText(string text, Font font)
		{
			//int start = box.TextLength;
			richTextBox.SelectionFont = font;
			richTextBox.AppendText(text);
			//int end = box.TextLength;
			
			// Textbox may transform chars, so (end-start) != text.Length
			/*
            richTextBox.Select(start, end - start);
            {
                richTextBox.SelectionFont = font;
                
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear*/
		}
		
	}
}
