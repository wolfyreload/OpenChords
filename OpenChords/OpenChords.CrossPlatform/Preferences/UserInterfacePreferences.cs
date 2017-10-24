using Eto.Forms;
using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform.Preferences
{
    public class UserInterfacePreferences : Panel
    {
        private FontAndColorPicker lyricsColorPicker;
        private FontAndColorPicker notesColorPicker;
        private FontAndColorPicker textboxColorPicker;
        private FontAndColorPicker labelColorPicker;
    
        public UserInterfacePreferences()
        {
            var userInterfaceSettings = Entities.UserInterfaceSettings.Instance;

            lyricsColorPicker = new FontAndColorPicker("Lyrics", FontAndColorPicker.FontAndColorPickerType.Font, true);
            notesColorPicker = new FontAndColorPicker("Notes", FontAndColorPicker.FontAndColorPickerType.Font, false);
            textboxColorPicker = new FontAndColorPicker("Textbox", FontAndColorPicker.FontAndColorPickerType.Font, false);
            labelColorPicker = new FontAndColorPicker("Labels", FontAndColorPicker.FontAndColorPickerType.Font, false);
    

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
                        }
                    },
                    new TableRow(lyricsColorPicker),
                    new TableRow(notesColorPicker),
                    new TableRow(textboxColorPicker),
                    null
                }
            };

            lyricsColorPicker.setFontFormat(userInterfaceSettings.LyricsFormat);
            notesColorPicker.setFontFormat(userInterfaceSettings.NoteFormat);
            textboxColorPicker.setFontFormat(userInterfaceSettings.TextboxFormat);
            labelColorPicker.setFontFormat(userInterfaceSettings.LabelFormat);
        }

        internal void SavePreferences()
        {
            var userInterfaceSettings = Entities.UserInterfaceSettings.Instance;
            userInterfaceSettings.LyricsFormat = lyricsColorPicker.GetSongElementFormat();
            userInterfaceSettings.NoteFormat = notesColorPicker.GetSongElementFormat();
            userInterfaceSettings.TextboxFormat = textboxColorPicker.GetSongElementFormat();
            userInterfaceSettings.LabelFormat = labelColorPicker.GetSongElementFormat();
            userInterfaceSettings.saveSettings();
        }
    }
}
