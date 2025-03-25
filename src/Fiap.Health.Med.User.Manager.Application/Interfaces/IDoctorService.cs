using Fiap.Health.Med.User.Manager.Application.DTOs;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;

namespace Fiap.Health.Med.User.Manager.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<Result<List<DoctorResponseDto>>> GetAllAsync();
        Task<Result<DoctorResponseDto>> GetByIdAsync(int id);
        Task<Result<int>> AddAsync(Doctor doctor);
        Task<Result> UpdateAsync(Doctor doctor);
        Task<Result> DeleteAsync(int id);

    }
}
