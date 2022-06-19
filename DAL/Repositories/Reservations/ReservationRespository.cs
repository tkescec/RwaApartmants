using DAL.Collection;
using DAL.Models;
using DAL.Models.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Reservations
{
    public class ReservationRespository : IReservationRespository
    {
        public string CS { get; }

        public ReservationRespository(string cS)
        {
            CS = cS;
        }

        public PaginationCollection<Reservation> GetReservations()
        {
            PaginationCollection<Reservation> pagination = new PaginationCollection<Reservation>
            {
                TotalRecords = 0
            };

            SqlParameter[] spParameter = new SqlParameter[1];

            spParameter[0] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetReservations), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[0].Value);

            return pagination;
        }

        public PaginationCollection<Reservation> GetReservations(int iPageIndex, int iPageSize)
        {
            PaginationCollection<Reservation> pagination = new PaginationCollection<Reservation>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<Reservation>()
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

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetReservations), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[2].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateReservationModel(row)
                );
            }

            return pagination;
        }

        public PaginationCollection<Reservation> GetReservations(int iPageIndex, int iPageSize, int? apartmentId)
        {
            PaginationCollection<Reservation> pagination = new PaginationCollection<Reservation>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<Reservation>()
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

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetReservations), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[3].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateReservationModel(row)
                );
            }

            return pagination;
        }
        
        public bool AddReservation(ReservationViewModel reservation)
        {
            if (reservation == null)
            {
                return false;
            }

            int? reservationtId = null;
            SqlParameter[] spParameter = new SqlParameter[10];

            spParameter[0] = new SqlParameter("@Guid", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.Input,
                Value = reservation.GUID
            };

            spParameter[1] = new SqlParameter("@CreatedAt", SqlDbType.DateTime2)
            {
                Direction = ParameterDirection.Input,
                Value = reservation.CreatedAt
            };

            spParameter[2] = new SqlParameter("@ApartmentId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = reservation.ApartmentID
            };

            spParameter[3] = new SqlParameter("@UserId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = reservation.UserID
            };

            spParameter[4] = new SqlParameter("@UserName", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = reservation.UserName
            };

            spParameter[5] = new SqlParameter("@UserAddress", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = reservation.UserAddress
            };

            spParameter[6] = new SqlParameter("@UserEmail", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = reservation.UserEmail
            };

            spParameter[7] = new SqlParameter("@UserPhone", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = reservation.UserPhone
            };

            spParameter[8] = new SqlParameter("@Details", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = reservation.Details
            };

            spParameter[9] = new SqlParameter("@ReservationId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(AddReservation), spParameter);

            reservationtId = Convert.ToInt32(spParameter[9].Value);

            if (reservationtId != null)
            {
                return true;
            }

            return false;
        }

        #region Private Methods
        private Reservation CreateReservationModel(DataRow row)
        {
            return new Reservation
            {
                ReservationID = (int)row[nameof(Reservation.ReservationID)],
                Apartment = row[nameof(Reservation.Apartment)].ToString(),
                Details = row[nameof(Reservation.Details)].ToString(),
                UserType = row[nameof(Reservation.UserType)].ToString(),
                UserName = row[nameof(Reservation.UserName)].ToString(),
                UserEmail = row[nameof(Reservation.UserEmail)].ToString(),
                UserAddress = row[nameof(Reservation.UserAddress)].ToString(),
                UserPhone = row[nameof(Reservation.UserPhone)].ToString(),
                Picture = GetFeaturedImage(row[nameof(Reservation.Picture)].ToString()),
            };
        }
        private string GetFeaturedImage(string path)
        {
            if (path == "")
            {
                return ImagesCollection.NO_IMAGE;
            }

            return System.IO.Path.Combine("~/Images/", path);
        }

        
        #endregion
    }
}
