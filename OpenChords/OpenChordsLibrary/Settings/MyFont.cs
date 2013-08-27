/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 11/21/2010
 * Time: 1:25 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace OpenChords.Settings
{
	/// <summary>
	/// Description of FontNames.
	/// </summary>
	public static class EditorFont
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public static string buttonFontName = "Microsoft Sans Serif";
		public static float buttonFontSize = 8F;
		public static FontStyle buttonFontStyle = FontStyle.Regular;
		
		public static string listboxFontName = "Microsoft Sans Serif";
		public static float listboxFontSize = 10F;
		public static FontStyle listboxFontStyle = FontStyle.Regular;
		
		public static string labelFontName = "Microsoft Sans Serif";
		public static float labelFontSize = 9F;
		public static FontStyle labelFontStyle = FontStyle.Regular;
		
		public static string groupboxFontName = "Microsoft Sans Serif";
		public static float groupboxFontSize = 7F;
		public static FontStyle groupboxFontStyle = FontStyle.Regular;
		
		public static string textboxFontName = "Lucida Console";
		public static float textboxFontSize = 11F;
		public static FontStyle textboxFontStyle = FontStyle.Regular;
		
		public static string textfieldFontName = "Lucida Console";
		public static float textfieldFontSize = 10F;
		public static FontStyle textfieldFontStyle = FontStyle.Regular;
	}
	
	/*
	public static class DisplayFont
	{
		public static string titleFontName = "Helvetica";
		public static float titleFontSize = DisplaySettings.titleSize;
		public static FontStyle titleFontStyle = FontStyle.Bold;
		
		public static string orderFontName = "Courier New";
		public static float orderFontSize = DisplaySettings.orderSize;
		public static FontStyle orderFontStyle = FontStyle.Bold;
		
		public static string currentVerseForOrderFontName = "Courier New";
		public static float currentVerseForOrderFontSize
		public static FontStyle currentVerseForOrderFontStyle = FontStyle.Bold;
		
		public static string verseHeaderFontName = "Helvetica";
		public static float verseHeaderFontSize = DisplaySettings.contentSize;
		public static FontStyle verseHeaderFontStyle = FontStyle.Italic;
		
		public static string verseChordFontName = "Courier New";
		public static float verseChordFontSize = DisplaySettings.contentSize;
		public static FontStyle verseChordFontStyle = FontStyle.Bold;
		
		public static string verseLyricFontName = "Courier New";
		public static float verseLyricFontSize = DisplaySettings.contentSize;
		public static FontStyle verseLyricFontStyle = FontStyle.Regular;
				
		public static string notesFontName = "Helvetica";
		public static float notesFontSize = DisplaySettings.notesSize;
		public static FontStyle notesFontStyle = FontStyle.Regular;
		

	}
	*/
}
