using Administrator.Models.ViewModels;
using Administrator.Services;
using Administrator.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator
{
    public partial class BookApartment : MainPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    if (string.IsNullOrEmpty(Request.QueryString["ApartmentId"]))
                        Response.Redirect("/Dashboard");

                    if (!int.TryParse(Request.QueryString["ApartmentId"], out int apartmentId))
                        Response.Redirect("/Dashboard");
                }

            }
            catch (Exception)
            {
                AlertService.ShowAlert(Page, AlertService.AlertType.Error, new SweetAlertModel
                {
                    Title = "Greška!",
                    Text = "Došlo je do problema. Molimo kontaktirajte administratora stranice."
                });
            }
        }
    }
}