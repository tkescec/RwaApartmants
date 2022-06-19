using DAL.Collection;
using DAL.Models;
using DAL.Models.ViewModels;
using System.Collections.Generic;

namespace DAL.Repositories.Apartments
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        PaginationCollection<Apartment> GetApartments();
        PaginationCollection<Apartment> GetApartments(int iPageIndex, int iPageSize);
        PaginationCollection<Apartment> GetApartments(int iPageIndex, int iPageSize, int iCityFilter, int iStatusFilter, string iSortFilter);
        IList<ApartmentStatus> GetApartmentStatuses();
        IList<ApartmentOwner> GetApartmentOwners();
        bool DeleteApartments(int apartmentId);
        bool AddApartment(ApartmentViewModel apartment);
        bool AddApartmentTags(int? apartmentId, IList<Tag> apartmentTags);
        bool AddApartmentPictures(int? apartmentId, IList<ApartmentPicture> apartmentPictures);
        ApartmentViewModel GetApartment(int apartmentId);
        IList<Tag> GetApartmentTags(int apartmentId);
    }
}
