/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 12/3/2010
 * Time: 3:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using OpenChords.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
            if (this.LyricsFormat.FontSize > 0 && this.contentLineSpacing > 0)
            {
                this.HeadingsFormat.FontSize--;
                this.LyricsFormat.FontSize--;
                this.ChordFormat.FontSize--;
                this.contentLineSpacing--;
                this.NoteFormat.FontSize--;
                this.NextPageFormat.FontSize--;
            }

        }

        public void increaseSizes()
        {
            this.HeadingsFormat.FontSize++;
            this.LyricsFormat.FontSize++;
            this.ChordFormat.FontSize++;
            this.contentLineSpacing++;
            this.NoteFormat.FontSize++;
            this.NextPageFormat.FontSize++;
        }
        
        
        
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DisplayAndPrintSettingsType settingsType { get; set; }
		
		
		public int pageHeight {get;	set;}
		public int pageWidth {get; set;}

		public float authorSize {get; set;}
		public float footerSize {get; set;}

		public float paragraphSpacing {get; set;}
		public float contentLineSpacing {get; set;}

		public int leftPageMargin {get; set;}
		public int topPageMargin {get; set;}
		public int bottomPageMargin {get; set;}
		public int rightPageMargin {get; set;}
		public int notesStartPosition {get; set;}
		public int lyricsStartPosition {get; set;}
		
        public bool? DualColumns {get; set;}
        public bool? ShowNotes  {get; set;}
        public bool? ShowChords  {get; set;}
        public bool? ShowLyrics  {get; set;}
        public bool? ShowPleaseTurnOver { get; set; }
        
        public bool? ShowGeneralNotes { get; set; }
        public string BackgroundColorHex { get; set; }
       
        public SongElementFormat TitleFormat { get; set; }
        public SongElementFormat ChordFormat { get; set; }
        public SongElementFormat LyricsFormat { get; set; }
        public SongElementFormat HeadingsFormat { get; set; }
        public SongElementFormat Order1Format { get; set; }
        public SongElementFormat Order2Format { get; set; }
        public SongElementFormat NoteFormat { get; set; }
        public SongElementFormat NextPageFormat { get; set; }

        public int? NoteWidth { get; set; }

        /// <summary>
        /// we check for nulls to determine if we need to refresh the settings file
        /// </summary>
        /// <returns></returns>
        public bool nullsInFile()
        {
            if (DualColumns == null || ShowNotes == null || ShowChords == null || ShowLyrics == null || ShowPleaseTurnOver == null)
                return true;

            if (ShowGeneralNotes == null)
                return true;

            if (BackgroundColorHex == null)
                return true;

            if (TitleFormat == null || ChordFormat == null || LyricsFormat == null ||
                HeadingsFormat == null || Order1Format == null || Order2Format == null ||
                NoteFormat == null || NextPageFormat == null)
                return true;

            if (NoteWidth == null)
                return true;

            return false;
        }

        [XmlIgnore]
        public Color BackgroundColor
        {
            get { return ColorTranslator.FromHtml(BackgroundColorHex); }
            set { BackgroundColorHex = ColorTranslator.ToHtml(value); }
        }

		public override string ToString ()
		{
			return "pageHeight:" + pageHeight + "\n"
				+  "pageWidth:" + pageWidth;
		}
		
		public static DisplayAndPrintSettings loadSettings(DisplayAndPrintSettingsType settingsType)
		{
					
			DisplayAndPrintSettings settings = new DisplayAndPrintSettings();
			
			if (settingsType == DisplayAndPrintSettingsType.DisplaySettings)
			{
                settings = XmlReaderWriter.readSettings(Settings.ExtAppsAndDir.displaySettingsFileName);
                if (settings == null) settings = new DisplayAndPrintSettings(DisplayAndPrintSettingsType.DisplaySettings);
                settings.SettingsFilePath = Settings.ExtAppsAndDir.displaySettingsFileName;
            }
            else if (settingsType == DisplayAndPrintSettingsType.PrintSettings)
			{
                settings = XmlReaderWriter.readSettings(Settings.ExtAppsAndDir.printSettingsFilename);
                if (settings == null) settings = new DisplayAndPrintSettings(DisplayAndPrintSettingsType.PrintSettings);
                settings.SettingsFilePath = Settings.ExtAppsAndDir.printSettingsFilename;
			}
            else if (settingsType == DisplayAndPrintSettingsType.TabletSettings)
            {
                settings = XmlReaderWriter.readSettings(Settings.ExtAppsAndDir.tabletSettingsFilename);
                if (settings == null) settings = new DisplayAndPrintSettings(DisplayAndPrintSettingsType.TabletSettings);
                settings.SettingsFilePath = Settings.ExtAppsAndDir.tabletSettingsFilename;
            }
			
			return settings;
		}

        //load settings file from indicated path
        public static DisplayAndPrintSettings loadSettings(DisplayAndPrintSettingsType settingsType, string path)
        {
           var settings = XmlReaderWriter.readSettings(path);
           if (settings == null) settings = new DisplayAndPrintSettings(settingsType, path);
           settings.SettingsFilePath = path;
            return settings;
        }
		
		public void saveSettings()
		{
            XmlReaderWriter.writeSettings(SettingsFilePath, this);   
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

		
		private float adjustForLowerResolutions1(float originalValue)
		{
				
			return (float)Math.Floor(originalValue / (900) * (pageHeight));
			
		}

        private float adjustForLowerResolutions2(float originalValue)
        {

            return (float)Math.Floor(originalValue / (1440) * (pageWidth));

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
                SettingsFilePath = Settings.ExtAppsAndDir.displaySettingsFileName;
                pageHeight = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;
                pageWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;

                titleSize = adjustForLowerResolutions1(13);
                authorSize = adjustForLowerResolutions1(12);
                contentSize = adjustForLowerResolutions1(15);
                footerSize = adjustForLowerResolutions1(13);
                notesSize = adjustForLowerResolutions1(14);
                orderSize = adjustForLowerResolutions1(13);
                nextPageSize = adjustForLowerResolutions1(15);
                

                paragraphSpacing = adjustForLowerResolutions2(7);
                contentLineSpacing = adjustForLowerResolutions2(3);

                leftPageMargin = 5;
                topPageMargin = 5;
                bottomPageMargin = 5;
                rightPageMargin = 5;

                notesStartPosition = (int)adjustForLowerResolutions1((float)120);
                lyricsStartPosition = 60;

                DualColumns = true;
                ShowNotes = true;
                ShowChords = true;
                ShowLyrics = true;
                ShowGeneralNotes = false;

                LyricsColor = ColorTranslator.FromHtml("White");
                BackgroundColor = Color.Black;
                ChordColor = ColorTranslator.FromHtml("#00FF80");
                HeadingsColor = ColorTranslator.FromHtml("#BE7C7C");
                TitleColor = ColorTranslator.FromHtml("#FEAF81");
                OrderColor1 = ColorTranslator.FromHtml("#FF8040");
                OrderColor2 = ColorTranslator.FromHtml("#FFFF80");
                NoteColor = ColorTranslator.FromHtml("#80FF80");
                NextPageColor = ColorTranslator.FromHtml("White");

                NoteWidth = 20;

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
                SettingsFilePath = Settings.ExtAppsAndDir.tabletSettingsFilename;
                pageHeight = 1024;
                pageWidth = 768;

                titleSize = adjustForLowerResolutions1(18);
                authorSize = adjustForLowerResolutions1(20);
                contentSize = adjustForLowerResolutions1(20);
                footerSize = adjustForLowerResolutions1(13);
                notesSize = adjustForLowerResolutions1(18);
                orderSize = adjustForLowerResolutions1(18);
                nextPageSize = adjustForLowerResolutions1(20);
                
                paragraphSpacing = adjustForLowerResolutions2(7);
                contentLineSpacing = adjustForLowerResolutions2(2);

                leftPageMargin = (int)contentSize;
                topPageMargin = (int)contentSize;
                bottomPageMargin = (int)contentSize;
                rightPageMargin = (int)contentSize;

                notesStartPosition = (int)adjustForLowerResolutions1((float)60);
                lyricsStartPosition = 60;

                DualColumns = false;
                ShowNotes = true;
                ShowChords = true;
                ShowLyrics = true;
                ShowGeneralNotes = false;

                LyricsColor = ColorTranslator.FromHtml("White");
                BackgroundColor = Color.Black;
                ChordColor = ColorTranslator.FromHtml("#00FF80");
                HeadingsColor = ColorTranslator.FromHtml("#BE7C7C");
                TitleColor = ColorTranslator.FromHtml("#FEAF81");
                OrderColor1 = ColorTranslator.FromHtml("#FF8040");
                OrderColor2 = ColorTranslator.FromHtml("#FFFF80");
                NoteColor = ColorTranslator.FromHtml("#80FF80");
                NextPageColor = ColorTranslator.FromHtml("White");

                NoteWidth = (int)adjustForLowerResolutions2(564);
                if (DualColumns.Value == true) NoteWidth = NoteWidth / 2;

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
                SettingsFilePath = Settings.ExtAppsAndDir.printSettingsFilename;
                pageHeight = 842;
                pageWidth = 595;

                titleSize = 12;
                authorSize = 12;
                contentSize = 15;
                footerSize = 13;
                nextPageSize = 15;

                paragraphSpacing = 15;
                contentLineSpacing = 6;

                leftPageMargin = 40;
                topPageMargin = 20;
                bottomPageMargin = 20;
                rightPageMargin = 20;
                notesSize = 13;
                notesStartPosition = 150;
                lyricsStartPosition = 60;
                orderSize = 12;
                
                

                DualColumns = false;
                ShowNotes = true;
                ShowChords = true;
                ShowLyrics = true;
                ShowGeneralNotes = false;

                LyricsColor = Color.Black;
                BackgroundColor = Color.White;
                ChordColor = Color.Black;
                NoteColor = Color.Black;
                HeadingsColor = Color.Black;
                TitleColor = Color.Black;
                OrderColor1 = Color.Black;
                OrderColor2 = Color.Gray;
                NextPageColor = ColorTranslator.FromHtml("White");

                NoteWidth = 150;

                BoldChords = true;
                BoldLyrics = false;
                BoldNotes = false;
                BoldHeadings = true;
                BoldTitle = true;
                BoldOrder1 = true;
                BoldOrder2 = true;
                BoldNextPage = true;


            }

            ShowPleaseTurnOver = true;

            TitleFormat = new SongElementFormat("Helvetica", titleSize, TitleColor, BoldTitle);
            HeadingsFormat = new SongElementFormat("Helvetica", contentSize, HeadingsColor, BoldHeadings);
            ChordFormat = new SongElementFormat("Courier New", contentSize, ChordColor, BoldChords);
            LyricsFormat = new SongElementFormat("Courier New", contentSize, LyricsColor, BoldLyrics);
            Order1Format = new SongElementFormat("Courier New", orderSize, OrderColor1, BoldOrder1);
            Order2Format = new SongElementFormat("Courier New", orderSize, OrderColor2, BoldOrder2);
            NoteFormat = new SongElementFormat("Courier New", notesSize, NoteColor, BoldNotes);
            NextPageFormat = new SongElementFormat("Courier New", nextPageSize, NextPageColor, BoldNextPage);
        }
		
        //public DisplayAndPrintSettings(SerializationInfo info, StreamingContext ctxt)
        //{
        //    pageHeight = (int)info.GetValue("pageHeight", typeof(int));
        //    pageWidth = (int)info.GetValue("pageWidth", typeof(int));
			
        //    titleSize = (float)info.GetValue("titleSize", typeof(float));
        //    authorSize = (float)info.GetValue("authorSize", typeof(float));
        //    contentSize = (float)info.GetValue("contentSize", typeof(float));
        //    footerSize = (float)info.GetValue("footerSize", typeof(float));
        //    notesSize = (float)info.GetValue("notesSize", typeof(float));
        //    orderSize = (float)info.GetValue("orderSize", typeof(float));
			
        //    paragraphSpacing = (float)info.GetValue("paragraphSpacing", typeof(float));
        //    contentLineSpacing = (float)info.GetValue("contentLineSpacing", typeof(float));
			
        //    leftPageMargin = (int)info.GetValue("leftPageMargin", typeof(int));
        //    topPageMargin = (int)info.GetValue("topPageMargin", typeof(int));
        //    bottomPageMargin = (int)info.GetValue("bottomPageMargin", typeof(int));
        //    rightPageMargin = (int)info.GetValue("rightPageMargin", typeof(int));
			
        //    notesStartPosition = (int)info.GetValue("notesStartPosition", typeof(int));
        //    lyricsStartPosition = (int)info.GetValue("lyricsStartPosition", typeof(int));
			
        //    settingsType = (DisplayAndPrintSettingsType)info.GetValue("settingsType", typeof(DisplayAndPrintSettingsType));

        //    DualColumns = (bool)info.GetValue("DualColumns", typeof(bool));
        //    ShowNotes = (bool)info.GetValue("ShowNotes", typeof(bool));
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        //{
        //    info.AddValue("pageHeight", pageHeight);
        //    info.AddValue("pageWidth", pageWidth);
			
        //    info.AddValue("titleSize", titleSize);
        //    info.AddValue("authorSize", authorSize);
        //    info.AddValue("contentSize", contentSize);
        //    info.AddValue("footerSize", footerSize);
        //    info.AddValue("notesSize", notesSize);
        //    info.AddValue("orderSize", orderSize);
			
        //    info.AddValue("paragraphSpacing", paragraphSpacing);
        //    info.AddValue("contentLineSpacing", contentLineSpacing);
			
        //    info.AddValue("leftPageMargin", leftPageMargin);
        //    info.AddValue("topPageMargin", topPageMargin);
        //    info.AddValue("bottomPageMargin", bottomPageMargin);
        //    info.AddValue("rightPageMargin", rightPageMargin);
			
        //    info.AddValue("notesStartPosition", notesStartPosition);
        //    info.AddValue("lyricsStartPosition", lyricsStartPosition);
			
        //    info.AddValue("settingsType", settingsType);

        //    info.AddValue("DualColumns", DualColumns);
        //    info.AddValue("ShowNotes", ShowNotes);
        //}



        
    }
}

