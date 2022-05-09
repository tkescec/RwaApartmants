using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administrator.Models.ViewModels
{
    public class SweetAlert
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Html { get; set; }
        public string Position { get; set; }
        public bool ShowCloseButton { get; set; }
        public bool ShowCancelButton { get; set; }
        public bool ShowConfirmButton { get; set; }
        public int? Timer { get; set; }
    }
}