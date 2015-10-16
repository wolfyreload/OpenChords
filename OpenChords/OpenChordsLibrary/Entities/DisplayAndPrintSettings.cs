/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 12/3/2010
 * Time: 3:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OpenChords.IO;
using System.Drawing;
using System.Xml.Serialization;

namespace OpenChords.Entities
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
    public enum DisplayAndPrintSettingsType {DisplaySettings, PrintSettings, TabletSettings}

	[Serializable()]
	public class DisplayAndPrintSettings
	{

        public void desceaseSizes()
        {
            if (this.LyricsFormat.FontSize > 0)
            {
                this.HeadingsFormat.FontSize--;
                this.LyricsFormat.FontSize--;
                this.ChordFormat.FontSize--;
                this.NoteFormat.FontSize--;
            }

        }

        public void increaseSizes()
        {
            this.HeadingsFormat.FontSize++;
            this.LyricsFormat.FontSize++;
            this.ChordFormat.FontSize++;
            this.NoteFormat.FontSize++;
        }
        
        
        
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DisplayAndPrintSettingsType settingsType { get; set; }
	
        public bool? ShowNotes  {get; set;}
        public bool? ShowChords  {get; set;}
        public bool? ShowLyrics  {get; set;}
          
        public string SongOrientation { get; set; }

        public string BackgroundColorHex { get; set; }
        public string VerseHeadingBackgroundColorHex { get; set; }
        public string VerseLyricsBackgroundColor1Hex { get; set; }
        public string VerseLyricsBackgroundColor2Hex { get; set; }
        public string VerseBorderColorHex { get; set; }

        public SongElementFormat TitleFormat { get; set; }
        public SongElementFormat ChordFormat { get; set; }
        public SongElementFormat LyricsFormat { get; set; }
        public SongElementFormat HeadingsFormat { get; set; }
        public SongElementFormat Order1Format { get; set; }
        public SongElementFormat Order2Format { get; set; }
        public SongElementFormat NoteFormat { get; set; }
   
        /// <summary>
        /// we check for nulls to determine if we need to refresh the settings file
        /// </summary>
        /// <returns></returns>
        public bool nullsInFile()
        {
            if (ShowNotes == null || ShowChords == null || ShowLyrics == null)
                return true;

            if (BackgroundColorHex == null)
                return true;

            if (TitleFormat == null || ChordFormat == null || LyricsFormat == null ||
                HeadingsFormat == null || Order1Format == null || Order2Format == null ||
                NoteFormat == null)
                return true;

            return false;
        }

        [XmlIgnore]
        public Color BackgroundColor
        {
            get { return ColorTranslator.FromHtml(BackgroundColorHex); }
            set { BackgroundColorHex = ColorTranslator.ToHtml(value); }
        }

        [XmlIgnore]
        public Color VerseHeadingBackgroundColor
        {
            get { return ColorTranslator.FromHtml(VerseHeadingBackgroundColorHex); }
            set { VerseHeadingBackgroundColorHex = ColorTranslator.ToHtml(value); }
        }

        [XmlIgnore]
        public Color VerseLyricsBackground1Color
        {
            get { return ColorTranslator.FromHtml(VerseLyricsBackgroundColor1Hex); }
            set { VerseLyricsBackgroundColor1Hex = ColorTranslator.ToHtml(value); }
        }

        [XmlIgnore]
        public Color VerseLyricsBackground2Color
        {
            get { return ColorTranslator.FromHtml(VerseLyricsBackgroundColor2Hex); }
            set { VerseLyricsBackgroundColor2Hex = ColorTranslator.ToHtml(value); }
        }

        [XmlIgnore]
        public Color VerseBorderColor
        {
            get { return ColorTranslator.FromHtml(VerseBorderColorHex); }
            set { VerseBorderColorHex = ColorTranslator.ToHtml(value); }
        }
		
		public static DisplayAndPrintSettings loadSettings(DisplayAndPrintSettingsType settingsType)
		{
					
			DisplayAndPrintSettings settings = new DisplayAndPrintSettings();
			
			if (settingsType == DisplayAndPrintSettingsType.DisplaySettings)
			{
                settings = XmlReaderWriter.readSettings(Settings.ExtAppsAndDir.DisplaySettingsFileName);
                if (settings == null) settings = new DisplayAndPrintSettings(DisplayAndPrintSettingsType.DisplaySettings);
                settings.SettingsFilePath = Settings.ExtAppsAndDir.DisplaySettingsFileName;
            }
            else if (settingsType == DisplayAndPrintSettingsType.PrintSettings)
			{
                settings = XmlReaderWriter.readSettings(Settings.ExtAppsAndDir.PrintSettingsFilename);
                if (settings == null) settings = new DisplayAndPrintSettings(DisplayAndPrintSettingsType.PrintSettings);
                settings.SettingsFilePath = Settings.ExtAppsAndDir.PrintSettingsFilename;
			}
            else if (settingsType == DisplayAndPrintSettingsType.TabletSettings)
            {
                settings = XmlReaderWriter.readSettings(Settings.ExtAppsAndDir.TabletSettingsFilename);
                if (settings == null) settings = new DisplayAndPrintSettings(DisplayAndPrintSettingsType.TabletSettings);
                settings.SettingsFilePath = Settings.ExtAppsAndDir.TabletSettingsFilename;
            }
			
			return settings;
		}

        //load settings file from indicated path
        public static DisplayAndPrintSettings loadSettings(DisplayAndPrintSettingsType settingsType, string path)
        {
            var settings = XmlReaderWriter.readSettings(path);
            if (settings == null) settings = new DisplayAndPrintSettings(settingsType, path);
            settings.SettingsFilePath = path;
            settings.settingsType = settingsType;
            return settings;
        }
		
		public void saveSettings()
		{
            saveSettings(SettingsFilePath);
		}

        public void saveSettings(string path)
        {
            XmlReaderWriter.writeSettings(path, this);
        }

        public bool isSettingsFilePresent()
        {
            var settingsFilePresent = false;
            settingsFilePresent = IO.FileFolderFunctions.isFilePresent(SettingsFilePath);
            return settingsFilePresent;
        }
		
		public DisplayAndPrintSettings()
		{
			
		}

		
		private float adjustForLowerResolutions1(float originalValue, int screenHeight)
		{
				
			return (float)Math.Floor(originalValue / (900) * (screenHeight));
			
		}

        private float adjustForLowerResolutions2(float originalValue, int screenWidth)
        {

            return (float)Math.Floor(originalValue / (1440) * (screenWidth));

        }
		
        [XmlIgnore]
        public string SettingsFilePath {get; set;}

        /// <summary>
        /// creates the default settings with a path in which to save the settings to
        /// </summary>
        /// <param name="settingsType"></param>
        /// <param name="path"></param>
        public DisplayAndPrintSettings(DisplayAndPrintSettingsType settingsType, string path)
        {
            createDefault(settingsType);

            SettingsFilePath = path;

        }

        /// <summary>
        /// defzult settings
        /// </summary>
        /// <param name="settingsType"></param>
        public DisplayAndPrintSettings(DisplayAndPrintSettingsType settingsType)
        {
            createDefault(settingsType);
        }

        private void createDefault(DisplayAndPrintSettingsType settingsType)
        {
            this.settingsType = settingsType;
            float titleSize;
            Color TitleColor;
            bool BoldTitle;
            float contentSize;
            Color ChordColor;
            Color LyricsColor;
            bool BoldChords;
            bool BoldLyrics;
            Color HeadingsColor;
            bool BoldHeadings;
            Color OrderColor1;
            Color OrderColor2;
            float notesSize;
            float orderSize;
            Color NoteColor;
            bool BoldNotes;
            bool BoldOrder1;
            bool BoldOrder2;
            Color NextPageColor;
            bool BoldNextPage;
            float nextPageSize;

            if (settingsType == DisplayAndPrintSettingsType.DisplaySettings)
            {
                SettingsFilePath = Settings.ExtAppsAndDir.DisplaySettingsFileName;
                var pageHeight = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;
                var pageWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;

                titleSize = adjustForLowerResolutions1(25, pageHeight);
                contentSize = adjustForLowerResolutions1(15, pageHeight);
                notesSize = adjustForLowerResolutions1(14, pageHeight);
                orderSize = adjustForLowerResolutions1(13, pageHeight);
                nextPageSize = adjustForLowerResolutions1(15, pageHeight);
            
                ShowNotes = true;
                ShowChords = true;
                ShowLyrics = true;
        
                LyricsColor = ColorTranslator.FromHtml("White");
                BackgroundColor = Color.Black;
                ChordColor = ColorTranslator.FromHtml("#00FF80");
                HeadingsColor = ColorTranslator.FromHtml("#BE7C7C");
                TitleColor = ColorTranslator.FromHtml("#FEAF81");
                OrderColor1 = ColorTranslator.FromHtml("#FF8040");
                OrderColor2 = ColorTranslator.FromHtml("#FFFF80");
                NoteColor = ColorTranslator.FromHtml("#80FF80");
                NextPageColor = ColorTranslator.FromHtml("White");
                VerseHeadingBackgroundColor = ColorTranslator.FromHtml("#490707");
                VerseLyricsBackground1Color = ColorTranslator.FromHtml("#000000");
                VerseLyricsBackground2Color = ColorTranslator.FromHtml("#2E2424");
                VerseBorderColor = ColorTranslator.FromHtml("#4C4848");

                BoldChords = true;
                BoldLyrics = true;
                BoldNotes = false;
                BoldHeadings = true;
                BoldTitle = true;
                BoldOrder1 = true;
                BoldOrder2 = true;
                BoldNextPage = true;

            }
            else if (settingsType == DisplayAndPrintSettingsType.TabletSettings)
            {
                SettingsFilePath = Settings.ExtAppsAndDir.TabletSettingsFilename;
                var pageHeight = 1024;
                var pageWidth = 768;

                titleSize = adjustForLowerResolutions1(25, pageHeight);
                contentSize = adjustForLowerResolutions1(20, pageHeight);
                notesSize = adjustForLowerResolutions1(18, pageHeight);
                orderSize = adjustForLowerResolutions1(18, pageHeight);
                nextPageSize = adjustForLowerResolutions1(20, pageHeight);
                                
                ShowNotes = true;
                ShowChords = true;
                ShowLyrics = true;
                
                LyricsColor = ColorTranslator.FromHtml("White");
                BackgroundColor = Color.Black;
                ChordColor = ColorTranslator.FromHtml("#00FF80");
                HeadingsColor = ColorTranslator.FromHtml("#BE7C7C");
                TitleColor = ColorTranslator.FromHtml("#FEAF81");
                OrderColor1 = ColorTranslator.FromHtml("#FF8040");
                OrderColor2 = ColorTranslator.FromHtml("#FFFF80");
                NoteColor = ColorTranslator.FromHtml("#80FF80");
                NextPageColor = ColorTranslator.FromHtml("White");
                VerseHeadingBackgroundColor = ColorTranslator.FromHtml("#490707");
                VerseLyricsBackground1Color = ColorTranslator.FromHtml("#000000");
                VerseLyricsBackground2Color = ColorTranslator.FromHtml("#2E2424");
                VerseBorderColor = ColorTranslator.FromHtml("#4C4848");

                BoldChords = true;
                BoldLyrics = true;
                BoldNotes = false;
                BoldHeadings = true;
                BoldTitle = true;
                BoldOrder1 = true;
                BoldOrder2 = true;
                BoldNextPage = true;
            }
            else //if (settingsType == DisplayAndPrintSettingsType.PrintSettings)
            {
                SettingsFilePath = Settings.ExtAppsAndDir.PrintSettingsFilename;
             
                titleSize = 12;
                contentSize = 15;
                nextPageSize = 15;

                notesSize = 13;
                orderSize = 12;

                ShowNotes = true;
                ShowChords = true;
                ShowLyrics = true;
                
                LyricsColor = Color.Black;
                BackgroundColor = Color.White;
                ChordColor = Color.Black;
                NoteColor = Color.Black;
                HeadingsColor = Color.Black;
                TitleColor = Color.Black;
                OrderColor1 = Color.Black;
                OrderColor2 = Color.Gray;
                NextPageColor = ColorTranslator.FromHtml("White");
                VerseHeadingBackgroundColor = ColorTranslator.FromHtml("White");
                VerseLyricsBackground1Color = ColorTranslator.FromHtml("White");
                VerseLyricsBackground2Color = ColorTranslator.FromHtml("White");
                VerseBorderColor = ColorTranslator.FromHtml("Black");

                BoldChords = true;
                BoldLyrics = false;
                BoldNotes = false;
                BoldHeadings = true;
                BoldTitle = true;
                BoldOrder1 = true;
                BoldOrder2 = true;
                BoldNextPage = true;


            }
            
            TitleFormat = new SongElementFormat("Arial", titleSize, TitleColor, BoldTitle);
            HeadingsFormat = new SongElementFormat("Arial", contentSize, HeadingsColor, BoldHeadings);
            ChordFormat = new SongElementFormat("Courier New", contentSize, ChordColor, BoldChords);
            LyricsFormat = new SongElementFormat("Courier New", contentSize, LyricsColor, BoldLyrics);
            Order1Format = new SongElementFormat("Courier New", orderSize, OrderColor1, BoldOrder1);
            Order2Format = new SongElementFormat("Courier New", orderSize, OrderColor2, BoldOrder2);
            NoteFormat = new SongElementFormat("Courier New", notesSize, NoteColor, BoldNotes);
        }

    }
}

