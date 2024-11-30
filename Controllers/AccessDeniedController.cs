using Microsoft.AspNetCore.Mvc;

namespace PetRehome.Controllers
{
    public class AccessDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
