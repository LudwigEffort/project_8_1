using LabWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LabWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<ComputerSoftware> ComputerSoftwares { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<LabUser> LabUsers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Software> Softwares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Computer>()
                    .HasOne(c => c.Item)
                    .WithOne(i => i.Computer)
                    .HasForeignKey<Computer>(c => c.Id);

            modelBuilder.Entity<ComputerSoftware>()
                    .HasKey(cs => new { cs.ComputerId, cs.SoftwareId });
            modelBuilder.Entity<ComputerSoftware>()
                    .HasOne(c => c.Computer)
                    .WithMany(cs => cs.ComputerSoftwares)
                    .HasForeignKey(c => c.ComputerId);
            modelBuilder.Entity<ComputerSoftware>()
                    .HasOne(s => s.Software)
                    .WithMany(cs => cs.ComputerSoftwares)
                    .HasForeignKey(s => s.SoftwareId);
        }
    }
}