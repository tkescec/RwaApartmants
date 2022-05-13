using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administrator.Events.Pagination
{
    public class PaginationEventArgs : EventArgs
    {
        public int PageIndex { get; set; }
    }
}