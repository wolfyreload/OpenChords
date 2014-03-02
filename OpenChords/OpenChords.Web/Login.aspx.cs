using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string returnUrl
        {
            get { return Request.Params["ReturnUrl"]; }
        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            var realHashedPassword = "hbc123hbc".GetHashCode();
            var hashedPassword = txtPassword.Text.GetHashCode();

            if (realHashedPassword == hashedPassword)
            {
                FormsAuthentication.SetAuthCookie(txtUserName.Text, false);
                if (returnUrl != null)
                    Response.Redirect(returnUrl);
                else
                    Response.Redirect("~/");
            }
            else
                lblError.Visible = true;
        
        }
    }
}