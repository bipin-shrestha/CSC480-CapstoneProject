using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using PetRehome.Models;
using System.Security.Claims;

namespace PetRehome.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly PetRehomeDbContext _db;
        public MessageController(PetRehomeDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Index()
        {
            var role = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)              
                .Select(c => c.Value)
                .FirstOrDefault();
            int receiverId;
            IEnumerable<MessageViewModel> msgs = null;
            if (role == "Adopter")
            {
                receiverId = _db.Adopters.Where(x => x.Email == User.Identity.Name).FirstOrDefault().AdopterId;
                msgs = (from msg in _db.Messages
                        join shl in _db.Shelters on msg.SenderId equals shl.ShelterId
                        where msg.RecieverId == receiverId
                        select new MessageViewModel
                        {
                            SenderName = shl.ShelterLoginEmail,
                            MessageText = msg.MessageText,
                            SentDateTime = msg.SentDateTime,
                            IsRead = msg.MessageRead
                        }).ToList();
            }
            else
            {
                receiverId = _db.Shelters.Where(x => x.ShelterLoginEmail == User.Identity.Name).FirstOrDefault().ShelterId;
                msgs = (from msg in _db.Messages
                        join adp in _db.Adopters on msg.SenderId equals adp.AdopterId
                        where msg.RecieverId == receiverId
                        select new MessageViewModel
                        {
                            SenderName = adp.Email,
                            MessageText = msg.MessageText,
                            SentDateTime = msg.SentDateTime,
                            IsRead = msg.MessageRead
                        }).ToList();
            }
            
            return View(msgs);
        }

        [Authorize]
        public IActionResult SendMessage(int shelterId)
        {
            ViewBag.ShelterId = shelterId;
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult SendMessage(MessageViewModel model)
        {
            if (!ModelState.IsValid) {
                return View();
            }
            var msg = new Message();
            msg.MessageText = model.MessageText;
            var senderId = _db.Adopters.Where(x => x.Email.ToLower() == User.Identity.Name).Select(x => x.AdopterId).FirstOrDefault();
            msg.SenderId = senderId;
            msg.RecieverId = (int)model.RecieverId;
            msg.SentDateTime = DateTime.Now;
            msg.MessageRead = false;

            _db.Messages.Add(msg);
            _db.SaveChanges();

            TempData["UserMessage"] = "Message sent to Shelter Successfully!";
            return Redirect("/PetListing");
        }
    }
}
