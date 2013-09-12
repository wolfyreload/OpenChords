using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web
{
    public partial class Songs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void lnkSong_Click(object sender, EventArgs e)
        {

        }

        protected void grdSongs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var test = (LinkButton)e.Row.FindControl("lnkSong");
        }


    }
}