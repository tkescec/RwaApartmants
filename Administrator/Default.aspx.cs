using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelForma.Visible = true;
                PanelIspis.Visible = false;
            }

            if (Session["user"] != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                //var username = txtUsername.Text;
                //var password = Cryptography.HashPassword(txtPassword.Text);

                //User user = ((IRepo)Application["database"]).AuthUser(username, password);
                string user = null;

                if (user == null)
                {
                    
                    PanelForma.Visible = true;
                    PanelIspis.Visible = true;

                    txtUsername.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    Session["user"] = user;
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
    }
}