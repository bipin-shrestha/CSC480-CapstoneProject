using System.ComponentModel.DataAnnotations;

namespace PetRehome.Models
{
    public class SignUpShelterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage = "Must be valid email address")]
        public string ShelterLoginEmail { get; set; }
        [Required(ErrorMessage = "Shelter Name is required")]
        public string ShelterName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
