using DAL.Collection;
using DAL.Models;

namespace DAL.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        PaginationCollection<User> GetUsers();
        PaginationCollection<User> GetUsers(int iPageIndex, int iPageSize);
    }
}
