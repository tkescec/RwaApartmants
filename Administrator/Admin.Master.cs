using DAL.Models;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Administrator.Services;
using Administrator.Models.ViewModels;

namespace Administrator
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                //Response.Redirect("/");
            }      
        }

        protected override void OnPreRender(EventArgs e)
        {            
            try
            {
                ActiveNavItem();
                //RenderUsernameOnNavbar();
                YearLabel.Text = DateTime.Now.Year.ToString();
            }
            catch (Exception)
            {
                AlertService.ShowAlert(Page, AlertService.AlertType.Error, new SweetAlertModel
                {
                    Title = "Greška!",
                    Text = "Došlo je do problema. Molimo kontaktirajte administratora stranice."
                });
            }

            base.OnPreRender(e);
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("/");
        }

        private void RenderUsernameOnNavbar()
        {
            try
            {
                User user = (User)Session["user"];
                lblUsername.Text = user.Username;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ActiveNavItem()
        {
            try
            {
                string url = Request.Url.AbsolutePath.Remove(0, 1);
                HtmlGenericControl navItem = (HtmlGenericControl)FindControl(url);


                if (navItem != null)
                {
                    navItem.Attributes.Add("class", "active");
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}