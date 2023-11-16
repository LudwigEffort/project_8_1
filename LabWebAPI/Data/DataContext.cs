using LabWebAPI.Model;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace LabWebAPI.Data
{
        public class DataContext : DbContext
        {
                public DataContext(DbContextOptions<DataContext> options) : base(options) { }

                public DbSet<Item> Items { get; set; }
                public DbSet<ItemSoftware> ItemSoftwares { get; set; }
                public DbSet<LabUser> LabUsers { get; set; }
                public DbSet<Reservation> Reservations { get; set; }
                public DbSet<Room> Rooms { get; set; }
                public DbSet<Software> Softwares { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                {
                        modelBuilder.Entity<ItemSoftware>()
                                .HasKey(isr => new { isr.ItemId, isr.SoftwareId });
                        modelBuilder.Entity<ItemSoftware>()
                                .HasOne(i => i.Item)
                                .WithMany(isr => isr.ItemSoftwares)
                                .HasForeignKey(i => i.ItemId);
                        modelBuilder.Entity<ItemSoftware>()
                                .HasOne(s => s.Software)
                                .WithMany(isr => isr.ItemSoftwares)
                                .HasForeignKey(s => s.SoftwareId);
                }
        }
}