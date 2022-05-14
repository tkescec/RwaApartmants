using System.Collections.Generic;

namespace DAL.Collection
{
    public class PaginationCollection<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public IList<T> Collection { get; set; }
    }
}
