using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetRehome.Models;
using System.Security.Claims;

namespace PetRehome.Controllers
{
    [Authorize]
    public class PetListingController : Controller
    {
        private readonly PetRehomeDbContext _db;

        public PetListingController(PetRehomeDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Index()
        {
            var pets = new List<Pet>();
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value == "Shelter") != null)
            {
                var userName = User.Identity.Name;
                var shelterId = _db.Shelters.Where(s => s.ShelterLoginEmail == userName).Select(s => s.ShelterId).FirstOrDefault();
                pets = _db.Pets.Where(x => x.ShelterId == shelterId).ToList();
            }
            else
            {
                pets = _db.Pets.ToList();
            } 
            return View(pets);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var pet = _db.Pets.FirstOrDefault(p => p.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        [Authorize(Roles = "Shelter")]
        public IActionResult AddPets()
        {
            return View();
        }

        [Authorize(Roles = "Shelter")]
        public IActionResult EditPets()
        {
            return View();
        }

        public JsonResult GetBreedsByType(string petType)
        {
            List<string> breedList = new List<string>();

            if (petType == "Dog")
            {
                breedList = new List<string>
                {
                    "Golden Retriever",
                    "Labrador Retriever",
                    "German Shepherd",
                    "Bulldog",
                    "Poodle",
                    "Beagle",
                    "Rottweiler",
                    "Siberian Husky",
                    "Shih Tzu",
                    "Dachshund",
                    "Boxer",
                    "Chihuahua",
                    "Doberman Pinscher",
                    "Cocker Spaniel",
                    "Border Collie",
                    "Australian Shepherd",
                    "Great Dane",
                    "Saint Bernard",
                    "Bichon Frise",
                    "Maltese"
                };
            }
            else if (petType == "Cat")
            {
                breedList = new List<string>
                {
                    "Abyssinian",
                    "Bengal",
                    "Birman",
                    "British Shorthair",
                    "Maine Coon",
                    "Persian",
                    "Ragdoll",
                    "Siamese",
                    "Sphynx",
                    "Scottish Fold",
                    "American Shorthair",
                    "Russian Blue",
                    "Exotic Shorthair",
                    "Savannah",
                    "Burmese",
                    "Oriental Shorthair",
                    "Manx",
                    "Turkish Angora",
                    "Egyptian Mau",
                    "Somali"
                };
            }

            return Json(breedList);
        }

    }
}
