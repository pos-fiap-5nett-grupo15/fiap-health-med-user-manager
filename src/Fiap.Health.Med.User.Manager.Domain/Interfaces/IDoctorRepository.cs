using Fiap.Health.Med.User.Manager.Domain.Enum;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;

namespace Fiap.Health.Med.User.Manager.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(int id);
        Task<Doctor?> GetByConcilAsync(string concilUf, int concilNumber);
        Task<IEnumerable<Doctor>> GetByFilterAsync(string? doctorName, EMedicalSpecialty? doctorSpecialty, int? doctorConcilNumber, string? doctorCrmUf);
        Task<int> AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(int id);
    }
}
