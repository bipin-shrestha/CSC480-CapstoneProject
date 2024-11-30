using System.ComponentModel.DataAnnotations;

namespace PetRehome.Models
{
    public class PetViewModel
    {
        [Required]
        [StringLength(100)]
        public string PetName { get; set; }

        [Required]
        [StringLength(50)]
        public string Breed { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageIds { get; set; }

        public string TrainingLevel { get; set; }

        public string Tags { get; set; }

        public bool Neutered { get; set; }

        public bool Declawed { get; set; }

        public string ExcerciseRequirement { get; set; }

        public string FurType { get; set; }

        [Required]
        public string Location { get; set; }

        public string MedicalHistory { get; set; }

        [Required]
        public string PetType { get; set; }

        public string SheddingLevel { get; set; }

        [Required]
        public string Size { get; set; }

        public string SocialLevel { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
