using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listeo.Models
{
    public class FiltersCookieViewModel
    {
        public int Rooms { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string City { get; set; }
        public int Sort { get; set; }
    }
}