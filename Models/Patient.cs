using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Patient
    {
        [Key]
        public string PatientID { get; set; } = string.Empty;
        [Required]
        public string PatientName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        [Required]
        public string GuardianName { get; set; } = string.Empty;
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

    public partial class Appointment
    {
        [Key]
        public string AppointmentID { get; set; } = string.Empty;
        [Required]
        public string PatientID { get; set; } = string.Empty;
        public string? ServiceType { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Status { get; set; } = "Scheduled";
        public virtual Patient? Patient { get; set; }
    }

    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        // Add this to match your DB column
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

}