using Fiap.Health.Med.User.Manager.Domain.Enum;

namespace Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.GetDoctorsByFilters
{
    public class GetDoctorsByFiltersOutput
    {
        public IEnumerable<Domain.Models.Doctor.Doctor> Doctors { get; init; } = [];
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int Total { get; init; }
    }
}
