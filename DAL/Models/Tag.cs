using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Serializable]
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string TypeName { get; set; }
        public string TypeNameEng { get; set; }
        public string Picture { get; set; }
        public int ApartmentCount { get; set; }
    }
}
