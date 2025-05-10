using Fiap.Health.Med.User.Manager.Domain.Enum;
using Fiap.Health.Med.User.Manager.Application.Common;
using Fiap.Health.Med.User.Manager.Application.DTOs.Auth.UserSearch;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.CreateDoctor;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.GetById;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.GetDoctorsByFilters;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.UpdateDoctor;

namespace Fiap.Health.Med.User.Manager.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<Common.V2.Result<GetDoctorsByFiltersOutput>> GetByFilterAsync(string? doctorName, EMedicalSpecialty? doctorSpecialty, int? doctorDoncilNumber, string? doctorCrmUf, int currentPage, int pageSize, CancellationToken ct);
        Task<Result<GetByIdOutput>> GetByIdAsync(int id, CancellationToken ct);
        Task<Result<UserSearchResponseDto>> GetByConcilAsync(string concilUf, int concilNumber, CancellationToken ct);
        Task<Result<int>> AddAsync(CreateDoctorInput doctor, CancellationToken ct);
        Task<Result> UpdateAsync(int doctorId, UpdateDoctorInput doctor, CancellationToken ct);
        Task<Result> DeleteAsync(int id, CancellationToken ct);
    }
}
