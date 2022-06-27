using DAL.Collection;
using DAL.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        public string CS { get; }

        public ReviewRepository(string cS)
        {
            CS = cS;
        }

        public PaginationCollection<Review> GetReviews()
        {
            PaginationCollection<Review> pagination = new PaginationCollection<Review>
            {
                TotalRecords = 0
            };

            SqlParameter[] spParameter = new SqlParameter[1];

            spParameter[0] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetReviews), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[0].Value);

            return pagination;
        }

        public PaginationCollection<Review> GetReviews(int? apartmentId)
        {
            PaginationCollection<Review> pagination = new PaginationCollection<Review>
            {
                TotalRecords = 0,
                Collection = new List<Review>()
            };

            SqlParameter[] spParameter = new SqlParameter[2];

            if (apartmentId != null)
            {
                spParameter[0] = new SqlParameter("@ApartmentId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentId
                };
            }

            spParameter[1] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetReviews), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[1].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateReviewModel(row)
                );
            }

            return pagination;
        }

        public PaginationCollection<Review> GetReviews(int iPageIndex, int iPageSize)
        {
            PaginationCollection<Review> pagination = new PaginationCollection<Review>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<Review>()
            };

            SqlParameter[] spParameter = new SqlParameter[3];

            spParameter[0] = new SqlParameter("@PageIndex", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = iPageIndex
            };

            spParameter[1] = new SqlParameter("@PageSize", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = iPageSize
            };

            spParameter[2] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetReviews), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[2].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateReviewModel(row)
                );
            }

            return pagination;
        }

        public PaginationCollection<Review> GetReviews(int iPageIndex, int iPageSize, int? apartmentId)
        {
            PaginationCollection<Review> pagination = new PaginationCollection<Review>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<Review>()
            };

            SqlParameter[] spParameter = new SqlParameter[4];

            if (apartmentId != null)
            {
                spParameter[0] = new SqlParameter("@ApartmentId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentId
                };
            }

            spParameter[1] = new SqlParameter("@PageIndex", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = iPageIndex
            };

            spParameter[2] = new SqlParameter("@PageSize", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = iPageSize
            };

            spParameter[3] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetReviews), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[3].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateReviewModel(row)
                );
            }

            return pagination;
        }

        #region Private Methods
        private Review CreateReviewModel(DataRow row)
        {
            return new Review
            {
                ReviewID = (int)row[nameof(Review.ReviewID)],
                Apartment = row[nameof(Review.Apartment)].ToString(),
                UserName = row[nameof(Review.UserName)].ToString(),
                Details = row[nameof(Review.Details)].ToString(),
                Stars = (int)row[nameof(Review.Stars)],
                CreatedAt = (DateTime?)(row.IsNull(nameof(Review.CreatedAt)) ? null : row[nameof(Review.CreatedAt)]),
            };
        }
        #endregion
    }
}
