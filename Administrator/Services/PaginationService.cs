using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Administrator.Services
{
    public static class PaginationService
    {
        public static List<ListItem> GetPaginationPages(int totalRecords, int pageIndex, int pageSize)
        {
            double dPageCount = (double)((decimal)totalRecords / Convert.ToDecimal(pageSize));
            int iPageCount = (int)Math.Ceiling(dPageCount);
            List<ListItem> lPages = new List<ListItem>();
            if (iPageCount > 0)
            {
                for (int i = 1; i <= iPageCount; i++)
                    lPages.Add(new ListItem(i.ToString(), i.ToString(), i != pageIndex));
            }

            return lPages;
        }
    }
}