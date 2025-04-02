using Fiap.Health.Med.User.Manager.Application.DTOs.Auth.UserSearch;
using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.CreatePatient;
using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.GetPatient;
using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.UpdatePatient;

namespace Fiap.Health.Med.User.Manager.Application.Interfaces
{
    public interface IPatientService
    {
        Task<Result<List<GetPatientOutput>>> GetAllAsync();
        Task<Result<GetPatientOutput>> GetByIdAsync(int id);
        Task<Result<UserSearchResponseDto>> GetByDocumentAsync(long document);
        Task<Result> AddAsync(CreatePatientInput patient);
        Task<Result> UpdateAsync(int patientId, UpdatePatientInput patient);
        Task<Result> DeleteAsync(int patientId);
    }
}
