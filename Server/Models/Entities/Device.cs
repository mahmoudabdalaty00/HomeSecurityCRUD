using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }

        [Required]
        public string DeviceName { get; set; }

        [Required]
        public string DeviceType { get; set; }

        [Required]
        public string Status { get; set; } // e.g., Online, Offline

        // Foreign Key to User
        public string UserId { get; set; }
        public User User { get; set; }

        // Foreign Key to House (Device belongs to a house)
        public int? HouseId { get; set; }
        public House House { get; set; }
    }

}
