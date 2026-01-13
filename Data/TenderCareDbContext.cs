using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class TenderCareDbContext : DbContext
    {
        public TenderCareDbContext(DbContextOptions<TenderCareDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change "Patients" to "patients" if it is lowercase in DB
            modelBuilder.Entity<Patient>(e => {
                e.ToTable("patients");
                e.HasKey(p => p.PatientID);
            });

            // Change "Appointments" to "appointments" if it is lowercase in DB
            modelBuilder.Entity<Appointment>(e => {
                e.ToTable("appointments");
                e.HasKey(a => a.AppointmentID);
            });

            // FIXED: Pointing to the lowercase "users" table
            modelBuilder.Entity<User>(e => {
                e.ToTable("users");
                e.HasKey(u => u.UserID);

                // Maps the C# property to the lowercase column if necessary
                e.Property(u => u.Role).HasDefaultValue("User");
            });
        }
    }
}