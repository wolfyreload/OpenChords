using Eto.Forms;
using OpenChords.CrossPlatform.Helpers;
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
        private int SongIndex;
        private int MaxIndex;
        private Song CurrentSong;
        private bool _SongChanged;
        private bool _SettingsChanged;
        private Functions.Metronome metronome1;
        protected ButtonMenuItem menuItemSongList;

        private WebView webView = new WebView();
        protected Button cmdMenu = new Button();
        protected Button cmdPreviousPage = new Button() { Text = "↑" };
        protected Button cmdNextPage = new Button() { Text = "↓" };
        protected Button cmdPreviousSong = new Button() { Text = "←" }; 
        protected Button cmdNextSong = new Button() { Text = "→" };

        private ShortcutSettings shortcutKeys;

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

        public frmPresent(Set set, DisplayAndPrintSettings settings, int startingSongIndex = 0)
        {
            CurrentSet = set;
            DisplaySettings = settings;
            SongIndex = startingSongIndex;
            Functions.WebServer.CurrentDisplayAndPrintSettings = settings;
            MaxIndex = CurrentSet.songList.Count;
            this.Title = "Present Set";
            buildUI();
        }

        private void buildUI()
        {
            shortcutKeys = Entities.ShortcutSettings.LoadSettings();

            Width = 500;
            Height = 500;
            WindowState = Eto.Forms.WindowState.Maximized;
            this.WindowStyle = Eto.Forms.WindowStyle.None;

            if (Settings.GlobalApplicationSettings.TouchScreenMode)
            {
                Splitter touchScreenUI = buildTouchscreenUI();
                Content = touchScreenUI;
            }
            else
            {
                Content = webView;
            }

            cmdPreviousSong.Click += menuItemPreviousSong_Executed;
            cmdNextSong.Click += menuItemNextSong_Executed;
            cmdPreviousPage.Click += cmdPreviousPage_Click;
            cmdNextPage.Click += cmdNextPage_Click;

            this.Icon = Graphics.Icon;

            var menuItemExit = MenuHelper.GetCommand("Exit", Graphics.ImageExit, shortcutKeys.ExitPresentation);
            menuItemExit.Executed += (s, e) => this.Close();

            // refresh item
            var menuItemRefresh = MenuHelper.GetCommand("Refresh", Graphics.ImageRefresh, shortcutKeys.RefreshPresentation);
            menuItemRefresh.Executed += menuItemRefresh_Executed;

            // size items
            var commandSongIncreaseSize = MenuHelper.GetCommand("Increase Font Size", Graphics.ImagMoveUp, shortcutKeys.IncreaseFontSize);
            commandSongIncreaseSize.Executed += commandSongIncreaseSize_Executed;
            var commandSongDecreaseSize = MenuHelper.GetCommand("Decrease Font Size", Graphics.ImageMoveDown, shortcutKeys.DecreaseFontSize);
            commandSongDecreaseSize.Executed += commandSongDecreaseSize_Executed;


            // key items
            var commandSongIncreaseKey = MenuHelper.GetCommand("Transpose Key Up", Graphics.ImagMoveUp, shortcutKeys.TransposeUp);
            commandSongIncreaseKey.Executed += commandSongIncreaseKey_Executed;
            var commandSongDecreaseKey = MenuHelper.GetCommand("Transpose Key Down", Graphics.ImageMoveDown, shortcutKeys.TransposeDown);
            commandSongDecreaseKey.Executed += commandSongDecreaseKey_Executed;
            var commandSongIncreaseCapo = MenuHelper.GetCommand("Capo Up", Graphics.ImagMoveUp, shortcutKeys.CapoUp);
            commandSongIncreaseCapo.Executed += commandSongIncreaseCapo_Executed;
            var commandSongDecreaseCapo = MenuHelper.GetCommand("Capo Down", Graphics.ImageMoveDown, shortcutKeys.CapoDown);
            commandSongDecreaseCapo.Executed += commandSongDecreaseCapo_Executed;

            // Navigation menu
            var menuItemNextSong = MenuHelper.GetCommand("Next Song", Graphics.ImageRight, shortcutKeys.GoToNextSong);
            menuItemNextSong.Executed += menuItemNextSong_Executed;
            var menuItemPreviousSong = MenuHelper.GetCommand("Previous Song", Graphics.ImageLeft, shortcutKeys.GoToPreviousSong);
            menuItemPreviousSong.Executed += menuItemPreviousSong_Executed;
            var menuItemNavigation = new ButtonMenuItem() { Text = "&Navigation", Items = { menuItemPreviousSong, menuItemNextSong }, Image = Graphics.ImageNavigation };

            //visibility menu
            var commandToggleChords = MenuHelper.GetCommand("Toggle Chords", Graphics.ImageChords, shortcutKeys.ToggleChords);
            commandToggleChords.Executed += (s, e) => { toggleChords(); };
            var commandToggleLyrics = MenuHelper.GetCommand("Toggle Lyrics", Graphics.ImageLyrics, shortcutKeys.ToggleLyrics);
            commandToggleLyrics.Executed += (s, e) => { toggleLyrics(); };
            var commandToggleNotes = MenuHelper.GetCommand("Toggle Notes", Graphics.ImageNotes, shortcutKeys.ToggleNotes);
            commandToggleNotes.Executed += (s, e) => { toggleNotes(); };

            //Metronome
            var commandToggleMetonome = MenuHelper.GetCommand("Toggle Metronome", Graphics.ImageMetronome, shortcutKeys.ToggleMetronome);
            commandToggleMetonome.Executed += commandToggleMetonome_Executed;

            //song List
            menuItemSongList = new ButtonMenuItem() { Text = "Song &List", Image = Graphics.ImageList };
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
                    new ButtonMenuItem() { Text = "&Size", Items = { commandSongIncreaseSize, commandSongDecreaseSize }, Image = Graphics.ImageSize},
                    new ButtonMenuItem() { Text = "&Key", Items = {commandSongIncreaseKey, commandSongDecreaseKey }, Image = Graphics.ImageKey},
                    new ButtonMenuItem() { Text = "&Capo", Items = { commandSongIncreaseCapo, commandSongDecreaseCapo }, Image = Graphics.ImageCapo},
                    menuItemNavigation,
                    new ButtonMenuItem() { Text = "&Visibility", Items = { commandToggleChords, commandToggleLyrics, commandToggleNotes }, Image = Graphics.ImageVisibility },
                    new ButtonMenuItem() { Text = "Other Options", Items = { commandToggleMetonome }, Image = Graphics.ImageOtherOptions },
                    menuItemSongList,
                    menuItemExit
                }
            };

            metronome1 = new OpenChords.Functions.Metronome();
            this.Closing += frmPresent_Closing;
            webView.Focus();

            drawSong();

            this.Topmost = Settings.GlobalApplicationSettings.ForceAlwaysOnTopWhenPresenting;
        }

        private Splitter buildTouchscreenUI()
        {
            var splitterView2 = new Splitter()
            {
                FixedPanel = SplitterFixedPanel.Panel1,
                RelativePosition = 20,
                Orientation = Orientation.Vertical,
                Panel1 = cmdPreviousPage,
                Panel2 = new Splitter()
                {
                    FixedPanel = SplitterFixedPanel.Panel2,
                    RelativePosition = 20,
                    Orientation = Orientation.Vertical,
                    Panel1 = webView,
                    Panel2 = cmdNextPage
                }
            };
            var splitterView1 = new Splitter()
            {
                FixedPanel = SplitterFixedPanel.Panel1,
                RelativePosition = 20,
                Panel1 = cmdPreviousSong,
                Panel2 = new Splitter()
                {
                    RelativePosition = 20,
                    FixedPanel = SplitterFixedPanel.Panel2,
                    Panel1 = splitterView2,
                    Panel2 = cmdNextSong
                }
            };
            return splitterView1;
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

        private void cmdNextPage_Click(object sender, EventArgs e)
        {
            webView.ExecuteScript("scrollDown()");
        }

        private void cmdPreviousPage_Click(object sender, EventArgs e)
        {
            webView.ExecuteScript("scrollUp()");
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

        private void drawSong()
        {
            if (MaxIndex == 0) return;
            if (SongIndex < 0) SongIndex = 0;
            if (SongIndex >= MaxIndex) SongIndex = MaxIndex - 1;
            CurrentSong = CurrentSet.songList[SongIndex];
            OpenChords.Functions.WebServer.CurrentSong = CurrentSong;
            string songHtml = CurrentSong.getHtml(DisplaySettings);
            webView.LoadHtml(songHtml);

            //update song position
            if (MaxIndex > 1)
                menuItemSongList.Text = string.Format("Song &List ({0}/{1})", SongIndex + 1, MaxIndex); 
        }
     }
}
