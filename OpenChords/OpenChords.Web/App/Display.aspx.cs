using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web.App
{
    public partial class Display2 : System.Web.UI.Page
    {

        private string SongName
        { get { return (string)this.Request.Params["Song"]; } }

        private string SetName
        { get { return (string)this.Request.Params["Set"]; } }

        private List<OpenChords.Entities.SongHtml> SongsHtml
        {
            get
            {
                return (List<OpenChords.Entities.SongHtml>)this.Session["SongHtml"];
            }
            set
            {
                this.Session["SongHtml"] = value;
            }
        }

        private int CurrentIndex
        {
            get
            {
                return (int)(this.ViewState["SongIndex"] ?? 0);
            }
            set
            {
                this.ViewState["SongIndex"] = value;
            }
        }
        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var settingsPath = App_Code.Global.SettingsFileName;
                
                if (SongName != null)
                {
                    var song = OpenChords.Entities.Song.loadSong(SongName);
                    var html = song.getHtml(settingsPath);
                    SongsHtml = html;
                }
                else if (SetName != null)
                {
                    var set = OpenChords.Entities.Set.loadSet(SetName);
                    var html = set.getHtml(settingsPath);
                    SongsHtml = html;
                }
                renderCurrentSong();
                setButtonStates();
            }
        }

        private void renderCurrentSong()
        {
            var htmlSong = SongsHtml[CurrentIndex];
            lblSongName.Text = htmlSong.Name;
            lblSongOrder.Text = htmlSong.Order;
            litSongContent.Mode = LiteralMode.PassThrough;
            litSongContent.Text = htmlSong.Html;
            
        }

        protected void cmdPreviousSong_Click(object sender, EventArgs e)
        {
            CurrentIndex--;
            renderCurrentSong();
            setButtonStates();
        }

        private void setButtonStates()
        {
            cmdPreviousSong.Visible = true;
            cmdNextSong.Visible = true;
            if (CurrentIndex == 0)
                cmdPreviousSong.Visible = false;
            else if (CurrentIndex == SongsHtml.Count() - 1)
                cmdNextSong.Visible = false;
            else if (SongsHtml.Count() == 1)
            {
                cmdPreviousSong.Visible = false;
                cmdNextSong.Visible = false;
            }

        }

        protected void cmdNextSong_Click(object sender, EventArgs e)
        {
            CurrentIndex++;
            renderCurrentSong();
            setButtonStates();
        }
    }
}