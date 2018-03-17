using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.CrossPlatform.SongEditor;
using OpenChords.Entities;
using OpenChords.CrossPlatform.Helpers;

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
        private ShortcutSettings shortcutKeys;

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
            refreshForm();

        }

        private void refreshForm()
        {
            initialSetup();

            
            setupHttpServer();

            shortcutKeys = Entities.ShortcutSettings.LoadSettings();


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
                    Orientation = Orientation.Horizontal,
                    Position = Helpers.FormHelper.getScreenXPercentageInPixels(15, this),
                    //Sets and song list
                    Panel1 = new Splitter()
                    {
                        Orientation = Orientation.Vertical,
                        Position = Helpers.FormHelper.getScreenYPercentageInPixels(40, this),

                        //sets
                        Panel1 = ucSetListPanel,
                        //song list
                        Panel2 = ucSongListPanel,
                    },
                    //song editor and song metadata
                    Panel2 = ucSongMetaDataPanel

                }

            };


            ucSongListPanel.refreshPanel();
            ucSetListPanel.refreshPanel();

            //file menu items
            var menuItemPreferences = MenuHelper.GetCommand("Preferences", Graphics.ImagePreferences);
            menuItemPreferences.Executed += menuItemPreferences_Executed;
            var menuItemQuit = MenuHelper.GetCommand("Quit", Graphics.ImageExit, shortcutKeys.QuitOpenChords);
            menuItemQuit.Executed += (s, e) => Application.Instance.Quit();

            // song menu items
            var menuItemNewSong = MenuHelper.GetCommand("New Song", Graphics.ImageNew, shortcutKeys.NewSong);
            menuItemNewSong.Executed += (s, e) => ucSongMetaDataPanel.NewSong();
            var menuItemSaveSong = MenuHelper.GetCommand("Save Song", Graphics.ImageSave, shortcutKeys.SaveSong);
            menuItemSaveSong.Executed += (s, e) => saveCurrentSong();
            var menuItemDeleteSong = MenuHelper.GetCommand("Delete Song", Graphics.ImageDelete);
            menuItemDeleteSong.Executed += (s, e) => { deleteCurrentlySelectedSong(); };
            var menuItemSongIncreaseKey = MenuHelper.GetCommand("Transpose Up", Graphics.ImagMoveUp, shortcutKeys.TransposeUp);
            menuItemSongIncreaseKey.Executed += (s, e) => ucSongMetaDataPanel.TransposeKeyUp();
            var menuItemSongDecreaseKey = MenuHelper.GetCommand("Transpose Down", Graphics.ImageMoveDown, shortcutKeys.TransposeDown);
            menuItemSongDecreaseKey.Executed += (s, e) => ucSongMetaDataPanel.TransposeKeyDown();
            var menuItemSongIncreaseCapo = MenuHelper.GetCommand("Capo Up", Graphics.ImagMoveUp, shortcutKeys.CapoUp);
            menuItemSongIncreaseCapo.Executed += (s, e) => ucSongMetaDataPanel.TransposeCapoUp();
            var menuItemSongDecreaseCapo = MenuHelper.GetCommand("Capo Down", Graphics.ImageMoveDown, shortcutKeys.CapoDown);
            menuItemSongDecreaseCapo.Executed += (s, e) => ucSongMetaDataPanel.TransposeCapoDown();
            var menuItemSongFixFormating = MenuHelper.GetCommand("Auto Format Song", Graphics.ImageRepairSong, shortcutKeys.AutoFormatSong);
            menuItemSongFixFormating.Executed += (s, e) => ucSongMetaDataPanel.FixFormatting();
            var menuItemSongRefeshList = MenuHelper.GetCommand("Refresh", Graphics.ImageRefresh, shortcutKeys.RefreshSongList);
            menuItemSongRefeshList.Executed += (s, e) => refreshSongList();
            var menuItemShowSongInExplorer = MenuHelper.GetCommand("Show In Explorer", Graphics.ImageExplore);
            menuItemShowSongInExplorer.Executed += (s, e) => ucSongListPanel.showSongInExplorer();

            var menuItemSongFileOperations = new ButtonMenuItem { Text = "File Operations", Items = { menuItemNewSong, menuItemSaveSong, menuItemDeleteSong, menuItemShowSongInExplorer }, Image = Graphics.ImageFileOperations };
            var menuItemKey = new ButtonMenuItem { Text = "Song Key", Items = { menuItemSongIncreaseKey, menuItemSongDecreaseKey }, Image = Graphics.ImageKey };
            var menuItemCapo = new ButtonMenuItem { Text = "Song Capo", Items = { menuItemSongIncreaseCapo, menuItemSongDecreaseCapo }, Image = Graphics.ImageCapo };

            //Sets menu items
            var menuItemSetNew = MenuHelper.GetCommand("New Set", Graphics.ImageNew);
            menuItemSetNew.Executed += (s, e) => ucSetListPanel.newSet();
            var commandSaveSet = MenuHelper.GetCommand("Save Set", Graphics.ImageSave);
            commandSaveSet.Executed += (s, e) => ucSetListPanel.saveSet();
            var commandRevertSet = MenuHelper.GetCommand("Revert Set", Graphics.ImageRevert);
            commandRevertSet.Executed += (s, e) => ucSetListPanel.revertSet();
            var menuItemSetDelete = MenuHelper.GetCommand("Delete Set", Graphics.ImageDelete);
            menuItemSetDelete.Executed += (s, e) => ucSetListPanel.deleteSet();
            var menuItemSetRefeshList = MenuHelper.GetCommand("Refresh", Graphics.ImageRefresh);
            menuItemSetRefeshList.Executed += (s, e) => ucSetListPanel.refreshPanel();
            var menuItemShowSetInExplorer = MenuHelper.GetCommand("Show In Explorer", Graphics.ImageExplore);
            menuItemShowSetInExplorer.Executed += (s, e) => ucSetListPanel.showSetInExplorer();


            var menuItemSetFileOperations = new ButtonMenuItem { Text = "File Operations", Items = { menuItemSetNew, commandSaveSet, commandRevertSet, menuItemSetDelete, menuItemShowSetInExplorer }, Image = Graphics.ImageFileOperations };


            //present menu items
            var menuItemPresentSong = MenuHelper.GetCommand("Present Song", Graphics.ImagePresentSong, shortcutKeys.PresentSong);
            menuItemPresentSong.Executed += (s, e) => ucSongMetaDataPanel.PresentSong();
            var menuItemPresentSet = MenuHelper.GetCommand("Present Set", Graphics.ImagePresentSet, shortcutKeys.PresentSet);
            menuItemPresentSet.Executed += (s, e) => ucSetListPanel.PresentSet();

            //exportMenu
            var commandExportToPrintSetHtml = MenuHelper.GetCommand("Current Set", Graphics.ImageSet, Tag: ExportOption.Set);
            commandExportToPrintSetHtml.Executed += exportToPrintHtml;
            var commandExportToPrintSongHtml = MenuHelper.GetCommand("Current Song", Graphics.ImageSong, shortcutKeys.PrintSongHtml, Tag: ExportOption.Song);
            commandExportToPrintSongHtml.Executed += exportToPrintHtml;
            var commandExportToPrintAllSongsHtml = MenuHelper.GetCommand("All Songs", Graphics.ImageAll, Tag: ExportOption.All);
            commandExportToPrintAllSongsHtml.Executed += exportToPrintHtml;
            var menuItemExportToPrintHtml = new ButtonMenuItem() { Text = "Export To &Print", Items = { commandExportToPrintSongHtml, commandExportToPrintSetHtml, commandExportToPrintAllSongsHtml }, Image = Graphics.ImagePrint };
            var commandExportToTabletSetHtml = MenuHelper.GetCommand("Current Set", Graphics.ImageSet, Tag: ExportOption.Set);
            commandExportToTabletSetHtml.Executed += exportToTabletHtml;
            var commandExportToTabletSongHtml = MenuHelper.GetCommand("Current Song", Graphics.ImageSong, Tag: ExportOption.Song);
            commandExportToTabletSongHtml.Executed += exportToTabletHtml;
            var commandExportToTabletAllSongsHtml = MenuHelper.GetCommand("All Songs", Graphics.ImageAll, Tag: ExportOption.All);
            commandExportToTabletAllSongsHtml.Executed += exportToTabletHtml;
            var menuItemExportToTabletHtml = new ButtonMenuItem() { Text = "Export To &Tablet", Items = { commandExportToTabletSongHtml, commandExportToTabletSetHtml, commandExportToTabletAllSongsHtml }, Image = Graphics.ImageHtml };
            var commandExportCurrentSetToOpenSong = MenuHelper.GetCommand("Current Set", Graphics.ImageOpenSong, shortcutKeys.ExportSetToOpenSong, Tag: ExportOption.Set);
            commandExportCurrentSetToOpenSong.Executed += exportToOpenSong;
            var menuItemExportToOpenSong = new ButtonMenuItem() { Text = "Export To &OpenSong", Items = { commandExportCurrentSetToOpenSong }, Image = Graphics.ImageOpenSong };
            var menuItemExportSetList = MenuHelper.GetCommand("Export Set List", Graphics.ImageList);
            menuItemExportSetList.Executed += (s, e) => ucSetListPanel.ExportToTextFile();

            //about menu
            var menuItemManual = MenuHelper.GetCommand("Help Documentation", Graphics.ImageHelp, shortcutKeys.ShowHelp);
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
                        Items = { menuItemSongFileOperations, menuItemKey, menuItemCapo, menuItemSongFixFormating, menuItemSongRefeshList }
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

        private void refreshSongList()
        {
            Entities.Song.ReloadAllSongs();
            ucSongListPanel.refreshPanel();
        }

        private void setupHttpServer()
        {
            if (Settings.GlobalApplicationSettings.HttpServerEnabled)
            {
                logger.Info("Starting Openchords web server");

                if (webserver != null)
                    webserver.Dispose();

                webserver = new OpenChords.Functions.WebServer(Settings.GlobalApplicationSettings.HttpServerPort);
                webserver.CannotStartHttpServerEvent += Webserver_CannotStartHttpServerEvent;
            }
            else
            {
                if (webserver != null)
                    webserver.Dispose();
            }
        }

        private void Webserver_CannotStartHttpServerEvent(object sender, string e)
        {
            Helpers.PopupMessages.ShowErrorMessage(e);
        }

        void menuItemPreferences_Executed(object sender, EventArgs e)
        {
            //check if there is already a preference screen open
            var windowList = Application.Instance.Windows;
            foreach (var window in windowList)
            {
                if (window.Title == "Preferences")
                {
                    window.Focus();
                    return;
                }
            }

            //get a song to use in the display/print/tablet settings preview
            Song tempSong = null;
            if (ucSongMetaDataPanel.CurrentSong.title != string.Empty)
                tempSong = ucSongMetaDataPanel.CurrentSong;
            else
                tempSong = Song.loadSong(Song.listOfAllSongs()[0]);

            //make sure that the set list is saved
            ucSetListPanel.saveSetState();

            //load preferences
            var formPreferences = new frmPreferences(tempSong);
            formPreferences.Show();

            //handle when preferences screen is closed
            formPreferences.Closing += FormPreferences_Closing;
        }

        private void FormPreferences_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            refreshForm();
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
                if (settings.settingsType == DisplayAndPrintSettingsType.PrintSettings)
                    UiHelper.ShowInFileManager(Settings.GlobalApplicationSettings.PrintFolder);
                else if (settings.settingsType == DisplayAndPrintSettingsType.TabletSettings)
                    UiHelper.ShowInFileManager(Settings.GlobalApplicationSettings.TabletFolder);
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

            //shutdown the webserver if one is running
            if (webserver != null)
                webserver.Dispose();
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
