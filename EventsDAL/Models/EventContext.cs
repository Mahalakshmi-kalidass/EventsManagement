using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.Models
{
    public class EventContext :  DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<EventAllocation> EventAllocations  { get; set; }
        public DbSet<TopicCovered> TopicsCovered { get; set; }
        public DbSet<EventAccess> EventAccesses { get; set; }

        public DbSet<User> Users { get; set; }
        //Exception logging
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseHelper.GetConnecionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreignKey configuration
            modelBuilder.Entity<EventAllocation>().HasOne<Event>().WithMany().HasForeignKey(ea => ea.EventId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EventAllocation>().HasOne<Location>().WithMany().HasForeignKey(ea => ea.LocationId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EventAllocation>().HasOne<Staff>().WithMany().HasForeignKey(ea => ea.StaffId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Staff>().HasOne<Location>().WithMany().HasForeignKey(s => s.LocationId);
           

            modelBuilder.Entity<TopicCovered>().HasOne<Location>().WithMany().HasForeignKey(tc => tc.LocationId);
            modelBuilder.Entity<TopicCovered>().HasOne<Staff>().WithMany().HasForeignKey(tc => tc.StaffId);
            modelBuilder.Entity<TopicCovered>().HasOne<Event>().WithMany().HasForeignKey(tc => tc.EventId);

            modelBuilder.Entity<Event>().HasData(
                new Event { EventId = Guid.NewGuid(), EventName = "Ignite", EventDate = DateTime.Now, EventDescription = "This is an annual event held by microsoft" },
                new Event { EventId = Guid.NewGuid(), EventName = "Microsoft Build", EventDate = DateTime.Now, EventDescription = "This is an annual event held by microsoft" }

                ) ;
            modelBuilder.Entity<EventAccess>().HasOne<User>().WithMany().HasForeignKey(ea => ea.UserId);
            modelBuilder.Entity<EventAccess>().HasOne<Event>().WithMany().HasForeignKey(ea => ea.EventId);

            modelBuilder.Entity<Location>().HasData(

                new Location { LocationId = Guid.NewGuid(), LocationName = "Chennai" },
                new Location { LocationId = Guid.NewGuid(), LocationName = "Mumbai" },
                new Location { LocationId = Guid.NewGuid(), LocationName = "Seatle" },
                new Location { LocationId = Guid.NewGuid(), LocationName = "LosAngels" }

                );

            modelBuilder.Entity<User>().Property(u => u.UserRole).HasConversion<int?>().HasDefaultValue(null);

            base.OnModelCreating(modelBuilder);
        }
    }
}
