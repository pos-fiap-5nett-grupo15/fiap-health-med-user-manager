namespace Fiap.Health.Med.User.Manager.Application.DTOs.Patient.UpdatePatient
{
    public class UpdatePatientInput
    {
        public string? Name { get; set; }
        public long? Document { get; set; }
        public string? HashedPassword { get; set; }
        public string? Email { get; set; }
    }
}