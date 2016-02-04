using OpenChords.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Entities
{
    [Serializable()]
    public class UserInterfaceSettings
    {
        public SongElementFormat LyricsFormat { get; set; }
        public SongElementFormat NoteFormat { get; set; }
        public SongElementFormat TextboxFormat { get; set; }
        public SongElementFormat LabelFormat { get; set; }
   
        private static UserInterfaceSettings _instance;

        public static UserInterfaceSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = loadSettings();
                return _instance;
            }
        }

  
        public UserInterfaceSettings()
        {
            LyricsFormat = new SongElementFormat("monospace", 16, System.Drawing.FontStyle.Regular, System.Drawing.Color.Black);
            NoteFormat = new SongElementFormat("monospace", 16, System.Drawing.FontStyle.Regular, System.Drawing.Color.Black);
            LabelFormat = new SongElementFormat("serif", 9, System.Drawing.FontStyle.Regular, System.Drawing.Color.Black);
            TextboxFormat = new SongElementFormat("serif", 9, System.Drawing.FontStyle.Regular, System.Drawing.Color.Black);
        }

        //load settings file from indicated path
        public static UserInterfaceSettings loadSettings()
        {
            var settings = XmlReaderWriter.readUserInterfaceSettings(Settings.ExtAppsAndDir.UserInterfaceSettingsFileName);
            if (settings == null) settings = new UserInterfaceSettings();
            return settings;
        }

        public void saveSettings()
        {
            XmlReaderWriter.writeUserInterfaceSettings(Settings.ExtAppsAndDir.UserInterfaceSettingsFileName, this);
        }

    }
}
