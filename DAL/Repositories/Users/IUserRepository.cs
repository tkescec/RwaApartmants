using DAL.Collection;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Pagination<User> GetAllUsers(int iPageIndex, int iPageSize);
    }
}
