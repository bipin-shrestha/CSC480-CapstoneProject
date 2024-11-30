using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PetRehome.Models
{
    public class MessageViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string SenderName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string SenderEmail { get; set; }
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }
        public int SenderId { get; set; }   
        public int? RecieverId { get; set; }
        [Required(ErrorMessage ="Message body is required")]
        public string MessageText { get; set; } 
        public DateTime SentDateTime { get; set; }
        public DateTime ReadDateTIme { get; set; }
        public bool IsRead { get; set; }
    }
}
