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

namespace DAL.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        public string CS { get; }

        public UserRepository(string cS)
        {
            CS = cS;
        }

        public Pagination<User> GetAllUsers(int iPageIndex, int iPageSize)
        {
            Pagination<User> pagination = new Pagination<User>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<User>()
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

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetAllUsers), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[2].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    new User
                    {
                        UserID = (int)row[nameof(User.UserID)],
                        Username = row[nameof(User.Username)].ToString(),
                        Role = row[nameof(User.Role)].ToString(),
                        EmailConfirmed = (bool)row[nameof(User.EmailConfirmed)],
                        DeletedAt = (DateTime?)(row.IsNull(nameof(User.DeletedAt)) ? null : row[nameof(User.DeletedAt)]),
                    }
                );
            }
            
            return pagination;
        }
    }
}
