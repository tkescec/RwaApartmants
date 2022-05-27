using DAL.Collection;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Apartments
{
    internal class ApartmentRepository : IApartmentRepository
    {
        public string CS { get; }

        public ApartmentRepository(string cS)
        {
            CS = cS;
        }

        public PaginationCollection<Apartment> GetAllApartments(int iPageIndex, int iPageSize)
        {
            throw new NotImplementedException();
        }
    }
}
