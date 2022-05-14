using Administrator.Events.Pagination;
using Administrator.Models.ViewModels;
using Administrator.Services;
using DAL.Collection;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;

namespace Administrator
{
    public partial class Users : System.Web.UI.Page
    {
        private const int PAGE_INDEX = 1;
        private const int PAGE_SIZE = 20;

        private PaginationCollection<User> _data;
        private IList<User> _allUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
            Pagination.PageClick += new PaginationDelegate(BtnPage_Click);

            try
            {
                if (!IsPostBack)
                {
                    GetData(PAGE_INDEX, PAGE_SIZE);
                    BuildPagination(_data);
                }
            }
            catch (Exception)
            {
                AlertService.ShowAlert(Page, AlertService.AlertType.Error, new SweetAlert
                {
                    Title = "Greška!",
                    Text = "Došlo je do problema. Molimo kontaktirajte administratora stranice."
                });
            }
            
        }

        protected void BtnPage_Click(object sender, PaginationEventArgs e)
        {
            GetData(e.PageIndex, PAGE_SIZE);
        }

        private void GetData(int pIndex, int pSize)
        {
            try
            {
                _data = ((IRepositories)Application["Repositories"]).UserRepository.GetAllUsers(pIndex, pSize);
                _allUsers = _data.Collection;

                BindData();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BuildPagination(PaginationCollection<User> data)
        {
            Application["PaginationPages"] = PaginationService.GetPaginationPages(data.TotalRecords, PAGE_INDEX, PAGE_SIZE);
        }

        private void BindData()
        {
            try
            {
                rptUsers.DataSource = _allUsers;
                rptUsers.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}