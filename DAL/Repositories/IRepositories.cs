using DAL.Repositories.Apartments;
using DAL.Repositories.Auth;
using DAL.Repositories.Cities;
using DAL.Repositories.Reservations;
using DAL.Repositories.Reviews;
using DAL.Repositories.Tags;
using DAL.Repositories.Users;

namespace DAL.Repositories
{
    public interface IRepositories
    {
        IAuthRepository AuthRepository { get; }
        IUserRepository UserRepository { get; }
        IApartmentRepository ApartmentRepository { get; }
        ICityRepository CityRepository { get; }
        ITagRepository TagRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IReservationRespository ReservationRespository { get; }
    }
}
