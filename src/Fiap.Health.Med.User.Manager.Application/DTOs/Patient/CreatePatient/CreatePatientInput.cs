namespace Fiap.Health.Med.User.Manager.Application.DTOs.Patient.CreatePatient
{
    public class CreatePatientInput
    {
        public required string Name { get; set; }
        public required long Document { get; set; }
        public required string HashedPassword { get; set; }
        public required string Email { get; set; }
    }
}
