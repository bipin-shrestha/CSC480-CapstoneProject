using Microsoft.AspNetCore.Mvc;

namespace PetRehome.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
