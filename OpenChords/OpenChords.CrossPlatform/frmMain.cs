using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.CrossPlatform.SongEditor;

namespace OpenChords.CrossPlatform
{
    
    /// <summary>
    /// Your application's main form
    /// </summary>
    public partial class frmMain : Form
    {
        protected SetListPanel ucSetListPanel = new SetListPanel();
        protected SongMetadataPanel ucSongMetaDataPanel = new SongMetadataPanel();
        protected SongListPanel ucSongListPanel = new SongListPanel();
        private int ScreenWidth;
        private int ScreenHeight;

        public frmMain()
        {
            logger.Info("Starting Openchords");

            //setup styles
            Eto.Style.Add<Button>("OpenFolder", button =>
            {
            });

            setup();

            this.Icon = Graphics.Icon;

            Title = "OpenChords";
            this.WindowState = Eto.Forms.WindowState.Maximized;
            ScreenWidth = (int)Screen.DisplayBounds.Width;
            ScreenHeight = (int)Screen.DisplayBounds.Height;

            // main content
            Content = new Panel
            {
                Width = 1024,
                Height = 768,

                // Main window
                Content = new Splitter()
                    {
                        Orientation = SplitterOrientation.Horizontal,
                        //Sets and song list
                        Panel1 = new Splitter()
                        {
                            Orientation = SplitterOrientation.Vertical,
                            //sets
                            Panel1 = new GroupBox() { Text = "Sets", Content = ucSetListPanel },
                            //song list
                            Panel2 = new GroupBox() { Text = "Songs", Content = ucSongListPanel },
                        },
                        //song editor and song metadata
                        Panel2 = ucSongMetaDataPanel

                    }

            };
            
            
            ucSongListPanel.refreshPanel();
            ucSetListPanel.refreshPanel();

            //file menu items
            var menuItemPreferences = new Command { MenuText = "Preferences" };
            menuItemPreferences.Executed += (s, e) => { new frmPreferences().Show(); };
            var menuItemQuit = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            menuItemQuit.Executed += (s, e) => Application.Instance.Quit();

            // song menu items
            var menuItemNewSong = new Command { MenuText = "New Song", Shortcut = Application.Instance.CommonModifier | Keys.N };
            menuItemNewSong.Executed += (s, e) => ucSongMetaDataPanel.NewSong();
            var menuItemSaveSong = new Command { MenuText = "Save Song", Shortcut = Application.Instance.CommonModifier | Keys.S };
            menuItemSaveSong.Executed += (s, e) => ucSongMetaDataPanel.SaveSong();
            var menuItemDeleteSong = new Command { MenuText = "Delete Song" };
            menuItemDeleteSong.Executed += (s, e) => { ucSongMetaDataPanel.DeleteSong(); ucSongListPanel.refreshPanel(); };
            var menuItemAdvancedSearch = new Command { MenuText = "Advanced Search" };
            menuItemAdvancedSearch.Executed += (s, e) => { throw new NotImplementedException(); };
            var menuItemSongIncreaseKey = new Command {MenuText = "Transpose Up", Shortcut = Application.Instance.CommonModifier | Keys.D0 };
            menuItemSongIncreaseKey.Executed += (s, e) => ucSongMetaDataPanel.TransposeKeyUp();
            var menuItemSongDecreaseKey = new Command {MenuText = "Transpose Down", Shortcut = Application.Instance.CommonModifier | Keys.D9 };
            menuItemSongDecreaseKey.Executed += (s, e) => ucSongMetaDataPanel.TransposeKeyDown();
            var menuItemSongIncreaseCapo = new Command { MenuText = "Capo Up", Shortcut = Application.Instance.CommonModifier | Keys.D8 };
            menuItemSongIncreaseCapo.Executed += (s, e) => ucSongMetaDataPanel.TransposeCapoUp();
            var menuItemSongDecreaseCapo = new Command { MenuText = "Capo Down", Shortcut = Application.Instance.CommonModifier | Keys.D7 };
            menuItemSongDecreaseCapo.Executed += (s, e) => ucSongMetaDataPanel.TransposeCapoDown();
            var menuItemSongFixFormating = new Command { MenuText = "Fix Song Formatting", Shortcut = Application.Instance.CommonModifier | Keys.F };
            menuItemSongFixFormating.Executed += (s, e) => ucSongMetaDataPanel.FixFormatting();
            
            var menuItemKey = new ButtonMenuItem { Text = "Song Key", Items = { menuItemSongIncreaseKey, menuItemSongDecreaseKey } };
            var menuItemCapo = new ButtonMenuItem { Text = "Song Capo", Items = { menuItemSongIncreaseCapo, menuItemSongDecreaseCapo } };


            //present menu items
            var menuItemPresentSong = new Command { MenuText = "Present Song", Shortcut = Keys.F11 };
            menuItemPresentSong.Executed += (s, e) => ucSongMetaDataPanel.PresentSong();
            var menuItemPresentSet = new Command { MenuText = "Present Set", Shortcut = Keys.F12 };
            menuItemPresentSet.Executed += (s, e) => ucSetListPanel.PresentSet();

            //exportMenu
            var commandExportSetToHtml = new Command { MenuText = "Se&t" };
            commandExportSetToHtml.Executed += (s, e) => ucSetListPanel.ExportToHtml();
            var commandExportSongToHtml = new Command { MenuText = "&Song" };
            commandExportSongToHtml.Executed += (s, e) => ucSongMetaDataPanel.ExportToHtml();
            var menuItemExportToHtml = new ButtonMenuItem() { Text = "To &Html", Items = { commandExportSetToHtml, commandExportSongToHtml } };
            //about menu
            var menuItemAbout = new Command { MenuText = "About..." };
            menuItemAbout.Executed += (s, e) => MessageBox.Show(this, "About my app...");

            // create menu
            Menu = new MenuBar
            {
                Items =
				{
					// File submenu
					new ButtonMenuItem 
                    { 
                        Text = "&File", 
                        Items = { menuItemPreferences }
                    },
                    new ButtonMenuItem()
                    {
                        Text = "&Song", 
                        Items = { menuItemNewSong, menuItemSaveSong, menuItemDeleteSong, menuItemAdvancedSearch, menuItemKey, menuItemCapo, menuItemSongFixFormating }
                    },
                    new ButtonMenuItem()
                    {
                        Text = "&Present", 
                        Items = { menuItemPresentSong, menuItemPresentSet }
                    },
                    new ButtonMenuItem()
                    {
                        Text = "&Export",
                        Items = { menuItemExportToHtml }
                    }
				},
                QuitItem = menuItemQuit,
                AboutItem = menuItemAbout
            };

            ucSongListPanel.Focus();
            
            // events
            this.Closing += frmMain_Closing;
            ucSongListPanel.SongChanged += SelectedSongChanged;
            ucSetListPanel.SongChanged += SelectedSongChanged;
            ucSongListPanel.AddSongToSet += (s, e) => ucSetListPanel.AddSongToSet(e);


        }

        void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ucSetListPanel.saveSetState();
        }

        void SelectedSongChanged(object sender, Entities.Song e)
        {
            ucSongMetaDataPanel.CurrentSong = e;
            ucSongMetaDataPanel.refreshPanel();
        }
    }
}
