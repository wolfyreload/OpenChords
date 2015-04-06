using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChords.CrossPlatform.Preferences
{
    class FontAndColorPanel : Panel
    {
        private Entities.DisplayAndPrintSettings displayAndPrintSettings;

        private FontAndColorPicker titleColorPicker;
        private FontAndColorPicker headingsColorPicker;
        private FontAndColorPicker chordsColorPicker;
        private FontAndColorPicker lyricsColorPicker;
        private FontAndColorPicker notesColorPicker;
        private FontAndColorPicker backgroundColor;
        private FontAndColorPicker verseHeadingBackgroundColor;
        private FontAndColorPicker verseLyricsBackgroundColor1;
        private FontAndColorPicker verseLyricsBackgroundColor2;
        private FontAndColorPicker verseLyricsBorderColor;

        public FontAndColorPanel(Entities.DisplayAndPrintSettings displayAndPrintSettings)
        {
            this.displayAndPrintSettings = displayAndPrintSettings;
            titleColorPicker = new FontAndColorPicker(label: "Title", format: displayAndPrintSettings.TitleFormat);
            headingsColorPicker = new FontAndColorPicker(label: "Headings", format: displayAndPrintSettings.HeadingsFormat);
            chordsColorPicker = new FontAndColorPicker(label: "Chords", format: displayAndPrintSettings.ChordFormat, useMonospaceFontsOnly: true);
            lyricsColorPicker = new FontAndColorPicker(label: "Lyrics", format: displayAndPrintSettings.LyricsFormat, useMonospaceFontsOnly: true);
            notesColorPicker = new FontAndColorPicker(label: "Notes", format: displayAndPrintSettings.NoteFormat);
            backgroundColor = new FontAndColorPicker(label: "Background Color", color: displayAndPrintSettings.BackgroundColor);
            verseHeadingBackgroundColor = new FontAndColorPicker(label: "Verse Heading Background Color", color: displayAndPrintSettings.VerseHeadingBackgroundColor);
            verseLyricsBackgroundColor1 = new FontAndColorPicker(label: "Verse Background Color 1", color: displayAndPrintSettings.VerseLyricsBackground1Color);
            verseLyricsBackgroundColor2 = new FontAndColorPicker(label: "Verse Background Color 2", color: displayAndPrintSettings.VerseLyricsBackground2Color);
            verseLyricsBorderColor = new FontAndColorPicker(label: "Verse Border Color", color: displayAndPrintSettings.VerseBorderColor);

            Content = new TableLayout()
            {
                Rows = 
                {
                    new TableRow() 
                    {
                        Cells = 
                        {
                            new Label(), 
                            new Label() { Text = "Font" }, 
                            new Label() { Text = "Size" }, 
                            new Label() { Text = "Color" }, 
                            new Label() { Text = "Style"} 
                        } 
                    },
                    titleColorPicker,
                    headingsColorPicker,
                    chordsColorPicker,
                    lyricsColorPicker,
                    notesColorPicker,
                    backgroundColor,
                    verseHeadingBackgroundColor,
                    verseLyricsBackgroundColor1,
                    verseLyricsBackgroundColor2,
                    verseLyricsBorderColor,
                    null
                }
            };
        }

        internal void UpdateColorAndFontPreferences()
        {
            displayAndPrintSettings.TitleFormat = titleColorPicker.GetSongElementFormat();
            displayAndPrintSettings.HeadingsFormat = headingsColorPicker.GetSongElementFormat();
            displayAndPrintSettings.ChordFormat = chordsColorPicker.GetSongElementFormat();
            displayAndPrintSettings.LyricsFormat = lyricsColorPicker.GetSongElementFormat();
            displayAndPrintSettings.NoteFormat = notesColorPicker.GetSongElementFormat();
            displayAndPrintSettings.BackgroundColor = backgroundColor.getColor();
            displayAndPrintSettings.VerseHeadingBackgroundColor = verseHeadingBackgroundColor.getColor();
            displayAndPrintSettings.VerseLyricsBackground1Color = verseLyricsBackgroundColor1.getColor();
            displayAndPrintSettings.VerseLyricsBackground2Color = verseLyricsBackgroundColor2.getColor();
            displayAndPrintSettings.VerseBorderColor = verseLyricsBorderColor.getColor();


        }
    }
}
