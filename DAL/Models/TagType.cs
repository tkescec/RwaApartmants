using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Serializable]
    public class TagType
    {
        public int TypeID { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
    }
}
