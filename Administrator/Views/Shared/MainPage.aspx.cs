using Administrator.Services;
using DAL.Collection;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator.Views.Shared
{
    public partial class MainPage : System.Web.UI.Page
    {
        public const int PAGE_INDEX = 1;
        public const int PAGE_SIZE = 20;
        public IRepositories Repositories { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            Repositories = ((IRepositories)Application["Repositories"]);

            base.OnLoad(e);
        }

        protected void BuildPagination<T>(PaginationCollection<T> data)
        {
            Application["PaginationPages"] = PaginationService.GetPaginationPages(data.TotalRecords, PAGE_INDEX, PAGE_SIZE);
        }

        protected void BindData<T>(Control control, IList<T> data)
        {
            try
            {
                if (control.GetType() == typeof(Repeater))
                {
                    using (Repeater repeater = control as Repeater)
                    {
                        repeater.DataSource = data;
                        repeater.DataBind();
                    }
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BindData<T>(Control control, IList<T> data, string dataTextField, string dataValueField)
        {
            try
            {
                if (control.GetType() == typeof(DropDownList))
                {
                    using (DropDownList dropDownList = control as DropDownList)
                    {
                        dropDownList.DataSource = data;
                        dropDownList.DataTextField = dataTextField;
                        dropDownList.DataValueField = dataValueField;
                        dropDownList.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}