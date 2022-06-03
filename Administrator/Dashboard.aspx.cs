using Administrator.Models.ViewModels;
using Administrator.Services;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public DashboardModel DashboardData { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetData();
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

        private void GetData()
        {
            try
            {
                var users = ((IRepositories)Application["Repositories"]).UserRepository.GetAllUsers(0, 0);
                var apartments = ((IRepositories)Application["Repositories"]).ApartmentRepository.GetAllApartments(0, 0);

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