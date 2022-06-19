using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels
{
    public class ReservationViewModel
    {
        public Guid GUID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ApartmentID { get; set; }
        public int? UserID { get; set; }
        public string Details { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }
    }
}
