using Fiap.Health.Med.User.Manager.Domain.Enum;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;

namespace Fiap.Health.Med.User.Manager.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByIdAsync(int id, CancellationToken ct);
        Task<Doctor?> GetByConcilAsync(string concilUf, int concilNumber, CancellationToken ct);
        Task<(IEnumerable<Doctor>, int)> GetByFilterAsync(string? doctorName, EMedicalSpecialty? doctorSpecialty, int? doctorConcilNumber, string? doctorCrmUf, int currentPage, int pageSize, CancellationToken ct);
        Task<int> AddAsync(Doctor doctor, CancellationToken ct);
        Task UpdateAsync(Doctor doctor, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}
