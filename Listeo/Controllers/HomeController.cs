using DAL.Collection;
using DAL.Models;
using Listeo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace Listeo.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            ApartmentCookieViewModel apartmentViewModel = new ApartmentCookieViewModel();

            try
            {
                HttpCookie cookieData = Request.Cookies["listeo"];
                FiltersCookieViewModel filtersData = new FiltersCookieViewModel();
                IList<City> cities = Repository.CityRepository.GetCities();
                int apartmentCount = Repository.ApartmentRepository.GetApartments().Collection.Count(p => p.StatusID == (int)Models.ApartmentStatus.Slobodno);
                int apartmentItems = 12;

                if (cookieData != null)
                {
                    byte[] data = Convert.FromBase64String(cookieData.Value);
                    string decodedString = Encoding.UTF8.GetString(data);
                    filtersData = JsonHelper.FromJsonNewtonsoft<FiltersCookieViewModel>(decodedString);
                }

                TempData["ApartmentCount"] = apartmentCount;
                TempData["ApartmentItems"] = apartmentItems;
                ViewBag.Page = 1;
                ViewBag.MoreApartments = apartmentCount > apartmentItems;
                GetTempData();
                FillApartmentViewModel(apartmentViewModel, cities, filtersData);

                
            }
            catch (Exception)
            {
                ViewBag.Error = "There was a problem. Please contact the site administrator.";
            }
            

            return View(apartmentViewModel);
        }
        
        [HttpGet]
        public PartialViewResult IndexPartial(int page = 1, int items = 6, int rooms = 0, int adults = 0, int children = 0, string city = "", int sort = 0)
        {
            if (TempData["ApartmentItems"] != null && rooms == 0)
            {
                items = (int)TempData["ApartmentItems"];
            } 

            TempData["ApartmentItems"] = items;

            IList<Apartment> apartments = Repository.ApartmentRepository.GetApartments().Collection
                                            .Where(a => a.StatusID == (int)Models.ApartmentStatus.Slobodno)
                                            .Where(a => a.TotalRooms >= rooms)
                                            .Where(a => a.MaxAdults >= adults)
                                            .Where(a => a.MaxChildren >= children)
                                            .Skip((page - 1) * items)
                                            .Take(items)
                                            .ToList();
            if (city != "")
            {
                apartments = apartments.Where(a => a.City == city).ToList();
            }
                

            apartments = GetSortedApartments(apartments, sort);

            IList<ApartmentViewModel> apartmentsList = new List<ApartmentViewModel>();

            foreach (var apartment in apartments)
            {
                IList<int> reviews = Repository.ReviewRepository.GetReviews(apartment.ApartmentID).Collection
                                        .Select(r => r.Stars)
                                        .ToList();

                apartmentsList.Add(
                    CreateApartmentListViewMode(apartment, reviews)
                );
            }

            return PartialView("_Index", apartmentsList);
        }

        private IList<Apartment> GetSortedApartments(IList<Apartment> apartments, int sort)
        {
            switch (sort)
            {
                case 1:
                    return apartments.OrderByDescending(a => a.Price).ToList();
                case 2:
                    return apartments.OrderBy(a => a.Price).ToList();
                default:
                    return apartments.OrderByDescending(a => a.ApartmentID).ToList();
            }
        }

        private ApartmentViewModel CreateApartmentListViewMode(Apartment apartment, IList<int> reviews)
        {
            return new ApartmentViewModel()
            {
                ApartmentId = apartment.ApartmentID,
                Name = apartment.Name,
                Address = apartment.Address,
                TotalRooms = apartment.TotalRooms,
                MaxAdults = apartment.MaxAdults,
                MaxChildren = apartment.MaxChildren,
                Picture = apartment.Base64Content,
                Price = apartment.Price,
                ReviewGrade = Math.Round(reviews.Any() ? reviews.Average() : 0, 1, MidpointRounding.AwayFromZero)
            };
        }

        private void FillApartmentViewModel(ApartmentCookieViewModel apartmentViewModel, IList<City> cities, FiltersCookieViewModel filtersData)
        {
            apartmentViewModel.Cities = cities;
            apartmentViewModel.SelectedCity = filtersData.City;
            apartmentViewModel.Children = filtersData.Children;
            apartmentViewModel.Adults = filtersData.Adults;
            apartmentViewModel.Rooms = filtersData.Rooms;
            apartmentViewModel.Sort = filtersData.Sort;
        }
    }
}