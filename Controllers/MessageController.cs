using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetRehome.Controllers
{
    public class MessageController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
