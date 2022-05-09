using System;
using System.Collections.Generic;
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
    }
}
