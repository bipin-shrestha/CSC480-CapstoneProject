using System.ComponentModel.DataAnnotations;

namespace PetRehome.Models
{
    public class PetViewModel
    {
        [Required(ErrorMessage = "Pet name is required")]
        [StringLength(50)]
        public string PetName { get; set; }

        [Required(ErrorMessage = "Breed is required")]
        [StringLength(50)]
        public string Breed { get; set; }

        [Required(ErrorMessage = "Pet Date of Birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Pet Desription is required")]
        public string Description { get; set; }

        public string TrainingLevel { get; set; }

        public bool Neutered { get; set; }

        public string ExcerciseRequirement { get; set; }

        [Required(ErrorMessage = "Pet Location is required")]
        public string Location { get; set; }

        public string MedicalHistory { get; set; }

        [Required(ErrorMessage = "Pet Type is required")]
        public string PetType { get; set; }

        [Required(ErrorMessage = "Pet Size is required")]
        public string Size { get; set; }

        public string SocialLevel { get; set; }

        public IFormFile? Image { get; set; }
    }
}
