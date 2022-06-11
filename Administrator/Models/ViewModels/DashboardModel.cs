using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administrator.Models.ViewModels
{
    public class DashboardModel
    {
        public int Apartments { get; set; }
        public int Reservations { get; set; }
        public int Reviews { get; set; }
        public int Users { get; set; }
    }
}