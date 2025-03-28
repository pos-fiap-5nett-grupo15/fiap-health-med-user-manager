using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.CreateDoctor;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.GetDoctorById;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.UpdateDoctor;

namespace Fiap.Health.Med.User.Manager.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<Result<List<GetDoctorOutput>>> GetAllAsync();
        Task<Result<GetDoctorOutput>> GetByIdAsync(int id);
        Task<Result<int>> AddAsync(CreateDoctorInput doctor);
        Task<Result> UpdateAsync(int doctorId, UpdateDoctorInput doctor);
        Task<Result> DeleteAsync(int id);
    }
}
