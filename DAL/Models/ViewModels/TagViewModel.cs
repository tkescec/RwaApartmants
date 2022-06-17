using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ViewModels
{
    public class TagViewModel
    {
        public Guid GUID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public int TypeId { get; set; }
    }
}
