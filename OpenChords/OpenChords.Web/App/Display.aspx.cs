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
                    var list = new List<OpenChords.Entities.SongHtml>();
                    list.Add(html);
                    SongsHtml = list;
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
            if (SongsHtml.Count() == 1)
            {
                cmdPreviousSong.Visible = false;
                cmdNextSong.Visible = false;
            }
            else if (CurrentIndex == 0)
                cmdPreviousSong.Visible = false;
            else if (CurrentIndex == SongsHtml.Count() - 1)
                cmdNextSong.Visible = false;
            

        }

        protected void cmdNextSong_Click(object sender, EventArgs e)
        {
            CurrentIndex++;
            renderCurrentSong();
            setButtonStates();
        }

        protected void cmdGoBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SongName))
                Response.Redirect("~/App/Songs.aspx?Song="+SongName);
            else
                Response.Redirect("~/App/Sets.aspx?Set="+SetName);
        }

        private OpenChords.Entities.Song getSong()
        {
            var htmlSong = SongsHtml[CurrentIndex];
            var songName = htmlSong.Name;
            var song = OpenChords.Entities.Song.loadSong(songName);
            return song;
        }

        private void upDateHtmlSong(OpenChords.Entities.Song song)
        {
            var settingsPath = App_Code.Global.SettingsFileName;
            var html = song.getHtml(settingsPath);
            SongsHtml[CurrentIndex] = html;
            renderCurrentSong();
        }

        protected void cmdKeyUp_Click(object sender, EventArgs e)
        {
            var song = getSong();
            song.transposeKeyUp();
            upDateHtmlSong(song);
        }

        protected void cmdKeyDown_Click(object sender, EventArgs e)
        {

        }

        protected void cmdCapoUp_Click(object sender, EventArgs e)
        {

        }

        protected void cmdCapoDown_Click(object sender, EventArgs e)
        {

        }
    }
}