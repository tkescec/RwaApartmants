using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repositories
{
    public class DbRepository : IRepository
    {
        private static readonly string _CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public User AuthUser(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
