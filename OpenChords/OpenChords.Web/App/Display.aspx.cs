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

        private OpenChords.Entities.Set _Set
        {
            get
            {
                return (OpenChords.Entities.Set)this.Session["Set"];
            }
            set
            {
                this.Session["Set"] = value;
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
                
                if (SongName != null)
                {
                    var song = OpenChords.Entities.Song.loadSong(SongName);
                    _Set = new OpenChords.Entities.Set();
                    _Set.addSongToSet(song);
                }
                else if (SetName != null)
                {
                    _Set = OpenChords.Entities.Set.loadSet(SetName);
                    _Set.loadAllSongs();
                }
                renderCurrentSong();
                setButtonStates();
            }
        }

        private void renderCurrentSong()
        {
            var settingsPath = App_Code.Global.SettingsFileName;
            var song = _Set.songList[CurrentIndex];
            var settings = OpenChords.Entities.DisplayAndPrintSettings.loadSettings(Entities.DisplayAndPrintSettingsType.DisplaySettings, settingsPath);
            var htmlSong = song.getHtml(settings); 
            litSongContent.Mode = LiteralMode.PassThrough;
            litSongContent.Text = htmlSong;
            
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
            if (_Set.songList.Count() == 1)
            {
                cmdPreviousSong.Visible = false;
                cmdNextSong.Visible = false;
            }
            else if (CurrentIndex == 0)
                cmdPreviousSong.Visible = false;
            else if (CurrentIndex == _Set.songList.Count() - 1)
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
            return _Set.songList[CurrentIndex];
        }

        protected void cmdKeyUp_Click(object sender, EventArgs e)
        {
            var song = getSong();
            song.transposeKeyUp();
            renderCurrentSong();
        }

        protected void cmdKeyDown_Click(object sender, EventArgs e)
        {
            var song = getSong();
            song.transposeKeyDown();
            renderCurrentSong();
        }

        protected void cmdCapoUp_Click(object sender, EventArgs e)
        {
            var song = getSong();
            song.capoUp();
            renderCurrentSong();
        }

        protected void cmdCapoDown_Click(object sender, EventArgs e)
        {
            var song = getSong();
            song.capoDown();
            renderCurrentSong();
        }

        protected void cmdShowOptions_Click(object sender, EventArgs e)
        {
            pnlOtherOptions.Visible = true;
            cmdShowOptions.Visible = false;
        }

        protected void cmdHideOptions_Click(object sender, EventArgs e)
        {
            pnlOtherOptions.Visible = false;
            cmdShowOptions.Visible = true;
        }
    }
}