using System.ComponentModel.DataAnnotations;

namespace EHRMvcDemo.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }

        public string ReasonForVisit { get; set; }
        public string Status { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
