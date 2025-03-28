namespace Fiap.Health.Med.User.Manager.Application.DTOs.Patient.GetPatient
{
    public class GetPatientOutput
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long Document { get; set; }
        public string HashedPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
