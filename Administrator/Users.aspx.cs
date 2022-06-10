using Administrator.Events.Pagination;
using Administrator.Models.ViewModels;
using Administrator.Services;
using Administrator.Views.Shared;
using DAL.Collection;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;

namespace Administrator
{
    public partial class Users : MainPage
    {
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
                AlertService.ShowAlert(Page, AlertService.AlertType.Error, new SweetAlertModel
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
                _data = Repositories.UserRepository.GetUsers(pIndex, pSize);
                _allUsers = _data.Collection;

                BindData(rptUsers, _allUsers);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}