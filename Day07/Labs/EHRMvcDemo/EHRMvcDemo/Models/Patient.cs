using System.ComponentModel.DataAnnotations;

namespace EHRMvcDemo.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        public Guid PatientGuid { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public string? SSN { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public string FullName => FirstName + " " + LastName;
    }
}
