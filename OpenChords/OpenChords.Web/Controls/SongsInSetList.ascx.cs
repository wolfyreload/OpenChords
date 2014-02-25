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

        public List<string> SongsIncurrentSet
        {
            get { return (List<string>)(this.ViewState["SongsInSet_CurrentSet"]); }
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
            var source = SongsIncurrentSet.Select(s => new RandomMappingClass() { Name = s });
            lstViewSongs.DataSource = source;
        }

        public void AddSong(string songName)
        {
            SongsIncurrentSet.Add(songName);
            lstViewSongs.SelectedIndex = SongsIncurrentSet.Count - 1;
            lstViewSongs.DataBind();
        }

        public void RemoveSong()
        {
            var index = lstViewSongs.SelectedIndex;
            if (index < 0 || SongsIncurrentSet.Count==0) return;
            SongsIncurrentSet.RemoveAt(index);
            index--;
            if (index < 0) index = 0;
            lstViewSongs.SelectedIndex = index;
            lstViewSongs.DataBind();
        }

        public void MoveSongUp()
        {
            var index = lstViewSongs.SelectedIndex;
            var song = SongsIncurrentSet[index];
            SongsIncurrentSet.RemoveAt(index);
            index--;
            SongsIncurrentSet.Insert(index, song);
            lstViewSongs.SelectedIndex = index;
            lstViewSongs.DataBind();
        }

        public void MoveSongDown()
        {
            var index = lstViewSongs.SelectedIndex;
            var song = SongsIncurrentSet[index];
            SongsIncurrentSet.RemoveAt(index);
            index++;
            SongsIncurrentSet.Insert(index, song);
            lstViewSongs.SelectedIndex = index;
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