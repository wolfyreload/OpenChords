using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.Entities
{
    public class ShortcutSettings
    {
        private static ShortcutSettings Instance { get; set; }

        public ShortcutKey PresentSong { get; set; }
        public ShortcutKey PresentSet { get; set; }
        public ShortcutKey QuitOpenChords { get; set; }
        public ShortcutKey NewSong { get; set; }
        public ShortcutKey SaveSong { get; set; }
        public ShortcutKey TransposeUp { get; set; }
        public ShortcutKey TransposeDown { get; set; }
        public ShortcutKey CapoUp { get; set; }
        public ShortcutKey CapoDown { get; set; }
        public ShortcutKey AutoFormatSong { get; set; }
        public ShortcutKey PrintSongHtml { get; set; }
        public ShortcutKey ExportSetToOpenSong { get; set; }
        public ShortcutKey ShowHelp { get; set; }
        public ShortcutKey RefreshSongList { get; set; }
        public ShortcutKey RefreshSetList { get; set; }
        public ShortcutKey RefreshPresentation { get; set; }
        public ShortcutKey AddSongToSet { get; set; }
        public ShortcutKey DeleteSong { get; set; }
        public ShortcutKey SelectRandomSong { get; set; }
        public ShortcutKey MoveSongUpInSet { get; set; }
        public ShortcutKey MoveSongDownInSet { get; set; }
        public ShortcutKey ExitPresentation { get; set; }
        public ShortcutKey IncreaseFontSize { get; set; }
        public ShortcutKey DecreaseFontSize { get; set; }
        public ShortcutKey GoToNextSong { get; set; }
        public ShortcutKey GoToPreviousSong { get; set; }
        public ShortcutKey ToggleChords { get; set; }
        public ShortcutKey ToggleLyrics { get; set; }
        public ShortcutKey ToggleNotes { get; set; }
        public ShortcutKey ToggleMetronome { get; set; }

        private static readonly string SettingsPath = Path.Combine(Settings.ExtAppsAndDir.SettingsFolder, "Shortcuts.xml");

        public ShortcutSettings()
        {
            PresentSong = new ShortcutKey("F11");
            PresentSet = new ShortcutKey("F12");
            QuitOpenChords = new ShortcutKey("Q", UseControl: true);
            NewSong = new ShortcutKey("N", UseControl: true);
            SaveSong = new ShortcutKey("S", UseControl: true);
            TransposeUp = new ShortcutKey("D0", UseControl: true);
            TransposeDown = new ShortcutKey("D9", UseControl: true);
            CapoUp = new ShortcutKey("D8", UseControl: true);
            CapoDown = new ShortcutKey("D7", UseControl: true);
            AutoFormatSong = new ShortcutKey("R", UseControl: true);
            PrintSongHtml = new ShortcutKey("P", UseControl: true);
            ExportSetToOpenSong = new ShortcutKey("O", UseControl: true);
            ShowHelp = new ShortcutKey("F1");
            RefreshSongList = new ShortcutKey("F5");
            RefreshSetList = new ShortcutKey("F5", UseControl: true);
            RefreshPresentation = new ShortcutKey("R", UseControl: true);
            AddSongToSet = new ShortcutKey("Enter", UseControl: true);
            DeleteSong = new ShortcutKey("Delete", UseControl: true);
            SelectRandomSong = new ShortcutKey("M", UseControl: true);
            MoveSongUpInSet = new ShortcutKey("Up", UseControl: true);
            MoveSongDownInSet = new ShortcutKey("Down", UseControl: true);
            ExitPresentation = new ShortcutKey("Escape");
            IncreaseFontSize = new ShortcutKey("P", UseAlt: true);
            DecreaseFontSize = new ShortcutKey("O", UseAlt: true);
            GoToNextSong = new ShortcutKey("Right", UseControl: true);
            GoToPreviousSong = new ShortcutKey("Left", UseControl: true);
            ToggleChords = new ShortcutKey("Q", UseControl: true);
            ToggleLyrics = new ShortcutKey("W", UseControl: true);
            ToggleNotes = new ShortcutKey("E", UseControl: true);
            ToggleMetronome = new ShortcutKey("M", UseControl: true);
        }

        public static ShortcutSettings LoadSettings()
        {
            if (Instance == null)
                Instance = IO.XmlReaderWriter.ReadShortcutSettings(SettingsPath);
            return Instance;
        }

        public void SaveSettings()
        {
            IO.XmlReaderWriter.WriteShortcutSettings(SettingsPath, this);
        }

        public static ShortcutSettings RefreshSettings()
        {
            Instance = new ShortcutSettings();
            return Instance;
        }
    }

    public class ShortcutKey
    {
        //needed for xml serialization
        public ShortcutKey() { }

        public ShortcutKey(string Key, bool UseControl=false, bool UseAlt=false)
        {
            this.UseControl = UseControl;
            this.UseAlt = UseAlt;
            this.Key = Key;
        }
        [System.Xml.Serialization.XmlAttributeAttribute("Control")]
        public bool UseControl { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute("Alt")]
        public bool UseAlt { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute("ShortcutKey")]
        public string Key { get; set; }

    }
}
