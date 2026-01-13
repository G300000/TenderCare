namespace Project.Models
{
    public class DashboardViewModel
    {
        public int TotalPatients { get; set; }
        public int TotalAppointments { get; set; }
        public int TotalConsultations { get; set; }
        public List<PatientListItem> RecentPatients { get; set; } = new();
    }

    public class PatientListItem
    {
        public string PatientID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Gender { get; set; }
        public DateTime? LastVisit { get; set; }
        public string Guardian { get; set; } = string.Empty;
    }
}