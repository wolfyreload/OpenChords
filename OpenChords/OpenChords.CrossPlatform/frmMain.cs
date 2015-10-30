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
                button.Width = 24;
                button.Height = 24;
            });

            setup();

            logger.Info("Starting Openchords web server");
            if (Settings.ExtAppsAndDir.HttpServerEnabled)
            {
                webserver = new OpenChords.Functions.WebServer(Settings.ExtAppsAndDir.HttpServerPort);
                webserver.CannotStartHttpServerEvent += Webserver_CannotStartHttpServerEvent;
            }

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
                        Position = Helpers.FormHelper.getScreenPercentageInPixels(15, this),
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
            var menuItemPreferences = new Command { MenuText = "Preferences", Image = Graphics.ImagePreferences };
            menuItemPreferences.Executed += menuItemPreferences_Executed;
            var menuItemQuit = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q, Image = Graphics.ImageExit };
            menuItemQuit.Executed += (s, e) => Application.Instance.Quit();

            // song menu items
            var menuItemNewSong = new Command { MenuText = "New Song", Shortcut = Application.Instance.CommonModifier | Keys.N, Image=Graphics.ImageNew };
            menuItemNewSong.Executed += (s, e) => ucSongMetaDataPanel.NewSong();
            var menuItemSaveSong = new Command { MenuText = "Save Song", Shortcut = Application.Instance.CommonModifier | Keys.S, Image = Graphics.ImageSave };
            menuItemSaveSong.Executed += (s, e) => saveCurrentSong();
            var menuItemDeleteSong = new Command { MenuText = "Delete Song", Image = Graphics.ImageDelete };
            menuItemDeleteSong.Executed += (s, e) => { deleteCurrentlySelectedSong(); };
            var menuItemAdvancedSearch = new Command { MenuText = "Advanced Search", Shortcut = Application.Instance.CommonModifier | Keys.F, Image = Graphics.ImageSearch };
            menuItemAdvancedSearch.Executed += menuItemAdvancedSearch_Executed;
            var menuItemSongIncreaseKey = new Command { MenuText = "Transpose Up", Shortcut = Application.Instance.CommonModifier | Keys.D0, Image = Graphics.ImagMoveUp };
            menuItemSongIncreaseKey.Executed += (s, e) => ucSongMetaDataPanel.TransposeKeyUp();
            var menuItemSongDecreaseKey = new Command { MenuText = "Transpose Down", Shortcut = Application.Instance.CommonModifier | Keys.D9, Image = Graphics.ImageMoveDown };
            menuItemSongDecreaseKey.Executed += (s, e) => ucSongMetaDataPanel.TransposeKeyDown();
            var menuItemSongIncreaseCapo = new Command { MenuText = "Capo Up", Shortcut = Application.Instance.CommonModifier | Keys.D8, Image = Graphics.ImagMoveUp };
            menuItemSongIncreaseCapo.Executed += (s, e) => ucSongMetaDataPanel.TransposeCapoUp();
            var menuItemSongDecreaseCapo = new Command { MenuText = "Capo Down", Shortcut = Application.Instance.CommonModifier | Keys.D7, Image = Graphics.ImageMoveDown };
            menuItemSongDecreaseCapo.Executed += (s, e) => ucSongMetaDataPanel.TransposeCapoDown();
            var menuItemSongFixFormating = new Command { MenuText = "Auto Format Song", Shortcut = Application.Instance.CommonModifier | Keys.R, Image = Graphics.ImageRepairSong };
            menuItemSongFixFormating.Executed += (s, e) => ucSongMetaDataPanel.FixFormatting();
            var menuItemSongRefeshList = new Command { MenuText = "Refresh", Image = Graphics.ImageRefresh };
            menuItemSongRefeshList.Executed += (s, e) => ucSongListPanel.refreshPanel();
            var menuItemShowSongInExplorer = new Command { MenuText = "Show In Explorer", Image = Graphics.ImageExplore };
            menuItemShowSongInExplorer.Executed += (s, e) => ucSongListPanel.showSongInExplorer();

            var menuItemSongFileOperations = new ButtonMenuItem { Text = "File Operations", Items = { menuItemNewSong, menuItemSaveSong, menuItemDeleteSong, menuItemShowSongInExplorer }, Image = Graphics.ImageFileOperations };
            var menuItemKey = new ButtonMenuItem { Text = "Song Key", Items = { menuItemSongIncreaseKey, menuItemSongDecreaseKey }, Image = Graphics.ImageKey };
            var menuItemCapo = new ButtonMenuItem { Text = "Song Capo", Items = { menuItemSongIncreaseCapo, menuItemSongDecreaseCapo }, Image = Graphics.ImageCapo };

            //Sets menu items
            var menuItemSetNew = new Command() { MenuText = "New Set", Image = Graphics.ImageNew };
            menuItemSetNew.Executed += (s, e) => ucSetListPanel.newSet();
            var commandSaveSet = new Command() { MenuText = "Save Set", Image = Graphics.ImageSave };
            commandSaveSet.Executed += (s, e) => ucSetListPanel.saveSet();
            var commandRevertSet = new Command() { MenuText = "Revert Set", Image = Graphics.ImageRevert };
            commandRevertSet.Executed += (s, e) => ucSetListPanel.revertSet();
            var menuItemSetDelete = new Command() { MenuText = "Delete Set", Image = Graphics.ImageDelete };
            menuItemSetDelete.Executed += (s, e) => ucSetListPanel.deleteSet();
            var menuItemSetRefeshList = new Command { MenuText = "Refresh", Image = Graphics.ImageRefresh };
            menuItemSetRefeshList.Executed += (s, e) => ucSetListPanel.refreshPanel();
            var menuItemShowSetInExplorer = new Command { MenuText = "Show In Explorer", Image = Graphics.ImageExplore };
            menuItemShowSetInExplorer.Executed += (s, e) => ucSetListPanel.showSetInExplorer();


            var menuItemSetFileOperations = new ButtonMenuItem { Text = "File Operations", Items = { menuItemSetNew, commandSaveSet, commandRevertSet, menuItemSetDelete, menuItemShowSetInExplorer }, Image = Graphics.ImageFileOperations };
      

            //present menu items
            var menuItemPresentSong = new Command { MenuText = "Present Song", Shortcut = Keys.F11, Image = Graphics.ImagePresentSong };
            menuItemPresentSong.Executed += (s, e) => ucSongMetaDataPanel.PresentSong();
            var menuItemPresentSet = new Command { MenuText = "Present Set", Shortcut = Keys.F12, Image = Graphics.ImagePresentSet };
            menuItemPresentSet.Executed += (s, e) => ucSetListPanel.PresentSet();

            //exportMenu
            var commandExportToPrintSetHtml = new Command { MenuText = "Current Set", Tag = ExportOption.Set, Image = Graphics.ImageSet };
            commandExportToPrintSetHtml.Executed += exportToPrintHtml;
            var commandExportToPrintSongHtml = new Command { MenuText = "Current Song", Tag = ExportOption.Song, Shortcut = Application.Instance.CommonModifier | Keys.P, Image = Graphics.ImageSong };
            commandExportToPrintSongHtml.Executed += exportToPrintHtml;
            var commandExportToPrintAllSongsHtml = new Command { MenuText = "All Songs", Tag = ExportOption.All, Image = Graphics.ImageAll };
            commandExportToPrintAllSongsHtml.Executed += exportToPrintHtml;
            var menuItemExportToPrintHtml = new ButtonMenuItem() { Text = "Export To &Print", Items = { commandExportToPrintSongHtml, commandExportToPrintSetHtml, commandExportToPrintAllSongsHtml }, Image = Graphics.ImagePrint };
            var commandExportToTabletSetHtml = new Command { MenuText = "Current Set", Tag = ExportOption.Set, Image = Graphics.ImageSet };
            commandExportToTabletSetHtml.Executed += exportToTabletHtml;
            var commandExportToTabletSongHtml = new Command { MenuText = "Current Song", Tag = ExportOption.Song, Image = Graphics.ImageSong };
            commandExportToTabletSongHtml.Executed += exportToTabletHtml;
            var commandExportToTabletAllSongsHtml = new Command { MenuText = "All Songs", Tag = ExportOption.All, Image = Graphics.ImageAll };
            commandExportToTabletAllSongsHtml.Executed += exportToTabletHtml;
            var menuItemExportToTabletHtml = new ButtonMenuItem() { Text = "Export To &Tablet", Items = { commandExportToTabletSongHtml, commandExportToTabletSetHtml, commandExportToTabletAllSongsHtml }, Image = Graphics.ImageHtml };
            var commandExportCurrentSetToOpenSong = new Command { MenuText = "Current Set", Tag = ExportOption.Set, Image = Graphics.ImageSet };
            commandExportCurrentSetToOpenSong.Executed += exportToOpenSong;
            var menuItemExportToOpenSong = new ButtonMenuItem() { Text = "Export To &OpenSong", Items = { commandExportCurrentSetToOpenSong }, Image = Graphics.ImageOpenSong };
            var menuItemExportSetList = new Command { MenuText = "Export Set List", Image = Graphics.ImageList };
            menuItemExportSetList.Executed += (s, e) => ucSetListPanel.ExportToTextFile();
            
            //about menu
            var menuItemManual = new Command { MenuText = "Help Documentation", Shortcut = Keys.F1, Image = Graphics.ImageHelp };
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
            ucSongListPanel.SongDeleting += (s, e) => deleteCurrentlySelectedSong();

        }

        private void Webserver_CannotStartHttpServerEvent(object sender, string e)
        {
            Helpers.PopupMessages.ShowErrorMessage(e);
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
            ucSongMetaDataPanel.ChangeToSong(e);
            ucSongMetaDataPanel.refreshPanel();
        }

        private void deleteCurrentlySelectedSong()
        {
            bool songDeleted = ucSongMetaDataPanel.DeleteSong();
            if (songDeleted)
            {
                ucSongListPanel.refreshPanel();
                ucSetListPanel.refreshPanel();
            }
        }

        private void saveCurrentSong()
        {
            bool newSongCreated = ucSongMetaDataPanel.SaveSong();
            if (newSongCreated)
            {
                ucSongListPanel.refreshPanel();
                ucSetListPanel.refreshSongList();
            }
        }

        void showManual()
        {
            logger.Info("Clicked");
            var formManual = new frmHelpDocumentation();
            formManual.Show();
        }
    }
}
