using System.ComponentModel.DataAnnotations;

namespace PetRehome.Models
{
    public class SignUpAdopterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage = "Must be valid email address")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please select a state")]
        public string State { get; set; }
        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
