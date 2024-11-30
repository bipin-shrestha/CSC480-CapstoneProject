using Microsoft.AspNetCore.Mvc;
using PetRehome.Models;
using System.Security.Cryptography;
using System.Text;

namespace PetRehome.Controllers
{
    public class ShelterSignupController : Controller
    {
        private readonly PetRehomeDbContext _db;
        private const int keySize = 64;
        private const int iterations = 350000;

        public ShelterSignupController(PetRehomeDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(SignUpShelterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (CheckEmailExists(model.ShelterLoginEmail))
            {
                ModelState.AddModelError("ShelterLoginEmail", "Username taken");
                return View(model);
            }

            var trans = _db.Database.BeginTransaction();
            try
            {
                HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
                var salt = RandomNumberGenerator.GetBytes(keySize);
                var hash = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(model.Password),
                    salt,
                    iterations,
                    hashAlgorithm,
                    keySize);

                var user = new User();
                user.UserName = model.ShelterLoginEmail;
                user.Password = Convert.ToHexString(hash);
                user.PasswordSalt = Convert.ToHexString(salt);
                _db.Users.Add(user);
                _db.SaveChanges();


                var shelter = new Shelter();
                shelter.ShelterName = model.ShelterName;
                shelter.ShelterLoginEmail = model.ShelterLoginEmail;
                shelter.ShelterAddress = model.Address;
                shelter.UserId = user.UserId;

                _db.Shelters.Add(shelter);
                _db.SaveChanges();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
            }

            return Redirect("/login");
        }

        private bool CheckEmailExists(string email)
        {
            return _db.Users.Where(x => x.UserName.ToLower() == email.ToLower()).Any();
        }
    }
}
