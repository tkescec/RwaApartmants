using Administrator.Events.Pagination;
using Administrator.Models.ViewModels;
using Administrator.Services;
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
    public partial class Users : System.Web.UI.Page
    {
        private const int PAGE_INDEX = 1;
        private const int PAGE_SIZE = 20;

        private Pagination<User> _data;
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
                Alert.ShowAlert(Page, Alert.AlertType.Error, new SweetAlert
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

        private void BuildPagination(Pagination<User> data)
        {
            double dPageCount = (double)((decimal)data.TotalRecords / Convert.ToDecimal(PAGE_SIZE));
            int iPageCount = (int)Math.Ceiling(dPageCount);
            List<ListItem> lPages = new List<ListItem>();
            if (iPageCount > 0)
            {
                for (int i = 1; i <= iPageCount; i++)
                    lPages.Add(new ListItem(i.ToString(), i.ToString(), i != PAGE_INDEX));
            }

            Application["DataSource"] = lPages;
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