using DAL.Models;
using DAL.Models.ViewModels;

namespace DAL.Repositories.Auth
{
    public interface IAuthRepository : IRepository<User>
    {
        User AuthUser(string email, string password);
        bool RegisterUser(RegisterViewModel user);
    }
}
