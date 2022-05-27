using DAL.Collection;
using DAL.Models;

namespace DAL.Repositories.Apartments
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        PaginationCollection<Apartment> GetAllApartments(int iPageIndex, int iPageSize);
    }
}
