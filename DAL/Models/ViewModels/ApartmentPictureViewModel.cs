using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels
{
    public class ApartmentPictureViewModel
    {
        public Guid GUID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ApartmentId { get; set; }
        public string Path { get; set; }
        public string Base64Content { get; set; }
        public string Name { get; set; }
        public bool IsRepresentative { get; set; }
    }
}
