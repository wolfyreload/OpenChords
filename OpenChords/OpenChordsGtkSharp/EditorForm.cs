using System;
using System.Collections.Generic;
using OpenChords.Entities;


namespace OpenChords
{
    public partial class EditorForm : Gtk.Window
    {
        private Song currentSong = new Song();
        private Set currentSet = new Set();
        private bool changesMade = false;
        private bool disableChangesMadeDetection = false;
        private List<string> songList = new List<string>();
        
        public EditorForm () : 
            base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            
            this.DoubleBuffered=true;
            
            AddWidgetEvents();
            
            refreshAll();
        }
        
        private void AddWidgetEvents()
        {   
            
            
        }

        private void refreshAll()
        {
            songList = IO.FileFolderFunctions.getDirectoryListingAsList(OpenChords.Settings.ExtAppsAndDir.songsFolder);   
            
            // Create a model that will hold two strings - Artist Name and Song Title
            Gtk.ListStore songListModel = new Gtk.ListStore (typeof (string));
 
            // Create a column for the artist name
            Gtk.TreeViewColumn songNameColumn = new Gtk.TreeViewColumn ();
            songNameColumn.Title = "Song List";
            
            // Create the text cell that will display the artist name
            Gtk.CellRendererText songNameCell = new Gtk.CellRendererText ();
            songNameColumn.PackStart (songNameCell, true);
            songNameColumn.AddAttribute(songNameCell, "text", 0);
            
            listSongs.AppendColumn(songNameColumn);
            
            foreach (var songName in songList)
                songListModel.AppendValues(songName);
           
            
            // Assign the model to the TreeView
            listSongs.Model = songListModel;
            
        }



        protected void OnRealized (object sender, System.EventArgs e)
        {
            
        }
        
        
        private void loadSong(string title)
        {
            currentSong = Song.loadSong(title);   
            txtTitle.Text = currentSong.title;
            txtKey.Text = currentSong.key;
            txtCapo.Text = currentSong.capo;
            txtAuthor.Text = currentSong.author;
            txtOrder.Text = currentSong.presentation;
            txtReference.Text = currentSong.ccli;
            
            
            
        }


     
   

        protected void OnListSongsRowActivated (object o, Gtk.RowActivatedArgs args)
        {
            var tree = (Gtk.TreeView)o;
            
            var renderedCell = (Gtk.CellRendererText)args.Column.Cells[0];
            var songTitle = renderedCell.Text;
            loadSong(songTitle);
            
        }
    }
}

