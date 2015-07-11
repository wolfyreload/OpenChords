using Eto.Forms;
using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform
{
    internal class frmPresent : Form
    {
        private Set CurrentSet;
        private DisplayAndPrintSettings DisplaySettings;
        private WebView webView = new WebView();
        private int SongIndex;
        private int MaxIndex;
        private Song CurrentSong;
        private bool _SongChanged;
        private bool _SettingsChanged;
        private Functions.Metronome metronome1;
       

        public frmPresent(Song song, DisplayAndPrintSettings settings)
        {
            CurrentSet = new Set();
            CurrentSet.addSongToSet(song);
            DisplaySettings = settings;
            Functions.WebServer.CurrentDisplayAndPrintSettings = settings;
            MaxIndex = CurrentSet.songList.Count;
            this.Title = "Present Song";
            buildUI();
        }

        public frmPresent(Set set, DisplayAndPrintSettings settings)
        {
            CurrentSet = set;
            DisplaySettings = settings;
            Functions.WebServer.CurrentDisplayAndPrintSettings = settings;
            MaxIndex = CurrentSet.songList.Count;
            this.Title = "Present Set";
            buildUI();
        }

        private void buildUI()
        {
            Width = 500;
            Height = 500;
            WindowState = Eto.Forms.WindowState.Maximized;
            this.WindowStyle = Eto.Forms.WindowStyle.None;
            Content = webView;
            this.Icon = Graphics.Icon;

            drawSong();

            if (Platform.IsWpf)
                webView.KeyDown += webView_KeyDown;

            var menuItemExit = new Command { MenuText = "Exit", Shortcut = Keys.Escape };
            menuItemExit.Executed += (s, e) => this.Close();
            
            // refresh item
            var menuItemRefresh = new Command { MenuText = "Refresh", Shortcut = Application.Instance.CommonModifier | Keys.R };
            menuItemRefresh.Executed += menuItemRefresh_Executed;

            // size items
            var commandSongIncreaseSize = new Command { MenuText = "Increase Font Size", Shortcut = Application.Instance.AlternateModifier | Keys.P};
            commandSongIncreaseSize.Executed += commandSongIncreaseSize_Executed;
            var commandSongDecreaseSize = new Command { MenuText = "Decrease Font Size", Shortcut = Application.Instance.AlternateModifier | Keys.O };
            commandSongDecreaseSize.Executed += commandSongDecreaseSize_Executed;
            
            
            // key items
            var commandSongIncreaseKey = new Command { MenuText = "Transpose Key Up", Shortcut = Application.Instance.CommonModifier | Keys.D0 };
            commandSongIncreaseKey.Executed += commandSongIncreaseKey_Executed;
            var commandSongDecreaseKey = new Command { MenuText = "Transpose Key Down", Shortcut = Application.Instance.CommonModifier | Keys.D9 };
            commandSongDecreaseKey.Executed += commandSongDecreaseKey_Executed;
            var commandSongIncreaseCapo = new Command { MenuText = "Capo Up", Shortcut = Application.Instance.CommonModifier | Keys.D8 };
            commandSongIncreaseCapo.Executed += commandSongIncreaseCapo_Executed;
            var commandSongDecreaseCapo = new Command { MenuText = "Capo Down", Shortcut = Application.Instance.CommonModifier | Keys.D7 };
            commandSongDecreaseCapo.Executed += commandSongDecreaseCapo_Executed;

            // Navigation menu
            var menuItemNextSong = new Command { MenuText = "Next Song", Shortcut = Application.Instance.CommonModifier | Keys.Right };
            menuItemNextSong.Executed += menuItemNextSong_Executed;
            var menuItemPreviousSong = new Command { MenuText = "Previous Song", Shortcut = Application.Instance.CommonModifier | Keys.Left };
            menuItemPreviousSong.Executed += menuItemPreviousSong_Executed;
            var menuItemNavigation = new ButtonMenuItem() { Text = "&Navigation", Items = { menuItemPreviousSong, menuItemNextSong } };
                
            //visibility menu
            var commandToggleChords = new Command { MenuText = "Toggle Chords", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            commandToggleChords.Executed += (s, e) => { toggleChords(); };
            var commandToggleLyrics = new Command { MenuText = "Toggle Lyrics", Shortcut = Application.Instance.CommonModifier | Keys.W };
            commandToggleLyrics.Executed += (s, e) => { toggleLyrics(); };
            var commandToggleNotes = new Command { MenuText = "Toggle Notes", Shortcut = Application.Instance.CommonModifier | Keys.E };
            commandToggleNotes.Executed += (s, e) => { toggleNotes(); };
            var commandToggleSharpsAndFlats = new Command { MenuText = "Toggle Sharps/Flats" };
            commandToggleSharpsAndFlats.Executed += (s, e) => { toggleSharpsAndFlats(); };

            //Metronome
            var commandToggleMetonome = new Command { MenuText = "Toggle Metronome", Shortcut = Application.Instance.CommonModifier | Keys.M };
            commandToggleMetonome.Executed += commandToggleMetonome_Executed;

            //song List
            var menuItemSongList = new ButtonMenuItem() { Text = "Song &List" };
            populateSongListMenu(menuItemSongList);

            if (MaxIndex == 1)
            {
                menuItemNavigation.Enabled = false;
                menuItemSongList.Enabled = false;
            }

            Menu = new MenuBar()
            {
                Items = 
                {
                    menuItemRefresh,
                    new ButtonMenuItem() { Text = "&Size", Items = { commandSongIncreaseSize, commandSongDecreaseSize }},
                    new ButtonMenuItem() { Text = "&Key", Items = {commandSongIncreaseKey, commandSongDecreaseKey, commandSongIncreaseCapo, commandSongDecreaseCapo }},
                    menuItemNavigation,
                    new ButtonMenuItem() { Text = "&Visibility", Items = { commandToggleChords, commandToggleLyrics, commandToggleNotes, commandToggleSharpsAndFlats } },
                    new ButtonMenuItem() { Text = "Other Options",
                        Items = 
                        {
                            commandToggleMetonome
                        }
                    },
                    menuItemSongList,
                    menuItemExit 
                }
            };

            metronome1 = new OpenChords.Functions.Metronome();
            this.Closing += frmPresent_Closing;
        }

        void commandToggleMetonome_Executed(object sender, EventArgs e)
        {
            metronome1.SetSong(CurrentSong);   
            metronome1.Enabled = !metronome1.Enabled;
        }

        void commandSongDecreaseSize_Executed(object sender, EventArgs e)
        {
            DisplaySettings.desceaseSizes();
            _SettingsChanged = true;
            drawSong();
        }

        void commandSongIncreaseSize_Executed(object sender, EventArgs e)
        {
            DisplaySettings.increaseSizes();
            _SettingsChanged = true; 
            drawSong();
        }

        void menuItemRefresh_Executed(object sender, EventArgs e)
        {
            CurrentSong.revertToSaved();
            _SongChanged = false;
            drawSong();
        }

        void commandSongDecreaseCapo_Executed(object sender, EventArgs e)
        {
            CurrentSong.capoDown();
            _SongChanged = true;
            drawSong();
        }

        void commandSongIncreaseCapo_Executed(object sender, EventArgs e)
        {
            CurrentSong.capoUp();
            _SongChanged = true;
            drawSong();
        }

        void commandSongDecreaseKey_Executed(object sender, EventArgs e)
        {
            CurrentSong.transposeKeyDown();
            _SongChanged = true;
            drawSong();
        }

        void commandSongIncreaseKey_Executed(object sender, EventArgs e)
        {
            CurrentSong.transposeKeyUp();
            _SongChanged = true;
            drawSong();
        }

        void menuItemPreviousSong_Executed(object sender, EventArgs e)
        {
            metronome1.Enabled = false;
            SaveSongifChanged();
            SongIndex--;
            drawSong();
        }

        void menuItemNextSong_Executed(object sender, EventArgs e)
        {
            metronome1.Enabled = false;
            SaveSongifChanged();
            SongIndex++;
            drawSong();
        }

        void frmPresent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSongifChanged();
            if (_SettingsChanged)
                DisplaySettings.saveSettings();
            metronome1.Dispose();
        }

        private void SaveSongifChanged()
        {
            if (_SongChanged)
            {
                CurrentSong.saveSong();
                _SongChanged = false;
            }
        }

        private void toggleSharpsAndFlats()
        {
            CurrentSong.ToggleSharpsAndFlats();
            _SongChanged = true;
            drawSong();
        }

        private void populateSongListMenu(ButtonMenuItem menuItemSongList)
        {
            for (int i = 0; i < CurrentSet.songList.Count(); i++)
            {
                var song = CurrentSet.songList[i];
                int shortcutKeyCode = i + 40; //this maps to the number keys on the keyboard
                var songCommand = new Command() { Tag = i, MenuText = song.title };
                if (shortcutKeyCode <= (int)Keys.D9)
                {
                    songCommand.Shortcut = Application.Instance.AlternateModifier | (Keys)shortcutKeyCode;
                }
                songCommand.Executed += songCommand_execute;
                menuItemSongList.Items.Add(songCommand);
            }
        }

        private void songCommand_execute(object sender, EventArgs e)
        {
            var songCommand = (Command)sender;
            SongIndex = (int)songCommand.Tag;
            drawSong();
            
        }

    

        private void toggleChords()
        {
            DisplaySettings.ShowChords = !(DisplaySettings.ShowChords ?? false);
            _SettingsChanged = true;
            drawSong();
        }

        private void toggleLyrics()
        {
            DisplaySettings.ShowLyrics = !(DisplaySettings.ShowLyrics ?? false);
            _SettingsChanged = true;
            drawSong();
        }

        private void toggleNotes()
        {
            DisplaySettings.ShowNotes = !(DisplaySettings.ShowNotes ?? false);
            _SettingsChanged = true;
            drawSong();
        }

        void webView_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Keys.Escape)
            //    this.Close();
            if (e.Key == Keys.Up || e.Key == Keys.Down) 
                e.Handled = true;
        }

        private void drawSong()
        {
            if (MaxIndex == 0) return;
            if (SongIndex < 0) SongIndex = 0;
            if (SongIndex >= MaxIndex) SongIndex = MaxIndex - 1;
            CurrentSong = CurrentSet.songList[SongIndex];
            OpenChords.Functions.WebServer.CurrentSong = CurrentSong;
            string songHtml = CurrentSong.getHtml(DisplaySettings);
            webView.LoadHtml(songHtml);
        }
     }
}
