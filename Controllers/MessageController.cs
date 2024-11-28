using Microsoft.AspNetCore.Mvc;

namespace PetRehome.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
