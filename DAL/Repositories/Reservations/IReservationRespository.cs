using DAL.Collection;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Reservations
{
    public interface IReservationRespository : IRepository<Reservation>
    {
        PaginationCollection<Reservation> GetReservations();
        PaginationCollection<Reservation> GetReservations(int iPageIndex, int iPageSize);
        PaginationCollection<Reservation> GetReservations(int iPageIndex, int iPageSize, int? apartmentId);
    }
}
