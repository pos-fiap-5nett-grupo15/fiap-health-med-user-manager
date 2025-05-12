using Fiap.Health.Med.User.Manager.Domain.Models.Patient;

namespace Fiap.Health.Med.User.Manager.Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<(IEnumerable<Patient>, string)> GetAllAsync();
        Task<(Patient?, string)> GetByIdAsync(int id);
        Task<(Patient?, string)> GetByDocumentAsync(long document);
        Task<(bool, string)> AddAsync(Patient patient);
        Task<(bool, string)> UpdateAsync(Patient patient);
        Task<(bool, string)> DeleteAsync(int id);
    }
}
