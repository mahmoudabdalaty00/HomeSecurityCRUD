using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required, EmailAddress, StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        public string Role { get; set; } // Admin, User

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Device> Devices { get; set; }
        public ICollection<AccessLog> AccessLogs { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<EmergencyContact> EmergencyContacts { get; set; }
        public UserSetting UserSetting { get; set; }
    }
}