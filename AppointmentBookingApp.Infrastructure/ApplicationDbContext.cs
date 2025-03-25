using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppointmentBookingApp.Domain.Entities;

namespace AppointmentBookingApp.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<ProfessionalAvailability> ProfessionalAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Appointment -> ProfessionalAvailability relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.ProfessionalAvailability)
                .WithMany()
                .HasForeignKey(a => a.ProfessionalAvailabilityId)
                .OnDelete(DeleteBehavior.NoAction);

            // Appointment -> Professional relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Professional)
                .WithMany()
                .HasForeignKey(a => a.ProfessionalId)
                .OnDelete(DeleteBehavior.NoAction); 

            // Appointment -> User (ApplicationUser) relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);  
        }
    }

}

