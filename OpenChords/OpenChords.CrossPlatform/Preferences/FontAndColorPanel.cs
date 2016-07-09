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
        private FontAndColorPicker paritalVerseHeadingBackgroundColor;
        private FontAndColorPicker verseLyricsBackgroundColor1;
        private FontAndColorPicker verseLyricsBackgroundColor2;
        private FontAndColorPicker verseLyricsBorderColor;
        public event EventHandler ItemChanged;


        public FontAndColorPanel(Entities.DisplayAndPrintSettings displayAndPrintSettings)
        {
            this.displayAndPrintSettings = displayAndPrintSettings;
            titleColorPicker = new FontAndColorPicker("Title", FontAndColorPicker.FontAndColorPickerType.FontAndColor);
            headingsColorPicker = new FontAndColorPicker("Headings", FontAndColorPicker.FontAndColorPickerType.FontAndColor);
            chordsColorPicker = new FontAndColorPicker("Chords", FontAndColorPicker.FontAndColorPickerType.FontAndColor, true);
            lyricsColorPicker = new FontAndColorPicker("Lyrics", FontAndColorPicker.FontAndColorPickerType.FontAndColor, true);
            notesColorPicker = new FontAndColorPicker("Notes", FontAndColorPicker.FontAndColorPickerType.FontAndColor);
            backgroundColor = new FontAndColorPicker("Background Color", FontAndColorPicker.FontAndColorPickerType.Color);
            verseHeadingBackgroundColor = new FontAndColorPicker("Verse Heading Background Color", FontAndColorPicker.FontAndColorPickerType.Color);
            paritalVerseHeadingBackgroundColor = new FontAndColorPicker("Partial Heading Background Color", FontAndColorPicker.FontAndColorPickerType.Color);
            verseLyricsBackgroundColor1 = new FontAndColorPicker("Verse Background Color 1", FontAndColorPicker.FontAndColorPickerType.Color);
            verseLyricsBackgroundColor2 = new FontAndColorPicker("Verse Background Color 2", FontAndColorPicker.FontAndColorPickerType.Color);
            verseLyricsBorderColor = new FontAndColorPicker("Verse Border Color", FontAndColorPicker.FontAndColorPickerType.Color);

            updateGuiFromSettingsObject(displayAndPrintSettings);

            Content = new TableLayout()
            {
                Style = "padded-table",
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
                    paritalVerseHeadingBackgroundColor,
                    verseLyricsBackgroundColor1,
                    verseLyricsBackgroundColor2,
                    verseLyricsBorderColor,
                    null
                }
            };

            titleColorPicker.ItemChanged += ColorPicker_ItemChanged;
            headingsColorPicker.ItemChanged += ColorPicker_ItemChanged;
            chordsColorPicker.ItemChanged += ColorPicker_ItemChanged;
            lyricsColorPicker.ItemChanged += ColorPicker_ItemChanged;
            notesColorPicker.ItemChanged += ColorPicker_ItemChanged;
            backgroundColor.ItemChanged += ColorPicker_ItemChanged;
            verseHeadingBackgroundColor.ItemChanged += ColorPicker_ItemChanged;
            paritalVerseHeadingBackgroundColor.ItemChanged += ColorPicker_ItemChanged;
            verseLyricsBackgroundColor1.ItemChanged += ColorPicker_ItemChanged;
            verseLyricsBackgroundColor2.ItemChanged += ColorPicker_ItemChanged;
            verseLyricsBorderColor.ItemChanged += ColorPicker_ItemChanged;
        }

        void ColorPicker_ItemChanged(object sender, EventArgs e)
        {
            if (ItemChanged != null)
                ItemChanged(this, e);
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
            displayAndPrintSettings.PartialVerseHeadingBackgroundColor = paritalVerseHeadingBackgroundColor.getColor();
            displayAndPrintSettings.VerseLyricsBackground1Color = verseLyricsBackgroundColor1.getColor();
            displayAndPrintSettings.VerseLyricsBackground2Color = verseLyricsBackgroundColor2.getColor();
            displayAndPrintSettings.VerseBorderColor = verseLyricsBorderColor.getColor();


        }

        internal void updateGuiFromSettingsObject(Entities.DisplayAndPrintSettings displayAndPrintSettings)
        {
            this.displayAndPrintSettings = displayAndPrintSettings;
            titleColorPicker.setFontFormat(displayAndPrintSettings.TitleFormat);
            headingsColorPicker.setFontFormat(displayAndPrintSettings.HeadingsFormat);
            chordsColorPicker.setFontFormat(displayAndPrintSettings.ChordFormat);
            lyricsColorPicker.setFontFormat(displayAndPrintSettings.LyricsFormat);
            notesColorPicker.setFontFormat(displayAndPrintSettings.NoteFormat);
            backgroundColor.setColor(displayAndPrintSettings.BackgroundColor);
            verseHeadingBackgroundColor.setColor(displayAndPrintSettings.VerseHeadingBackgroundColor);
            paritalVerseHeadingBackgroundColor.setColor(displayAndPrintSettings.PartialVerseHeadingBackgroundColor);
            verseLyricsBackgroundColor1.setColor(displayAndPrintSettings.VerseLyricsBackground1Color);
            verseLyricsBackgroundColor2.setColor(displayAndPrintSettings.VerseLyricsBackground2Color);
            verseLyricsBorderColor.setColor(displayAndPrintSettings.VerseBorderColor);

        }
    }
}
