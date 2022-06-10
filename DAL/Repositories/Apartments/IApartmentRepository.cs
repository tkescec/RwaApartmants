using DAL.Collection;
using DAL.Models;
using System.Collections.Generic;

namespace DAL.Repositories.Apartments
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        PaginationCollection<Apartment> GetApartments();
        PaginationCollection<Apartment> GetApartments(int iPageIndex, int iPageSize);
        PaginationCollection<Apartment> GetApartments(int iPageIndex, int iPageSize, int iCityFilter, int iStatusFilter, string iSortFilter);
        IList<ApartmentStatus> GetApartmentStatuses();
    }
}
