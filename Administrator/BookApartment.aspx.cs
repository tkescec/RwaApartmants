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
    public partial class BookApartment : MainPage
    {
        private ApartmentViewModel _apartment;
        private IList<User> _allUsers;

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
                    GetData(typeof(User), apartmentId);

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

        protected void btnBookApartment_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    int? userId = null;
                    if (ddlBookingUsers.SelectedValue != "")
                        userId = Int32.Parse(ddlBookingUsers.SelectedValue);
                    

                    ReservationViewModel reservationModel = new ReservationViewModel()
                    {
                        GUID = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        ApartmentID = (int)ViewState["apartmentId"],
                        UserID = userId,
                        UserName = GetTextBoxValue(txtUserName),
                        UserAddress = GetTextBoxValue(txtUserAddress),
                        UserEmail = GetTextBoxValue(txtUserEmail),
                        UserPhone = GetTextBoxValue(txtUserPhone),
                        Details = GetTextBoxValue(txtBookingDetails)
                    };

                    if (Repositories.ReservationRespository.AddReservation(reservationModel))
                    {
                        AlertService.ShowAlert(Page, AlertService.AlertType.Success, new SweetAlertModel
                        {
                            Title = "Uspjeh!",
                            Text = "Uspješno ste rezervirali apartman."
                        });

                        ResetForm();

                        return;
                    }

                    AlertService.ShowAlert(Page, AlertService.AlertType.Info, new SweetAlertModel
                    {
                        Title = "Info!",
                        Text = "Nismo uspjeli napraviti rezervaciju apartmana!"
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
                    titleApartment.InnerHtml = "Rezervacija apartmana - " + _apartment.Name;
                }
                else if (type == typeof(User))
                {
                    _allUsers = Repositories.UserRepository.GetUsers().Collection;
                    BindData(ddlBookingUsers, _allUsers, "Username", "UserID");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetTextBoxValue(TextBox textBox)
        {
            return textBox.Text != "" ? textBox.Text : null;
        }

        private void ResetForm()
        {
            ddlBookingUsers.SelectedIndex = 0;
            txtUserName.Text = "";
            txtUserAddress.Text = "";
            txtUserEmail.Text = "";
            txtUserPhone.Text = "";
            txtBookingDetails.Text = "";
        }
    }
}