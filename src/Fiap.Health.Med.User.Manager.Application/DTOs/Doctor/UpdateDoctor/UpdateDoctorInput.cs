using Fiap.Health.Med.User.Manager.Domain.Enum;

namespace Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.UpdateDoctor
{
    public class UpdateDoctorInput
    {
        public int? CrmNumber { get; set; }
        public string? CrmUf { get; set; }
        public string? Name { get; set; }
        public string? HashedPassword { get; set; }
        public string? Email { get; set; } 
        public Especialidade? MedicalSpecialty { get; set; }
    }
}
