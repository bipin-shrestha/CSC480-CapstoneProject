using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetRehome.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Transactions;

namespace PetRehome.Controllers
{
    public class LoginController : Controller
    {
        private readonly PetRehomeDbContext _db;
        private const int keySize = 64;
        private const int iterations = 350000;

        public LoginController(PetRehomeDbContext db) 
        {
            _db = db;
        }

        public IActionResult Index(string linkValue)
        {
            if (!string.IsNullOrEmpty(linkValue))
                ViewBag.link = linkValue;
            else
                ViewBag.link = "Adopter";
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var user = _db.Users.FirstOrDefault(x => x.UserName.ToLower() == model.UserName);      

            if (user == null)
            {
                ViewBag.Error = "Invalid Username or Password!";
                return View();
            }
            
            var isValid = VerifyPassword(model.Password, user.Password, user.PasswordSalt);

            if (isValid)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserType", model.UserType)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index","PetListing");
            }

            ViewBag.Error = "Invalid Username or Password!";
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.LogoutMessage = "Log out successful!";
            return RedirectToAction("Index", "Home");
        }

        private bool VerifyPassword(string password, string hash, string salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            var saltByte = ConvertToByteArray(salt);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, saltByte, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        private static byte[] ConvertToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexValues(hex[i << 1]) << 4) + (GetHexValues(hex[(i << 1) + 1])));
            }

            return arr;
        }

        private static int GetHexValues(char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
    }
}
