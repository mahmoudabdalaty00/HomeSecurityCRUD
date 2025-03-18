using Server.Models.Entities;

namespace Server.Models.Dtos
{
    public class UpdateDeviceDto
    {

        public int DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string DeviceType { get; set; }

        public string Status { get; set; } // e.g., Online, Offline
         
        // Foreign Key to House (Device belongs to a house)
        public int? HouseId { get; set; }
        public House House { get; set; }
    }
}