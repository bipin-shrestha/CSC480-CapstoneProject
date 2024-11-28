using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PetRehome.Models;

namespace PetRehome.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionHandlerFeature?.Error;

            return View(new ErrorViewModel
            {
                ErrorMessage = exception?.Message,
                StackTrace = exception?.StackTrace
            });
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleStatusCode(int statusCode)
        {
            var viewModel = new ErrorViewModel
            {
                StatusCode = statusCode
            };

            return View("HandleError", viewModel);
        }
    }
}
