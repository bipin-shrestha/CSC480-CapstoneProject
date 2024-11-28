using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetRehome.Models;
using System.Security.Cryptography;
using System.Text;

namespace PetRehome.Controllers
{
    public class AdopterSignupController : Controller
    {
        private readonly PetRehomeDbContext _db;
        private const int keySize = 64;
        private const int iterations = 350000;

        public AdopterSignupController(PetRehomeDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string link)
        {       
            ViewBag.States = GetStateValues();
            return View();            
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index(SignUpAdopterViewModel model)
        {
            ViewBag.State = GetStateValues();
            if (!ModelState.IsValid)
                return View(model);

            if (CheckEmailExists(model.Username))
            {
                ModelState.AddModelError("Username", "Username taken");
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
                user.UserName = model.Username;
                user.Password = Convert.ToHexString(hash);
                user.PasswordSalt = Convert.ToHexString(salt);
                _db.Users.Add(user);
                _db.SaveChanges();
                

                var adopter = new Adopter();
                adopter.FirstName = model.Firstname;
                adopter.LastName = model.Lastname;
                adopter.Address = model.Address;
                adopter.City = model.City;
                adopter.State = model.State;
                adopter.PhoneNumber = model.PhoneNumber;
                adopter.ZipCode = model.ZipCode;
                adopter.Email = model.Username;
                adopter.UserId = user.UserId;

                _db.Adopters.Add(adopter);
                _db.SaveChanges();
                trans.Commit();
            }
            catch(Exception ex)
            {
                trans.Rollback();
            }
            
            return View(model);
        }

        private List<SelectListItem> GetStateValues()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "AL", Text = "Alabama" },
                new SelectListItem { Value = "AK", Text = "Alaska" },
                new SelectListItem { Value = "AZ", Text = "Arizona" },
                new SelectListItem { Value = "AR", Text = "Arkansas" },
                new SelectListItem { Value = "CA", Text = "California" },
                new SelectListItem { Value = "CO", Text = "Colorado" },
                new SelectListItem { Value = "CT", Text = "Connecticut" },
                new SelectListItem { Value = "DE", Text = "Delaware" },
                new SelectListItem { Value = "FL", Text = "Florida" },
                new SelectListItem { Value = "GA", Text = "Georgia" },
                new SelectListItem { Value = "HI", Text = "Hawaii" },
                new SelectListItem { Value = "ID", Text = "Idaho" },
                new SelectListItem { Value = "IL", Text = "Illinois" },
                new SelectListItem { Value = "IN", Text = "Indiana" },
                new SelectListItem { Value = "IA", Text = "Iowa" },
                new SelectListItem { Value = "KS", Text = "Kansas" },
                new SelectListItem { Value = "KY", Text = "Kentucky" },
                new SelectListItem { Value = "LA", Text = "Louisiana" },
                new SelectListItem { Value = "ME", Text = "Maine" },
                new SelectListItem { Value = "MD", Text = "Maryland" },
                new SelectListItem { Value = "MA", Text = "Massachusetts" },
                new SelectListItem { Value = "MI", Text = "Michigan" },
                new SelectListItem { Value = "MN", Text = "Minnesota" },
                new SelectListItem { Value = "MS", Text = "Mississippi" },
                new SelectListItem { Value = "MO", Text = "Missouri" },
                new SelectListItem { Value = "MT", Text = "Montana" },
                new SelectListItem { Value = "NE", Text = "Nebraska" },
                new SelectListItem { Value = "NV", Text = "Nevada" },
                new SelectListItem { Value = "NH", Text = "New Hampshire" },
                new SelectListItem { Value = "NJ", Text = "New Jersey" },
                new SelectListItem { Value = "NM", Text = "New Mexico" },
                new SelectListItem { Value = "NY", Text = "New York" },
                new SelectListItem { Value = "NC", Text = "North Carolina" },
                new SelectListItem { Value = "ND", Text = "North Dakota" },
                new SelectListItem { Value = "OH", Text = "Ohio" },
                new SelectListItem { Value = "OK", Text = "Oklahoma" },
                new SelectListItem { Value = "OR", Text = "Oregon" },
                new SelectListItem { Value = "PA", Text = "Pennsylvania" },
                new SelectListItem { Value = "RI", Text = "Rhode Island" },
                new SelectListItem { Value = "SC", Text = "South Carolina" },
                new SelectListItem { Value = "SD", Text = "South Dakota" },
                new SelectListItem { Value = "TN", Text = "Tennessee" },
                new SelectListItem { Value = "TX", Text = "Texas" },
                new SelectListItem { Value = "UT", Text = "Utah" },
                new SelectListItem { Value = "VT", Text = "Vermont" },
                new SelectListItem { Value = "VA", Text = "Virginia" },
                new SelectListItem { Value = "WA", Text = "Washington" },
                new SelectListItem { Value = "WV", Text = "West Virginia" },
                new SelectListItem { Value = "WI", Text = "Wisconsin" },
                new SelectListItem { Value = "WY", Text = "Wyoming" }
            };
        }

        private bool CheckEmailExists(string email)
        {
            return _db.Users.Where(x => x.UserName.ToLower() == email.ToLower()).Any();
        }
    }
}
