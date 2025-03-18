using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Models.Entities;

namespace Server.Date
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<History> Histories { get; set; }

        //i didnot update migration for these entities
        public DbSet<User> Users { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
    }
}
