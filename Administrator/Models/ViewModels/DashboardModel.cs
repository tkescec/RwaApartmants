﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administrator.Models.ViewModels
{
    public class DashboardModel
    {
        public int Apartmants { get; set; }
        public int Reservations { get; set; }
        public int Reviewes { get; set; }
        public int Users { get; set; }
    }
}