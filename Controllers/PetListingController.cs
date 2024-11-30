using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetRehome.Models;
using System.Security.Claims;

namespace PetRehome.Controllers
{
    [Authorize]
    public class PetListingController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PetRehomeDbContext _db;

        public PetListingController(PetRehomeDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        [Authorize]
        public IActionResult Index(string selectedBreed, string selectedType, string selectedLocation)
        {
            ViewBag.BreedOptions = _db.Pets.Select(p => p.Breed).Distinct().OrderBy(b => b).ToList();
            ViewBag.TypeOptions = _db.Pets.Select(p => p.PetType).Distinct().OrderBy(t => t).ToList();
            ViewBag.LocationOPtions = _db.Pets.Select(p => p.Location).Distinct().OrderBy(l => l).ToList();
            var pets = new List<Pet>();
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value == "Shelter") != null)
            {
                var userName = User.Identity.Name;
                var shelterId = _db.Shelters.Where(s => s.ShelterLoginEmail == userName).Select(s => s.ShelterId).FirstOrDefault();
                pets = _db.Pets.Where(x => x.ShelterId == shelterId).ToList();
                if (!string.IsNullOrEmpty(selectedBreed))
                {
                    pets = pets.Where(p => p.Breed == selectedBreed).ToList();
                }
                if (!string.IsNullOrEmpty(selectedType))
                {
                    pets = pets.Where(p => p.PetType == selectedType).ToList();
                }

                if (!string.IsNullOrEmpty(selectedLocation))
                {
                    pets = pets.Where(p => p.Location == selectedLocation).ToList();
                }
            }
            else
            {
                pets = _db.Pets.ToList();
                if (!string.IsNullOrEmpty(selectedBreed))
                {
                    pets = pets.Where(p => p.Breed == selectedBreed).ToList();
                }
                if (!string.IsNullOrEmpty(selectedType))
                {
                    pets = pets.Where(p => p.PetType == selectedType).ToList();
                }

                if (!string.IsNullOrEmpty(selectedLocation))
                {
                    pets = pets.Where(p => p.Location == selectedLocation).ToList();
                }
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
        [HttpPost]
        public IActionResult AddPets(PetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbPet = new Pet();
            dbPet.Status = "Available";
            dbPet.PetName = model.PetName;
            dbPet.Breed = model.Breed;
            dbPet.Description = model.Description;

            var shelterId = _db.Shelters.Where(x => x.ShelterLoginEmail == User.Identity.Name).FirstOrDefault().ShelterId;
            dbPet.ShelterId = shelterId;
            dbPet.PetType = model.PetType;
            dbPet.Neutered = model.Neutered;
            dbPet.Location = model.Location;
            dbPet.Size = model.Size;
            dbPet.DateOfBirth = model.DateOfBirth;
            dbPet.SocailLevel = model.SocialLevel;
            dbPet.ExcerciseRequirement = model.ExcerciseRequirement;
            dbPet.AdopterId = 0;
            dbPet.Neutered = true;
            dbPet.Declawed = false;

            var imgId = new List<int>();
            imgId.Add(1);
            dbPet.ImageIds = imgId;

            _db.Pets.Add(dbPet);
            _db.SaveChanges();

            if(model.Image != null)
            {
                string uploadLocation = Path.Combine(_webHostEnvironment.WebRootPath, "img/pets");
                string petImageName = dbPet.PetId.ToString() + ".jpg";
                string filePath = Path.Combine(uploadLocation, petImageName);
                var image = model.Image;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }


            TempData["UserMessage"] = model.PetName + " was added successfully!";

            return Redirect("/PetListing");
        }

        [Authorize(Roles = "Shelter")]
        [HttpGet]
        public IActionResult DeletePets (string petId)
        {
            var pet = _db.Pets.Find(petId);

            if(pet == null)
            {
                return NotFound();
            }

            _db.Pets.Remove(pet);
            _db.SaveChanges();

            TempData["UserMessage"] = pet.PetName + " was deleted successfully!";

            return Redirect("/PetListing");
        }

        [Authorize(Roles = "Shelter")]
        public IActionResult EditPets(string id)
        {
            var pet = _db.Pets.FirstOrDefault(p => p.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        [Authorize(Roles = "Shelter")]
        [HttpPost]
        public IActionResult EditPets(Pet pet)
        {
            if (!ModelState.IsValid)
                return View(pet);

            var dbPet = _db.Pets.Find(pet.PetId);
            if (pet == null)
            {
                return NotFound();
            }
            dbPet.PetName = pet.PetName;
            dbPet.Neutered = pet.Neutered;
            dbPet.Breed = pet.Breed;
            dbPet.DateOfBirth = pet.DateOfBirth;
            dbPet.Description = pet.Description;
            dbPet.TrainingLevel = pet.TrainingLevel;
            dbPet.ExcerciseRequirement = pet.ExcerciseRequirement;
            dbPet.Location = pet.Location;
            dbPet.Size = pet.Size;
            _db.SaveChanges();
            TempData["UserMessage"] = pet.PetName + " was updated successfully!";
            return Redirect("/PetListing");
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
