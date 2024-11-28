using Microsoft.AspNetCore.Mvc;

namespace PetRehome.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
