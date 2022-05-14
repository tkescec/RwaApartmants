using Administrator.Events.Pagination;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Administrator.Controls
{
    public partial class Pagination : System.Web.UI.UserControl
    {
        public event PaginationDelegate PageClick;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptPagination.DataSource = (List<ListItem>)Application["PaginationPages"];
                rptPagination.DataBind();

                LinkButton firstBtn = (LinkButton)rptPagination.Items[0].FindControl("lnkPage");
                firstBtn.CssClass = "current-page";
            }
        }

        protected void BtnPage_Click(object sender, EventArgs e)
        {
            ResetButtonsCss();

            LinkButton btn = (LinkButton)sender;
            int PageIndex = Convert.ToInt32(btn.CommandArgument);
            btn.CssClass = "current-page";

            PageClick.Invoke(this, new PaginationEventArgs
            {
                PageIndex = PageIndex,
            });

        }

        private void ResetButtonsCss()
        {
            foreach (RepeaterItem item in rptPagination.Items)
            {
                if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
                {
                    LinkButton lb = (LinkButton)item.FindControl("lnkPage");

                    if (lb != null)
                    {
                        lb.CssClass = "";
                    }
                }
            }
        }
    }
}