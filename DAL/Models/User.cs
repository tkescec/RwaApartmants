using System;

namespace DAL.Models
{
    [Serializable]
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
