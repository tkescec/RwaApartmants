using Administrator.Events.Pagination;
using Administrator.Models.ViewModels;
using Administrator.Services;
using Administrator.Views.Shared;
using DAL.Collection;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator
{
    public partial class Bookings : MainPage
    {
        private PaginationCollection<Reservation> _data;
        private IList<Reservation> _allReservations;

        protected void Page_Load(object sender, EventArgs e)
        {
            Pagination.PageClick += new PaginationDelegate(BtnPage_Click);

            try
            {
                if (!IsPostBack)
                {
                    ViewState["CurrentPageIndex"] = PAGE_INDEX;
                    GetData(PAGE_INDEX, PAGE_SIZE);
                    BuildPagination(_data);
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

        protected void BtnPage_Click(object sender, PaginationEventArgs e)
        {
            ViewState["CurrentPageIndex"] = e.PageIndex;
            GetData(e.PageIndex, PAGE_SIZE);
        }

        private void GetData(int pIndex, int pSize)
        {
            try
            {
                _data = Repositories.ReservationRespository.GetReservations(pIndex, pSize);
                _allReservations = _data.Collection;

                BindData(rptReservations, _allReservations);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}