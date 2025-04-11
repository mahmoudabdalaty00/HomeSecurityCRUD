using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Models.Entities;
using static DeviceConfiguration;

namespace Server.Date
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<History> Histories { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Alarm> Alarms { get; set; }

        //public DbSet<Camera> Cameras { get; set; }
        //public DbSet<Sensor> Sensors { get; set; }
        // public DbSet<User> Users { get; set; }
        //public DbSet<AccessLog> AccessLogs { get; set; }
        //public DbSet<EventLog> EventLogs { get; set; }
        //public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        //public DbSet<UserSetting> UserSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply default Identity configurations
            base.OnModelCreating(modelBuilder);

            // Custom configurations for other entities
            modelBuilder.Entity<House>()
                .HasMany(h => h.Devices)
                .WithOne(d => d.House)
                .HasForeignKey(d => d.HouseId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new HouseConfiguration());

        }
    }
}




















