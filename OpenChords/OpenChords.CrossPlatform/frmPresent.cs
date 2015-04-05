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
       

        public frmPresent(Song song, DisplayAndPrintSettings settings)
        {
            CurrentSet = new Set();
            CurrentSet.addSongToSet(song);
            DisplaySettings = settings;
            MaxIndex = CurrentSet.songList.Count;
            buildUI();
        }

        public frmPresent(Set set, DisplayAndPrintSettings settings)
        {
            CurrentSet = set;
            DisplaySettings = settings;
            MaxIndex = CurrentSet.songList.Count;
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

            webView.KeyUp += webView_KeyUp;

            var menuItemExit = new Command { MenuText = "Exit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            menuItemExit.Executed += (s, e) => this.Close();
            
            // refresh item
            var menuItemRefresh = new Command { MenuText = "Refresh", Shortcut = Application.Instance.CommonModifier | Keys.R };
            menuItemRefresh.Executed += (s, e) => { drawSong(); };

            // size items
            var commandSongIncreaseSize = new Command { MenuText = "Increase Font Size", Shortcut = Application.Instance.CommonModifier | Keys.P };
            commandSongIncreaseSize.Executed += (s, e) => { DisplaySettings.increaseSizes(); drawSong(); };
            var commandSongDecreaseSize = new Command { MenuText = "Decrease Font Size", Shortcut = Application.Instance.CommonModifier | Keys.O };
            commandSongDecreaseSize.Executed += (s, e) => { DisplaySettings.desceaseSizes(); drawSong(); };
            
            
            // key items
            var commandSongIncreaseKey = new Command { MenuText = "Transpose Key Up", Shortcut = Application.Instance.CommonModifier | Keys.D0 };
            commandSongIncreaseKey.Executed += (s, e) => { CurrentSong.transposeKeyUp(); CurrentSong.saveSong(); drawSong(); };
            var commandSongDecreaseKey = new Command { MenuText = "Transpose Key Down", Shortcut = Application.Instance.CommonModifier | Keys.D9 };
            commandSongDecreaseKey.Executed += (s, e) => { CurrentSong.transposeKeyDown(); CurrentSong.saveSong(); drawSong(); };
            var commandSongIncreaseCapo = new Command { MenuText = "Capo Up", Shortcut = Application.Instance.CommonModifier | Keys.D8 };
            commandSongIncreaseCapo.Executed += (s, e) => { CurrentSong.capoUp(); CurrentSong.saveSong(); drawSong(); };
            var commandSongDecreaseCapo = new Command { MenuText = "Capo Down", Shortcut = Application.Instance.CommonModifier | Keys.D7 };
            commandSongDecreaseCapo.Executed += (s, e) => { CurrentSong.capoDown(); CurrentSong.saveSong(); drawSong(); };

            // Navigation menu
            var menuItemNextSong = new Command { MenuText = "Next Song", Shortcut = Application.Instance.CommonModifier | Keys.Right };
            menuItemNextSong.Executed += (s, e) => { SongIndex++; drawSong(); };
            var menuItemPreviousSong = new Command { MenuText = "Previous Song", Shortcut = Application.Instance.CommonModifier | Keys.Left };
            menuItemPreviousSong.Executed += (s, e) => { SongIndex--; drawSong(); };
            var menuItemNavigation = new ButtonMenuItem() { Text = "&Navigation", Items = { menuItemPreviousSong, menuItemNextSong } };
                
            //visibility menu
            var commandToggleChords = new Command { MenuText = "Toggle Chords", Shortcut = Application.Instance.CommonModifier | Keys.C };
            commandToggleChords.Executed += (s, e) => { toggleChords(); };
            var commandToggleLyrics = new Command { MenuText = "Toggle Lyrics", Shortcut = Application.Instance.CommonModifier | Keys.W };
            commandToggleLyrics.Executed += (s, e) => { toggleLyrics(); };
            var commandToggleNotes = new Command { MenuText = "Toggle Notes", Shortcut = Application.Instance.CommonModifier | Keys.N };
            commandToggleNotes.Executed += (s, e) => { toggleNotes(); };
            var commandToggleSharpsAndFlats = new Command { MenuText = "Toggle Sharps/Flats", Shortcut = Application.Instance.CommonModifier | Keys.S };
            commandToggleSharpsAndFlats.Executed += (s, e) => { toggleSharpsAndFlats(); };

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
                    menuItemSongList,
                    menuItemExit 
                }
            };

            this.Closing += frmPresent_Closing;
        }

        void frmPresent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisplaySettings.saveSettings();
        }

        private void toggleSharpsAndFlats()
        {
            CurrentSong.ToggleSharpsAndFlats();
            CurrentSong.saveSong();
            drawSong();
        }

        private void populateSongListMenu(ButtonMenuItem menuItemSongList)
        {
            for (int i = 0; i < CurrentSet.songList.Count(); i++)
            {
                var song = CurrentSet.songList[i];
                var songCommand = new Command() { Tag = i, MenuText = song.title };
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
            drawSong();
        }

        private void toggleLyrics()
        {
            DisplaySettings.ShowLyrics = !(DisplaySettings.ShowLyrics ?? false);
            drawSong();
        }

        private void toggleNotes()
        {
            DisplaySettings.ShowNotes = !(DisplaySettings.ShowNotes ?? false);
            drawSong();
        }

        void webView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Keys.Escape)
                this.Close();
        }

        private void drawSong()
        {
            if (MaxIndex == 0) return;
            if (SongIndex < 0) SongIndex = 0;
            if (SongIndex >= MaxIndex) SongIndex = MaxIndex - 1;
            CurrentSong = CurrentSet.songList[SongIndex];
            var currentSongName = CurrentSong.title;
            CurrentSong = Song.loadSong(currentSongName); //get song off of file system
            string songHtml = CurrentSong.getHtml(DisplaySettings);
            webView.LoadHtml(songHtml);
        }
     }
}
