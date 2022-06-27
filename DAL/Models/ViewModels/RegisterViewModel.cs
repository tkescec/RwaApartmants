using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DAL.Models.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            GUID = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        private string password;

        public Guid GUID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get => password; set => password = Crypto.HashPassword(value); }
        public int RoleId { get; set; }
    }
}
