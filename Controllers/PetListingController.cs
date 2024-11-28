using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetRehome.Models;

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

        public IActionResult Index()
        {
            var pets = _db.Pets.ToList();
            return View(pets);
        }

        public IActionResult Details(string id)
        {
            var pet = _db.Pets.FirstOrDefault(p => p.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }
    }
}
