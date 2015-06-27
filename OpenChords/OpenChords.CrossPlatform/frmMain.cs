using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.CrossPlatform.SongEditor;
using OpenChords.Entities;

namespace OpenChords.CrossPlatform
{

    /// <summary>
    /// Your application's main form
    /// </summary>
    public partial class frmMain : Form
    {
        enum ExportOption { All, Set, Song };
        protected SetListPanel ucSetListPanel;
        protected SongMetadataPanel ucSongMetaDataPanel;
        protected SongListPanel ucSongListPanel;
        private Functions.WebServer webserver;

        public frmMain()
        {
            logger.Info("Starting Openchords");

            //add a style to the table layout
            Eto.Style.Add<TableLayout>("padded-table", table =>
            {
                table.Padding = new Padding(10);
                table.Spacing = new Size(5, 5);
            });


            //setup styles
            Eto.Style.Add<Button>("OpenFolder", button =>
            {
            });

            setup();

            logger.Info("Starting Openchords web server");
            if (Settings.ExtAppsAndDir.HttpServerEnabled)
                webserver = new OpenChords.Functions.WebServer(Settings.ExtAppsAndDir.HttpServerPort);


            this.Icon = Graphics.Icon;

            Title = "OpenChords";
            this.WindowState = Eto.Forms.WindowState.Maximized;

            ucSetListPanel = new SetListPanel();
            ucSongMetaDataPanel = new SongMetadataPanel();
            ucSongListPanel = new SongListPanel();

            // main content
            Content = new Panel
            {
                // Main window
                Content = new Splitter()
                    {
                        Orientation = SplitterOrientation.Horizontal,
                        Position = Helpers.getScreenPercentageInPixels(15, this),
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
            menuItemPreferences.Executed += menuItemPreferences_Executed;
            var menuItemQuit = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            menuItemQuit.Executed += (s, e) => Application.Instance.Quit();

            // song menu items
            var menuItemNewSong = new Command { MenuText = "New Song", Shortcut = Application.Instance.CommonModifier | Keys.N };
            menuItemNewSong.Executed += (s, e) => ucSongMetaDataPanel.NewSong();
            var menuItemSaveSong = new Command { MenuText = "Save Song", Shortcut = Application.Instance.CommonModifier | Keys.S };
            menuItemSaveSong.Executed += (s, e) => ucSongMetaDataPanel.SaveSong();
            var menuItemDeleteSong = new Command { MenuText = "Delete Song" };
            menuItemDeleteSong.Executed += (s, e) => { ucSongMetaDataPanel.DeleteSong(); ucSongListPanel.refreshPanel(); };
            var menuItemAdvancedSearch = new Command { MenuText = "Advanced Search", Shortcut = Application.Instance.CommonModifier | Keys.F };
            menuItemAdvancedSearch.Executed += menuItemAdvancedSearch_Executed;
            var menuItemSongIncreaseKey = new Command { MenuText = "Transpose Up", Shortcut = Application.Instance.CommonModifier | Keys.D0 };
            menuItemSongIncreaseKey.Executed += (s, e) => ucSongMetaDataPanel.TransposeKeyUp();
            var menuItemSongDecreaseKey = new Command { MenuText = "Transpose Down", Shortcut = Application.Instance.CommonModifier | Keys.D9 };
            menuItemSongDecreaseKey.Executed += (s, e) => ucSongMetaDataPanel.TransposeKeyDown();
            var menuItemSongIncreaseCapo = new Command { MenuText = "Capo Up", Shortcut = Application.Instance.CommonModifier | Keys.D8 };
            menuItemSongIncreaseCapo.Executed += (s, e) => ucSongMetaDataPanel.TransposeCapoUp();
            var menuItemSongDecreaseCapo = new Command { MenuText = "Capo Down", Shortcut = Application.Instance.CommonModifier | Keys.D7 };
            menuItemSongDecreaseCapo.Executed += (s, e) => ucSongMetaDataPanel.TransposeCapoDown();
            var menuItemSongFixFormating = new Command { MenuText = "Auto Format Song", Shortcut = Application.Instance.CommonModifier | Keys.R };
            menuItemSongFixFormating.Executed += (s, e) => ucSongMetaDataPanel.FixFormatting();
            var menuItemSongRefeshList = new Command { MenuText = "Refresh" };
            menuItemSongRefeshList.Executed += (s, e) => ucSongListPanel.refreshPanel();
            var menuItemShowSongInExplorer = new Command { MenuText = "Show In Explorer" };
            menuItemShowSongInExplorer.Executed += (s, e) => ucSongListPanel.showSongInExplorer();

            var menuItemSongFileOperations = new ButtonMenuItem { Text = "File Operations", Items = { menuItemNewSong, menuItemSaveSong, menuItemDeleteSong, menuItemShowSongInExplorer } };
            var menuItemKey = new ButtonMenuItem { Text = "Song Key", Items = { menuItemSongIncreaseKey, menuItemSongDecreaseKey } };
            var menuItemCapo = new ButtonMenuItem { Text = "Song Capo", Items = { menuItemSongIncreaseCapo, menuItemSongDecreaseCapo } };

            //Sets menu items
            var menuItemSetNew = new Command() { MenuText = "New Set" };
            menuItemSetNew.Executed += (s, e) => ucSetListPanel.newSet();
            var commandSaveSet = new Command() { MenuText = "Save Set" };
            commandSaveSet.Executed += (s, e) => ucSetListPanel.saveSet();
            var commandRevertSet = new Command() { MenuText = "Revert Set" };
            commandRevertSet.Executed += (s, e) => ucSetListPanel.revertSet();
            var menuItemSetDelete = new Command() { MenuText = "Delete Set" };
            menuItemSetDelete.Executed += (s, e) => ucSetListPanel.deleteSet();
            
            var menuItemSetRefeshList = new Command { MenuText = "Refresh" };
            menuItemSetRefeshList.Executed += (s, e) => ucSetListPanel.refreshPanel();
            var menuItemShowSetInExplorer = new Command { MenuText = "Show In Explorer" };
            menuItemShowSetInExplorer.Executed += (s, e) => ucSetListPanel.showSetInExplorer();


            var menuItemSetFileOperations = new ButtonMenuItem { Text = "File Operations", Items = { menuItemSetNew, commandSaveSet, commandRevertSet, menuItemSetDelete, menuItemShowSetInExplorer } };
      

            //present menu items
            var menuItemPresentSong = new Command { MenuText = "Present Song", Shortcut = Keys.F11 };
            menuItemPresentSong.Executed += (s, e) => ucSongMetaDataPanel.PresentSong();
            var menuItemPresentSet = new Command { MenuText = "Present Set", Shortcut = Keys.F12 };
            menuItemPresentSet.Executed += (s, e) => ucSetListPanel.PresentSet();

            //exportMenu
            var commandExportToPrintSetHtml = new Command { MenuText = "Current Set", Tag = ExportOption.Set };
            commandExportToPrintSetHtml.Executed += exportToPrintHtml;
            var commandExportToPrintSongHtml = new Command { MenuText = "Current Song", Tag = ExportOption.Song, Shortcut = Application.Instance.CommonModifier | Keys.P };
            commandExportToPrintSongHtml.Executed += exportToPrintHtml;
            var commandExportToPrintAllSongsHtml = new Command { MenuText = "All Songs", Tag = ExportOption.All };
            commandExportToPrintAllSongsHtml.Executed += exportToPrintHtml;
            var menuItemExportToPrintHtml = new ButtonMenuItem() { Text = "Export To &Print", Items = { commandExportToPrintSetHtml, commandExportToPrintSongHtml, commandExportToPrintAllSongsHtml } };
            var commandExportToTabletSetHtml = new Command { MenuText = "Current Set", Tag = ExportOption.Set };
            commandExportToTabletSetHtml.Executed += exportToTabletHtml;
            var commandExportToTabletSongHtml = new Command { MenuText = "Current Song", Tag = ExportOption.Song };
            commandExportToTabletSongHtml.Executed += exportToTabletHtml;
            var commandExportToTabletAllSongsHtml = new Command { MenuText = "All Songs", Tag = ExportOption.All };
            commandExportToTabletAllSongsHtml.Executed += exportToTabletHtml;
            var menuItemExportToTabletHtml = new ButtonMenuItem() { Text = "Export To &Tablet", Items = { commandExportToTabletSetHtml, commandExportToTabletSongHtml, commandExportToTabletAllSongsHtml } };
            var commandExportCurrentSetToOpenSong = new Command { MenuText = "Current Set", Tag = ExportOption.Set };
            commandExportCurrentSetToOpenSong.Executed += exportToOpenSong;
            var menuItemExportToOpenSong = new ButtonMenuItem() { Text = "Export To &OpenSong", Items = { commandExportCurrentSetToOpenSong } };
            var menuItemExportSetList = new Command { MenuText = "Export Set List" };
            menuItemExportSetList.Executed += (s, e) => ucSetListPanel.ExportToTextFile();
            
            //about menu
            var menuItemManual = new Command { MenuText = "Manual", Shortcut = Keys.F1 };
            menuItemManual.Executed += (s, e) => showManual();

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
                        Items = { menuItemSongFileOperations, menuItemAdvancedSearch, menuItemKey, menuItemCapo, menuItemSongFixFormating, menuItemSongRefeshList }
                    },
                    new ButtonMenuItem()
                    {
                        Text = "Se&ts", 
                        Items = { menuItemSetFileOperations, menuItemSetRefeshList }
                    },
                    new ButtonMenuItem()
                    {
                        Text = "&Present", 
                        Items = { menuItemPresentSong, menuItemPresentSet }
                    },
                    new ButtonMenuItem()
                    {
                        Text = "&Export",
                        Items = { menuItemExportToPrintHtml, menuItemExportToTabletHtml, menuItemExportToOpenSong, menuItemExportSetList }
                    },
                    new ButtonMenuItem()
                    {
                        Text = "&Help",
                        Items = { menuItemManual }
                    }
				},
                QuitItem = menuItemQuit,
             
            };

            ucSongListPanel.Focus();

            // events
            this.Closing += frmMain_Closing;
            ucSongListPanel.SongChanged += SelectedSongChanged;
            ucSetListPanel.SongChanged += SelectedSongChanged;
            ucSongListPanel.AddSongToSet += (s, e) => ucSetListPanel.AddSongToSet(e);


        }

       
        void menuItemPreferences_Executed(object sender, EventArgs e)
        {
            Song tempSong = null;
            if (ucSongMetaDataPanel.CurrentSong.title != string.Empty)
                tempSong = ucSongMetaDataPanel.CurrentSong;
            else
                tempSong = Song.loadSong(Song.listOfAllSongs()[0]);

            new frmPreferences(tempSong).Show();
        }

        void menuItemAdvancedSearch_Executed(object sender, EventArgs e)
        {
            var advancedSearchForm = new frmAdvancedSearch();
            advancedSearchForm.AddSongToSet += (s, e2) => ucSetListPanel.AddSongToSet(e2);
            advancedSearchForm.Show(); 
        }

        private void exportToOpenSong(object sender, EventArgs e)
        {
            var option = (ExportOption)((sender as Command).Tag);
          
            if (option == ExportOption.All)
            {
                throw new NotImplementedException();
            }
            else if (option == ExportOption.Set)
                ucSetListPanel.ExportToOpenSong();
            else if (option == ExportOption.Song)
                throw new NotImplementedException();
          
            
        }

        private void exportToTabletHtml(object sender, EventArgs e)
        {
            var option = (ExportOption)((sender as Command).Tag);
            exportToHtml(option, DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings));
        }

        private void exportToPrintHtml(object sender, EventArgs e)
        {
            var option = (ExportOption)((sender as Command).Tag);
            exportToHtml(option, DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.PrintSettings));
        }

        private void exportToHtml(ExportOption option, DisplayAndPrintSettings settings)
        {
            if (option == ExportOption.All)
            {
                foreach (var song in Song.listOfAllSongs())
                {
                    Song.loadSong(song).ExportToHtml(settings);
                }
            }
            else if (option == ExportOption.Set)
                ucSetListPanel.ExportToHtml(settings);
            else if (option == ExportOption.Song)
                ucSongMetaDataPanel.ExportToHtml(settings);
        }

        void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ucSetListPanel.saveSetState();
            ucSetListPanel.saveSetBeforeClosing();
        }

        void SelectedSongChanged(object sender, Entities.Song e)
        {
            ucSongMetaDataPanel.CurrentSong = e;
            ucSongMetaDataPanel.refreshPanel();
        }

        void showManual()
        {
            logger.Info("Clicked");
            var formManual = new frmHelpDocumentation();
            formManual.Show();
        }
    }
}
