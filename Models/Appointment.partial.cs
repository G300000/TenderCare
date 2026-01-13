using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    // Keeping 'partial' allows C# to merge this file with the hidden generated one
    public partial class Appointment
    {
        // This is the new field. It will work once you run the SQL command below.
        public string? Notes { get; set; }

        [NotMapped]
        public string DisplayDate => AppointmentDate?.ToString("g") ?? "TBD";
    }
}