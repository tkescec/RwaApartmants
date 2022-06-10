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

namespace DAL.Repositories.Apartments
{
    internal class ApartmentRepository : IApartmentRepository
    {
        public string CS { get; }

        public ApartmentRepository(string cS)
        {
            CS = cS;
        }

        public PaginationCollection<Apartment> GetApartments()
        {
            PaginationCollection<Apartment> pagination = new PaginationCollection<Apartment>
            {
                TotalRecords = 0,
            };

            SqlParameter[] spParameter = new SqlParameter[1];

            spParameter[0] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartments), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[0].Value);


            return pagination;
        }

        public PaginationCollection<Apartment> GetApartments(int iPageIndex, int iPageSize)
        {
            PaginationCollection<Apartment> pagination = new PaginationCollection<Apartment>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<Apartment>()
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

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartments), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[2].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateApartmentModel(row)
                );
            }

            return pagination;
        }

        public PaginationCollection<Apartment> GetApartments(int iPageIndex, int iPageSize, int iCityFilter, int iStatusFilter, string iSortFilter)
        {
            PaginationCollection<Apartment> pagination = new PaginationCollection<Apartment>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<Apartment>()
            };

            SqlParameter[] spParameter = new SqlParameter[7];

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

            if (iCityFilter != 0)
            {
                spParameter[2] = new SqlParameter("@FilterByCity", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = iCityFilter
                };
            }

            if (iStatusFilter != 0)
            {
                spParameter[3] = new SqlParameter("@FilterByStatus", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = iStatusFilter
                };
            }

            if (iSortFilter != "")
            {
                string sortColumn = GetSortColumn(iSortFilter);
                string sortDirection = GetSortDirection(iSortFilter);

                spParameter[4] = new SqlParameter("@SortColumn", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = sortColumn
                };
                spParameter[5] = new SqlParameter("@SortDirection", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = sortDirection
                };
            }

            spParameter[6] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartments), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[6].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateApartmentModel(row)
                );
            }

            return pagination;
        }

        public IList<ApartmentStatus> GetApartmentStatuses()
        {
            List<ApartmentStatus> cities = new List<ApartmentStatus>();

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartmentStatuses)).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                cities.Add(
                    new ApartmentStatus
                    {
                        StatusID = (int)row[nameof(ApartmentStatus.StatusID)],
                        Name = row[nameof(ApartmentStatus.Name)].ToString()
                    }
                );
            }

            return cities;
        }

        #region Private Methods
        private Apartment CreateApartmentModel(DataRow row)
        {
            return new Apartment
            {
                ApartmentID = (int)row[nameof(Apartment.ApartmentID)],
                Owner = row[nameof(Apartment.Owner)].ToString(),
                Status = row[nameof(Apartment.Status)].ToString(),
                City = row[nameof(Apartment.City)].ToString(),
                Address = row[nameof(Apartment.Address)].ToString(),
                Name = row[nameof(Apartment.Name)].ToString(),
                NameEng = row[nameof(Apartment.NameEng)].ToString(),
                Price = (decimal)row[nameof(Apartment.Price)],
                MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                Picture = GetFeaturedImage(row[nameof(Apartment.Picture)].ToString()),
                CreatedAt = (DateTime?)(row.IsNull(nameof(Apartment.CreatedAt)) ? null : row[nameof(Apartment.CreatedAt)]),
                DeletedAt = (DateTime?)(row.IsNull(nameof(Apartment.DeletedAt)) ? null : row[nameof(Apartment.DeletedAt)]),
            };
        }
        private string GetSortDirection(string iSortFilter)
        {
            switch (iSortFilter)
            {
                case "price_asc":
                case "rooms_asc":
                case "cap_asc":
                    return "ASC";
                case "price_desc":
                case "rooms_desc":
                case "cap_desc":
                    return "DESC";
                default:
                    return "ASC";
            }
        }
        private string GetSortColumn(string iSortFilter)
        {
            switch (iSortFilter)
            {
                case "price_asc":
                case "price_desc":
                    return "Price";
                case "rooms_asc":
                case "rooms_desc":
                    return "TotalRooms";
                case "cap_asc":
                case "cap_desc":
                    return "MaxAdults";
                default:
                    return "Price";
            }
        }
        private string GetFeaturedImage(string base64)
        {
            if (base64 == "")
            {
                return ImagesCollection.NO_IMAGE;
            }

            return base64;
        }
        #endregion
    }
}
