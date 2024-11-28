using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetRehome.Models
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Username is required")]
        [EmailAddress (ErrorMessage = "Must be valid email address")]
        public string UserName {  get; set; }

        [Required (ErrorMessage = "Password is required")]
        [PasswordPropertyText]
        public string Password { get; set; }

        public string? UserType { get; set; }
    }
}
