using DAL.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Cities
{
    public class CityRepository : ICityRepository
    {
        public string CS { get; }

        public CityRepository(string cS)
        {
            CS = cS;
        }

        public IList<City> GetCities()
        {
            List<City> cities = new List<City>();

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetCities)).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                cities.Add(
                    new City
                    {
                        CityID = (int)row[nameof(City.CityID)],
                        Name = row[nameof(City.Name)].ToString()
                    }
                );
            }

            return cities;
        }
    }
}
