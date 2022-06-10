using System;

namespace DAL.Models
{
    [Serializable]
    public class Apartment
    {
        public int ApartmentID { get; set; }
        public string Owner { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public decimal Price { get; set; }
        public int? MaxAdults { get; set; }
        public int? MaxChildren { get; set; }
        public int? TotalRooms { get; set; }
        public int? BeachDistance { get; set; }
        public string Picture { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
