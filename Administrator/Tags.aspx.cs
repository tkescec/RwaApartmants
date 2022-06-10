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
    public partial class Tags : MainPage
    {
        private PaginationCollection<Tag> _data;
        private IList<Tag> _allTags;

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

        protected void DeleteTag_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int tagId = int.Parse((item.FindControl("lblTagId") as Label).Text);
            int apartmentCount = int.Parse((item.FindControl("lblApartmentCount") as Label).Text);

            if (apartmentCount > 0 )
            {
                AlertService.ShowAlert(Page, AlertService.AlertType.Warning, new SweetAlertModel
                {
                    Title = "Upozorenje!",
                    Text = "Ne možete obrisati željeni tag. Tag je dodjeljen nekom od apartmana."
                });

                return;
            }

            try
            {
                if (Repositories.TagRepository.DeleteTags(tagId))
                {
                    int currentPageIndex = (int)ViewState["CurrentPageIndex"];

                    GetData(currentPageIndex, PAGE_SIZE);
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

        protected void DeleteAllTags_Click(object sender, EventArgs e)
        {
            try
            {
                if (Repositories.TagRepository.DeleteTags(null))
                {
                    GetData(PAGE_INDEX, PAGE_SIZE);
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

        private void GetData(int pIndex, int pSize)
        {
            try
            {
                _data = Repositories.TagRepository.GetTags(pIndex, pSize);
                _allTags = _data.Collection;

                BindData(rptTags, _allTags);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}