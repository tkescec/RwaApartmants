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
    public partial class Apartments : MainPage
    {
        private PaginationCollection<Apartment> _data;
        private IList<City> _allCities;
        private IList<ApartmentStatus> _allApartmentStatuses;
        private IList<Apartment> _allApartments;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Pagination.PageClick += new PaginationDelegate(BtnPage_Click);

                if (IsPostBack)
                {
                    int cityFilter = Int32.Parse(filterByCity.SelectedValue);
                    int statusFilter = Int32.Parse(filterByStatus.SelectedValue);
                    string sortFilter = sortBy.SelectedValue.Trim().ToLower();

                    GetData(typeof(Apartment), PAGE_INDEX, PAGE_SIZE, cityFilter, statusFilter, sortFilter);
                    BuildPagination(_data);

                    return;
                }

                GetData(typeof(City));
                GetData(typeof(ApartmentStatus));
                GetData(typeof(Apartment), PAGE_INDEX, PAGE_SIZE);
                BuildPagination(_data);

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
            GetData(typeof(Apartment), e.PageIndex, PAGE_SIZE);
        }

        protected void DeleteApartment_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int apartmentId = int.Parse((item.FindControl("lblApartmentId") as Label).Text);
        }

        private void GetData(Type type)
        {
            try
            {
                if (type == typeof(City))
                {
                    _allCities = Repositories.CityRepository.GetCities();
                    BindData(filterByCity, _allCities, "Name", "CityID");
                } 
                else if (type == typeof(ApartmentStatus))
                {
                    _allApartmentStatuses = Repositories.ApartmentRepository.GetApartmentStatuses();
                    BindData(filterByStatus, _allApartmentStatuses, "Name", "StatusID");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetData(Type type, int pIndex, int pSize)
        {
            try
            {
                if (type == typeof(Apartment))
                {
                    _data = Repositories.ApartmentRepository.GetApartments(pIndex, pSize);
                    _allApartments = _data.Collection;
                    BindData(rptApartments, _allApartments);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void GetData(Type type, int pIndex, int pSize, int cityFilter, int statusFilter, string sortFilter)
        {
            try
            {
                if (type == typeof(Apartment))
                {
                    _data = Repositories.ApartmentRepository.GetApartments(pIndex, pSize, cityFilter, statusFilter, sortFilter);
                    _allApartments = _data.Collection;
                    BindData(rptApartments, _allApartments);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}