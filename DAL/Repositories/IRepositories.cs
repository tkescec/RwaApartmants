using DAL.Repositories.Auth;
using DAL.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IRepositories
    {
        IAuthRepository AuthRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
