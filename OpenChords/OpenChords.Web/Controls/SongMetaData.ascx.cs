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
        protected SongMetaData _linkToMyself;

        public event System.EventHandler SongKeyIncreased;
        public event System.EventHandler SongKeyDecreased;
        public event System.EventHandler SongCapoIncreased;
        public event System.EventHandler SongCapoDecreased;
        public event System.EventHandler PresentationOrderChanged;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlTempo.DataSource = OpenChords.Entities.Song.TempoOptions();
                ddlSignature.DataSource = OpenChords.Entities.Song.TimeSignatureOptions();
            }
            ddlTempo.DataBind();
            ddlSignature.DataBind();
        }

        public void fillInSongMeta(OpenChords.Entities.Song song)
        {
            
            txtTitle.Text = song.title;
            txtOrder.Text = song.presentation;
            txtAuthor.Text = song.author;
            txtCopyright.Text = song.copyright;            
            txtCcli.Text = song.ccli;
            txtReference.Text = song.hymn_number;
            txtKey.Text = song.key;
            txtCapo.Text = song.capo;

            if (song.tempo != "")
                ddlTempo.Text = song.tempo;
            else
                ddlTempo.SelectedIndex = 0;

            if (song.time_sig != "")
                ddlSignature.Text = song.time_sig;
            else
                ddlSignature.SelectedIndex = 0;

        }

        public OpenChords.Entities.Song getSongObject()
        {
            var song = new OpenChords.Entities.Song();
            song.title = txtTitle.Text;
            song.presentation = txtOrder.Text;
            song.author = txtAuthor.Text;
            song.copyright = txtCopyright.Text;
            song.ccli = txtCcli.Text;
            song.hymn_number = txtReference.Text;
            song.key = txtKey.Text;
            song.capo = txtCapo.Text;

            if (ddlTempo.SelectedIndex > 0)
                song.tempo = ddlTempo.Text;
            if (ddlSignature.SelectedIndex > 0)
                song.time_sig = ddlSignature.Text;

            return song;
        }

        protected void imgCapoUp_Click(object sender, ImageClickEventArgs e)
        {
            if (SongCapoIncreased != null)
                SongCapoIncreased(this, e);
        }

        protected void imgCapoDown_Click(object sender, ImageClickEventArgs e)
        {
            if (SongCapoDecreased != null)
                SongCapoDecreased(this, e);
        }

        protected void imgKeyUp_Click(object sender, ImageClickEventArgs e)
        {
            if (SongKeyIncreased != null)
                SongKeyIncreased(this, e);
        }

        protected void ImageKeyDown_Click(object sender, ImageClickEventArgs e)
        {
            if (SongKeyDecreased != null)
                SongKeyDecreased(this, e);
        }

        protected void txtOrder_TextChanged(object sender, EventArgs e)
        {
            if (PresentationOrderChanged != null)
                PresentationOrderChanged(this, e);
        }
    }
}