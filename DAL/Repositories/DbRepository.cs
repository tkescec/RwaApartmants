using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories.Auth;
using DAL.Repositories.Users;

namespace DAL.Repositories
{
    public class DbRepository : IRepositories
    {
        private static readonly string CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public IAuthRepository AuthRepository => new AuthRepository(CS);
        public IUserRepository UserRepository => new UserRepository(CS);

    }
}
