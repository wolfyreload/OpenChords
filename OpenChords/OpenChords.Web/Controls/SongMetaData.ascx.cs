using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web.Controls
{
    public partial class SongMetaData : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void fillInSongMeta(OpenChords.Entities.Song song)
        {
            txtTitle.Text = song.title;
        }
    }
}