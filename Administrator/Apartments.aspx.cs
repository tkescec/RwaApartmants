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
        private int _cityFilter;
        private int _statusFilter;
        private string _sortFilter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Pagination.PageClick += new PaginationDelegate(BtnPage_Click);

                if (IsPostBack)
                {
                    _cityFilter = Int32.Parse(filterByCity.SelectedValue);
                    _statusFilter = Int32.Parse(filterByStatus.SelectedValue);
                    _sortFilter = sortBy.SelectedValue.Trim().ToLower();

                    GetData(typeof(Apartment), PAGE_INDEX, PAGE_SIZE, _cityFilter, _statusFilter, _sortFilter);
                    BuildPagination(_data);

                    return;
                }

                ViewState["CurrentPageIndex"] = PAGE_INDEX;
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
            ViewState["CurrentPageIndex"] = e.PageIndex;
            GetData(typeof(Apartment), e.PageIndex, PAGE_SIZE);
        }

        protected void DeleteApartment_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int apartmentId = int.Parse((item.FindControl("lblApartmentId") as Label).Text);

            try
            {
                if (Repositories.ApartmentRepository.DeleteApartments(apartmentId))
                {
                    int currentPageIndex = (int)ViewState["CurrentPageIndex"];

                    GetData(typeof(Apartment), currentPageIndex, PAGE_SIZE, _cityFilter, _statusFilter, _sortFilter);

                    AlertService.ShowAlert(Page, AlertService.AlertType.Success, new SweetAlertModel
                    {
                        Title = "Uspjeh!",
                        Text = "Uspješno ste obrisali odabrani apartman."
                    });

                    return;
                }

                AlertService.ShowAlert(Page, AlertService.AlertType.Info, new SweetAlertModel
                {
                    Title = "Info!",
                    Text = "Nismo uspjeli obrisati odabrani apartman!"
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