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

        public PaginationCollection<Apartment> GetAllApartments(int iPageIndex, int iPageSize)
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

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetAllApartments), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[2].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    new Apartment
                    {
                        ApartmentID = (int)row[nameof(Apartment.ApartmentID)],
                        Owner = row[nameof(Apartment.Owner)].ToString(),
                        Status = row[nameof(Apartment.Status)].ToString(),
                        City = row[nameof(Apartment.City)].ToString(),
                        Address = row[nameof(Apartment.Address)].ToString(),
                        Name = row[nameof(Apartment.Name)].ToString(),
                        NameEng = row[nameof(Apartment.NameEng)].ToString(),
                        Price = (double)row[nameof(Apartment.Price)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        CreatedAt = (DateTime?)(row.IsNull(nameof(Apartment.CreatedAt)) ? null : row[nameof(Apartment.CreatedAt)]),
                        DeletedAt = (DateTime?)(row.IsNull(nameof(Apartment.DeletedAt)) ? null : row[nameof(Apartment.DeletedAt)]),
                    }
                );
            }

            return pagination;
        }
    }
}
