using Administrator.Models.ViewModels;
using Administrator.Services;
using Administrator.Views.Shared;
using DAL.Models;
using DAL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator
{
    public partial class EditApartment : MainPage
    {
        private ApartmentViewModel _apartment;

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

                    ViewState["apartmentId"] = apartmentId;
                    GetData(typeof(Apartment), apartmentId);
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

        protected void btnUpdateApartment_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    

                    AlertService.ShowAlert(Page, AlertService.AlertType.Info, new SweetAlertModel
                    {
                        Title = "Info!",
                        Text = "Nismo uspjeli ažurirati željeni apartman!"
                    });
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

        private void GetData(Type type, int apartmentId)
        {
            try
            {
                if (type == typeof(Apartment))
                {
                    _apartment = Repositories.ApartmentRepository.GetApartment(apartmentId);
                    titleApartment.InnerHtml = "Uređivanje apartmana - " + _apartment.Name;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}