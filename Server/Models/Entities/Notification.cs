using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required, StringLength(255)]
        public string Message { get; set; }

        [Required, StringLength(50)]
        public string Type { get; set; } // Alert, Reminder

        [Required, StringLength(50)]
        public string Status { get; set; } // Read, Unread

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}
