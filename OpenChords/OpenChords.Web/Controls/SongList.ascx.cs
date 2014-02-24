using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web.Controls
{
    public partial class SongList : System.Web.UI.UserControl
    {
        protected SongList _myself;

        public event EventHandler NewSongSelected;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool _UseAdvangedSongSearch
        {
            get { return (bool)(this.ViewState["UseAdvancedSearch"] ?? false); }
            set { this.ViewState["UseAdvancedSearch"] = value; }
        }

        public override void DataBind()
        {
            lstViewSongs.DataBind();
        }

        public string SelectedValue
        {
            get { return (string)lstViewSongs.SelectedValue; }
            set
            {   
                var dummy = value;
                for (int i = 0; i < lstViewSongs.Items.Count; i++)
                {
                    var item = (LinkButton)lstViewSongs.Items[i].FindControl("lnkSets");
                    if (item.Text == value)
                    {
                        lstViewSongs.SelectedIndex = i;
                        lstViewSongs.DataBind();
                        break;
                    }

                }
                if (NewSongSelected != null)
                    NewSongSelected(this, new EventArgs());
            }
        }

       
        protected void cmdAdvancedSearch_Click(object sender, ImageClickEventArgs e)
        {
            _UseAdvangedSongSearch = true;
            lstViewSongs.DataBind();
        }

        protected void txtSearchSong_TextChanged(object sender, EventArgs e)
        {
            _UseAdvangedSongSearch = false;
            lstViewSongs.DataBind();
        }

        class RandomMappingClass
        {
            public string Name { get; set; }
        }

        protected void lstViewSongs_DataBinding(object sender, EventArgs e)
        {
            List<string> allSongs = null;

            if (_UseAdvangedSongSearch)
            {
                allSongs = OpenChords.Functions.SongSearch.search(txtSearchSong.Text).ToList();
            }
            else
            {
                allSongs = OpenChords.Entities.Song.listOfAllSongs();
                var filter = txtSearchSong.Text.ToUpper();
                if (!string.IsNullOrEmpty(filter))
                {
                    allSongs = allSongs.Where(a => a.ToUpper().Contains(filter)).ToList();
                }
            }

            var source = allSongs.Select(s => new RandomMappingClass() { Name = s });
            lstViewSongs.DataSource = source;
        }

        protected void lstViewSongs_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            lstViewSongs.SelectedIndex = e.NewSelectedIndex;
            lstViewSongs.DataBind();
            if (NewSongSelected != null)
                NewSongSelected(this, e);
        }


    }
}