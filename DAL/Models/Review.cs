using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Serializable]
    public class Review
    {
        public int ReviewID { get; set; }
        public string Apartment { get; set; }
        public string UserName { get; set; }
        public string Details { get; set; }
        public int Stars { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
