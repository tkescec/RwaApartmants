using DAL.Models;
using DAL.Repositories;
using System;
using Utilities;

namespace Administrator
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                FormPanel.Visible = true;
                MessagePanel.Visible = false;
                txtEmail.Focus();
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
                var email = txtEmail.Text;
                var password = Crypto.HashPassword(txtPassword.Text);

                try
                {
                    User user = ((IRepositories)Application["Repositories"]).AuthRepository.AuthUser(email, password);
                    SignIn(user);
                }
                catch (Exception)
                {
                    ShowMessagePanel("Došlo je do problema. Molimo kontaktirajte administratora stranice.");
                }


            }
        }

        private void SignIn(User user)
        {
            if (user == null)
            {
                ShowMessagePanel("Neispravna E-mail adresa i/ili zaporka.");
            }
            else
            {
                if (!user.EmailConfirmed)
                {
                    ShowMessagePanel("Niste potvrdili E-mail adresu. Potvrdite E-mail adresu kako bi mogli pristupiti sustavu!");
                }
                else if (user.DeletedAt != null)
                {
                    ShowMessagePanel("Korisnički račun je daktiviran!");
                }
                else if (user.Role != Roles.RoleType.Administrator.ToString())
                {
                    ShowMessagePanel("Nemate dopuštenje za pristup administracijskom sučelju!");
                }
                else
                {
                    Session["user"] = user;
                    Response.Redirect("Dashboard.aspx");
                }
            }

            //txtEmail.Text = "";
            txtPassword.Text = "";
            txtPassword.Focus();
        }

        private void ShowMessagePanel(string message)
        {
            lblErrorLogin.Text = message;
            MessagePanel.Visible = true;
        }
    }
}