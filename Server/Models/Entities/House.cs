 using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class House
    {
        [Key]
        public int HouseId { get; set; }

        [Required]
        public string Name { get; set; } // Example: "John's Home"

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Country { get; set; }

        public string Description { get; set; } // Optional house description

        // Foreign Key: Each house belongs to a user
        public string UserId { get; set; }
        public User User { get; set; }

        // Navigation property: A house can have multiple devices
        public ICollection<Device> Devices { get; set; }
    }

}
