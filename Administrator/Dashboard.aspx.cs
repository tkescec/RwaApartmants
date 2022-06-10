using Administrator.Models.ViewModels;
using Administrator.Services;
using Administrator.Views.Shared;
using DAL.Collection;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator
{
    public partial class Dashboard : MainPage
    {
        public DashboardModel DashboardData { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetDashboardData();
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

        private void GetDashboardData()
        {
            try
            {
                PaginationCollection<User> users = Repositories.UserRepository.GetUsers();
                PaginationCollection<Apartment> apartments = Repositories.ApartmentRepository.GetApartments();

                DashboardData = new DashboardModel
                {
                    Apartments = apartments.TotalRecords,
                    Reservations = 0,
                    Reviewes = 0,
                    Users = users.TotalRecords

                };

                BindData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindData()
        {
            ApartmentCount.InnerText = DashboardData.Apartments.ToString();
            ReservationCount.InnerText = DashboardData.Reservations.ToString();
            ReviewCount.InnerText = DashboardData.Reviewes.ToString();
            UserCount.InnerText = DashboardData.Users.ToString();
        }
    }
}