using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace PetRehome.Models
{
    public class PetRehomeDbContext: DbContext
    {
        public PetRehomeDbContext(DbContextOptions<PetRehomeDbContext> option) : base(option) { }
        public DbSet<Adopter> Adopters { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Message> Messages { get; set; }
    }

    public class Adopter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdopterId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set;}
        [Required]
        public string ZipCode { get; set;}
        public string PhoneNumber { get; set;}
        public int UserId { get; set; }
    }

    public class Shelter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShelterId { get; set; }
        [Required]
        public string ShelterName { get; set; }
        [Required]
        public string ShelterAddress { get; set; }
        [Required]
        public string ShelterLoginEmail { get; set; }
        public int UserId { get; set; }
    }

    public class Match
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchId { get; set; }
        public int ShelterId { get; set; }
        public int AdopterId { get; set; }
    }

    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
    }

    public class Pet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PetId { get; set; }
        public string PetName { get; set; }
        public string Breed { get; set; }
        public string PetType { get; set; }
        public string ? Size { get; set; }
        public string ? FurType { get; set; }
        public string ? SheddingLevel { get;set; }
        public string ? TrainingLevel { get; set; }
        public string ? ExcerciseRequirement { get; set; }
        public bool ? Neutered { get; set; }
        public bool ? Declawed { get; set; }
        public string ? SocailLevel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ? Description { get; set; }
        public List<int> ? ImageIds { get; set; }
        public List<string> ? MedicalHistory { get; set; }
        public List<string> ? Tags { get;set; }
        public string ? Status { get; set; }
        public string ? Location { get; set; }
        public int ? ShelterId { get; set; }
        public int ? AdopterId { get; set; }
    }

    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int RecieverId { get; set; }
        [Required]
        public string MessageText { get; set; }
        [Required]
        public DateTime SentDateTime { get; set; }
        [Required]
        public DateTime ReadDateTime { get; set; }
        public bool MessageRead { get; set; }
    }
}
