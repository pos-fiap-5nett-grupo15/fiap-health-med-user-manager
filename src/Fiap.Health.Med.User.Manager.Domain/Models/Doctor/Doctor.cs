using Fiap.Health.Med.User.Manager.Domain.Enum;

namespace Fiap.Health.Med.User.Manager.Domain.Models.Doctor
{
    public class Doctor
    {
        public int Id { get; set; }
        public int CrmNumber { get; set; }
        public string CrmUf { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public Especialidade MedicalSpecialty { get; set; }
    }
}
