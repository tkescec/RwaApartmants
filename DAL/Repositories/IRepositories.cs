using DAL.Repositories.Auth;
using DAL.Repositories.Users;

namespace DAL.Repositories
{
    public interface IRepositories
    {
        IAuthRepository AuthRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
