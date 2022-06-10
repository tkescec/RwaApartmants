using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Cities
{
    public interface ICityRepository : IRepository<City>
    {
        IList<City> GetCities();
    }
}
