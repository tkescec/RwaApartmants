using DAL.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;

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

    }
}
