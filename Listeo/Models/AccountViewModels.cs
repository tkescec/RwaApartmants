using System.ComponentModel.DataAnnotations;

namespace Listeo.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; }


        public static explicit operator DAL.Models.ViewModels.RegisterViewModel(RegisterViewModel obj)
        {
            DAL.Models.ViewModels.RegisterViewModel output = new DAL.Models.ViewModels.RegisterViewModel()
            {
                Username = obj.Username,
                Email = obj.Email,
                Password = obj.Password,
                RoleId = obj.RoleId,
            };

            return output;
        }
    }
}
