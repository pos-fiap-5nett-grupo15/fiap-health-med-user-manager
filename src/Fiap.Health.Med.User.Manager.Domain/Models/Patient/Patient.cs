namespace Fiap.Health.Med.User.Manager.Domain.Models.Patient
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Document { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
    }
}
