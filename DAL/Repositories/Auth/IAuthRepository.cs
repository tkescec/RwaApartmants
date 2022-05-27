using DAL.Models;

namespace DAL.Repositories.Auth
{
    public interface IAuthRepository : IRepository<User>
    {
        User AuthUser(string email, string password);
    }
}
