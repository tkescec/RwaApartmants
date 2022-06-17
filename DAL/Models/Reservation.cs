using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Serializable]
    public class Reservation
    {
        public int ReservationID { get; set; }
        public string Apartment { get; set; }
        public string Details { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public string Picture { get; set; }
    }
}
