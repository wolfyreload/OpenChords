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
            lstSongs.DataBind();
        }

        public string SelectedValue
        {
            get { return lstSongs.SelectedValue; }
        }



        protected void lstSongs_DataBinding(object sender, EventArgs e)
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

            lstSongs.DataSource = allSongs;
        }

        protected void cmdAdvancedSearch_Click(object sender, ImageClickEventArgs e)
        {
            _UseAdvangedSongSearch = true;
            lstSongs.DataBind();
        }

        protected void txtSearchSong_TextChanged(object sender, EventArgs e)
        {
            _UseAdvangedSongSearch = false;
            lstSongs.DataBind();
        }


    }
}