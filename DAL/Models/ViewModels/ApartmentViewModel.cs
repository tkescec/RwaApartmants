using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels
{
    public class ApartmentViewModel
    {
        public Guid GUID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OwnerId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int CityId { get; set; }
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
        public IList<Tag> Tags { get; set; }
        public IList<ApartmentPicture> Pictures { get; set; }
        public IList<Review> Reviews { get; set; }
        
    }
}
