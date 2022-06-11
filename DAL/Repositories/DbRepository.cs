using System.Configuration;
using DAL.Repositories.Apartments;
using DAL.Repositories.Auth;
using DAL.Repositories.Cities;
using DAL.Repositories.Reviews;
using DAL.Repositories.Tags;
using DAL.Repositories.Users;

namespace DAL.Repositories
{
    public class DbRepository : IRepositories
    {
        private static readonly string CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public IAuthRepository AuthRepository => new AuthRepository(CS);
        public IUserRepository UserRepository => new UserRepository(CS);
        public IApartmentRepository ApartmentRepository => new ApartmentRepository(CS);
        public ICityRepository CityRepository => new CityRepository(CS);
        public ITagRepository TagRepository => new TagRepository(CS);
        public IReviewRepository ReviewRepository => new ReviewRepository(CS);
    }
}
