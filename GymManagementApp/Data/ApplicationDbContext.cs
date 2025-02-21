using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymManagementApp.Data.Entities;
using GymManagementApp.Models;

namespace GymManagementApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<TrainerEntity> Trainers { get; set; }
        public DbSet<GymUser> GymUsers { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<WorkoutClass> WorkoutClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ważne dla Identity!

            modelBuilder.Entity<Membership>().HasData(
                new Membership { Id = 1, Type = "Miesięczny", Price = 100 },
                new Membership { Id = 2, Type = "Roczny", Price = 1000 },
                new Membership { Id = 3, Type = "Jednorazowy", Price = 20 }
            );
        }
    }
}
