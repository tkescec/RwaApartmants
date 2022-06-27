using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Listeo.Models
{
    public class ApartmentViewModelBase
    {
        public int ApartmentId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Owner { get; set; }
        public decimal Price { get; set; }
        public int? MaxAdults { get; set; }
        public int? MaxChildren { get; set; }
        public int? TotalRooms { get; set; }
        public int? BeachDistance { get; set; }
        public string Picture { get; set; }
        public string Base64Content { get; set; }
    }

    public class ApartmentViewModel : ApartmentViewModelBase
    {
        public double ReviewGrade { get; set; }
    }

    public class ApartmentListViewModel : ApartmentViewModelBase
    {
        public IList<ApartmentPicture> Gallery { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Review> Reviews { get; set; }
        public User User { get; set; }
    }

    public class ApartmentCookieViewModel
    {
        public IList<City> Cities { get; set; }
        public int Rooms { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string SelectedCity { get; set; }
        public int Sort { get; set; }
    }

    public enum ApartmentStatus
    {
        Zauzeto = 1,
        Rezervirano = 2,
        Slobodno = 3
    }
}