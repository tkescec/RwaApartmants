using DAL.Collection;
using DAL.Models;

namespace DAL.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        PaginationCollection<User> GetAllUsers(int iPageIndex, int iPageSize);
    }
}
