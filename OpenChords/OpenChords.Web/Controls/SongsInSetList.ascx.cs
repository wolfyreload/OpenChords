using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web.Controls
{
    public partial class SongsInSetList : System.Web.UI.UserControl
    {
        protected SongsInSetList _linkToMyself;

        public event EventHandler SelectedSongChanged;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void DataBind()
        {
            lstViewSongs.DataBind();
        }

        public string SelectedSet
        { 
            get { return (string)this.ViewState["SongsInSet_SetName"]; } 
            set { this.ViewState["SongsInSet_SetName"] = value; } 
        }

        public OpenChords.Entities.Set CurrentSet
        {
            get { return (OpenChords.Entities.Set)(this.ViewState["SongsInSet_CurrentSet"]); }
            set { this.ViewState["SongsInSet_CurrentSet"] = value; }
        }

        public int SelectedIndex
        {
            get { return lstViewSongs.SelectedIndex; }
        }

        public int Count
        {
            get { return lstViewSongs.Items.Count; }
        }

        class RandomMappingClass
        {
            public string Name { get; set; }
        }

        protected void lstViewSongs_DataBinding(object sender, EventArgs e)
        {
            var source = CurrentSet.songList;
            lstViewSongs.DataSource = source;
        }

        public void AddSong(string songName)
        {
            CurrentSet.addSongToSet(songName);
            lstViewSongs.SelectedIndex = CurrentSet.indexOfCurrentSong;
            lstViewSongs.DataBind();
        }

        public void RemoveSong()
        {
            var index = lstViewSongs.SelectedIndex;
            var songName = (string)lstViewSongs.SelectedValue;
            CurrentSet.removeSongFromSet(index);
            lstViewSongs.SelectedIndex = CurrentSet.indexOfCurrentSong;
            lstViewSongs.DataBind();
        }

        public void MoveSongUp()
        {
            var index = lstViewSongs.SelectedIndex;
            CurrentSet.indexOfCurrentSong = index;
            CurrentSet.moveSongUp();
            index--;
            lstViewSongs.SelectedIndex = CurrentSet.indexOfCurrentSong;
            lstViewSongs.DataBind();
        }

        public void MoveSongDown()
        {
            var index = lstViewSongs.SelectedIndex;
            CurrentSet.indexOfCurrentSong = index;
            CurrentSet.moveSongDown();
            index++;
            lstViewSongs.SelectedIndex = CurrentSet.indexOfCurrentSong;
            lstViewSongs.DataBind();
        }

        protected void lstViewSongs_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            lstViewSongs.SelectedIndex = e.NewSelectedIndex;
            lstViewSongs.DataBind();
            if (SelectedSongChanged != null)
                SelectedSongChanged(this, e);
        }
    }
}