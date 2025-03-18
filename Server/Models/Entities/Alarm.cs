using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class Alarm
    {
        [Key]
        public int AlarmId { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required, StringLength(50)]
        public string AlarmType { get; set; } // Siren, Silent

        [Required, StringLength(50)]
        public string Status { get; set; } // Triggered, Off

        public DateTime? TriggerTime { get; set; }

        // Navigation Property
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
    }

}
