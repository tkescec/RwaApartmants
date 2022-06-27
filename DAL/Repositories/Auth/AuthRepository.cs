using DAL.Models;
using DAL.Models.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Repositories.Auth
{
    public class AuthRepository :  IAuthRepository
    {
        public string CS { get; }

        public AuthRepository(string cS)
        {
            CS = cS;
        }

        public User AuthUser(string email, string password)
        {
            DataTable tblAuth = SqlHelper.ExecuteDataset(CS, nameof(AuthUser), email, password).Tables[0];

            if (tblAuth.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = tblAuth.Rows[0];

            return new User
            {
                UserID = (int)row[nameof(User.UserID)],
                Username = row[nameof(User.Username)].ToString(),
                Role = row[nameof(User.Role)].ToString(),
                EmailConfirmed = (bool)row[nameof(User.EmailConfirmed)],
                DeletedAt = (DateTime?)(row.IsNull(nameof(User.DeletedAt)) ? null : row[nameof(User.DeletedAt)]),
            };
        }

        public bool RegisterUser(RegisterViewModel register)
        {
            if (register == null)
            {
                return false;
            }

            int? userId = null;
            SqlParameter[] spParameter = new SqlParameter[7];

            spParameter[0] = new SqlParameter("@Guid", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.Input,
                Value = register.GUID
            };

            spParameter[1] = new SqlParameter("@CreatedAt", SqlDbType.DateTime2)
            {
                Direction = ParameterDirection.Input,
                Value = register.CreatedAt
            };

            spParameter[2] = new SqlParameter("@Email", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = register.Email
            };

            spParameter[3] = new SqlParameter("@Password", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = register.Password
            };

            spParameter[4] = new SqlParameter("@UserName", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = register.Username
            };

            spParameter[5] = new SqlParameter("@RoleId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = register.RoleId
            };

            spParameter[6] = new SqlParameter("@UserId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(RegisterUser), spParameter);

            userId = Convert.ToInt32(spParameter[6].Value);

            if (userId != null)
            {
                return true;
            }

            return false;
        }
    }
}
