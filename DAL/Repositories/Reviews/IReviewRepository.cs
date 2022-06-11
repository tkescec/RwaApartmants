using DAL.Collection;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Reviews
{
    public interface IReviewRepository : IRepository<Review>
    {
        PaginationCollection<Review> GetReviews();
        PaginationCollection<Review> GetReviews(int iPageIndex, int iPageSize);
        PaginationCollection<Review> GetReviews(int iPageIndex, int iPageSize, int? apartmentId);
    }
}
