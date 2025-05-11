using BCrypt;
using Fiap.Health.Med.User.Manager.Application.Common;
using Fiap.Health.Med.User.Manager.Domain.Enum;
using Fiap.Health.Med.User.Manager.Application.DTOs.Auth.UserSearch;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.CreateDoctor;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.GetById;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.GetDoctorsByFilters;
using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.UpdateDoctor;
using Fiap.Health.Med.User.Manager.Application.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using FluentValidation;
using System.Net;

namespace Fiap.Health.Med.User.Manager.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateDoctorInput> _createDoctorInputValidator;
        private readonly IValidator<UpdateDoctorInput> _updateDoctorInputValidator;

        public DoctorService(
            IUnitOfWork unitOfWork,
            IValidator<CreateDoctorInput> createDoctorInputValidator,
            IValidator<UpdateDoctorInput> updateDoctorInputValidator)
        {
            this._unitOfWork = unitOfWork;
            _createDoctorInputValidator = createDoctorInputValidator;
            _updateDoctorInputValidator = updateDoctorInputValidator;
        }

        public async Task<Common.V2.Result<GetDoctorsByFiltersOutput>> GetByFilterAsync(
            string? doctorName,
            EMedicalSpecialty? doctorSpecialty,
            int? doctorDoncilNumber,
            string? doctorCrmUf,
            int currentPage,
            int pageSize,
            CancellationToken ct)
        {
            try
            {
                (var list, var total) = await this._unitOfWork.DoctorRepository.GetByFilterAsync(doctorName, doctorSpecialty, doctorDoncilNumber, doctorCrmUf, currentPage, pageSize, ct);

                return Common.V2.Result<GetDoctorsByFiltersOutput>.Success(HttpStatusCode.OK, new GetDoctorsByFiltersOutput
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    Total = total,
                    Doctors = list
                });
            }
            catch (Exception e)
            {
                return Common.V2.Result<GetDoctorsByFiltersOutput>.Fail(HttpStatusCode.InternalServerError, $"Erro ao buscar médicos: {e.Message}");
            }
        }

        public async Task<Result<GetByIdOutput>> GetByIdAsync(int id, CancellationToken ct)
        {
            try
            {
                var doctor = await this._unitOfWork.DoctorRepository.GetByIdAsync(id, ct);

                if (doctor == null)
                    return Result<GetByIdOutput>.Fail("Médico não encontrado.");

                var responseDto = new GetByIdOutput
                {
                    Id = doctor.Id,
                    CrmNumber = doctor.CrmNumber,
                    CrmUf = doctor.CrmUf,
                    Name = doctor.Name,
                    Email = doctor.Email,
                    MedicalSpecialty = doctor.MedicalSpecialty
                };

                return Result<GetByIdOutput>.Ok(responseDto);
            }
            catch (Exception e)
            {
                return Result<GetByIdOutput>.Fail($"Erro ao buscar médico: '{e.Message}'");
            }
        }

        public async Task<Result<UserSearchResponseDto>> GetByConcilAsync(string concilUf, int concilNumber, CancellationToken ct)
        {
            try
            {
                var doctor = await this._unitOfWork.DoctorRepository.GetByConcilAsync(concilUf, concilNumber, ct);
                if (doctor == null)
                    return Result<UserSearchResponseDto>.Fail("Médico não encontrado.");
                var responseDto = new UserSearchResponseDto
                {
                    Username = $"{doctor.CrmUf}{doctor.CrmNumber}",
                    HashPassword = doctor.HashedPassword,
                    UserType = EUserType.Doctor
                };
                return Result<UserSearchResponseDto>.Ok(responseDto);
            }
            catch (Exception e)
            {
                return Result<UserSearchResponseDto>.Fail($"Erro ao buscar médico: '{e.Message}'");
            }
        }

        public async Task<Result<int>> AddAsync(CreateDoctorInput createDoctorInput, CancellationToken ct)
        {
            try
            {
                var validationResult = await _createDoctorInputValidator.ValidateAsync(createDoctorInput);

                if (!validationResult.IsValid)
                    return Result<int>.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

                int doctorId = await this._unitOfWork.DoctorRepository.AddAsync(new Doctor
                {
                    CrmNumber = createDoctorInput.CrmNumber,
                    CrmUf = createDoctorInput.CrmUf,
                    Name = createDoctorInput.Name,
                    HashedPassword = BCryptHelper.HashPassword(createDoctorInput.Password, BCryptHelper.GenerateSalt()),
                    Email = createDoctorInput.Email,
                    MedicalSpecialty = createDoctorInput.MedicalSpecialty
                }, ct);

                return Result<int>.Ok(doctorId);
            }
            catch (Exception e)
            {
                return Result<int>.Fail($"Erro ao criar médico: '{e.Message}'");
            }
        }

        public async Task<Result> UpdateAsync(int doctorId, UpdateDoctorInput updateDoctorInput, CancellationToken ct)
        {
            try
            {
                var validationResult = _updateDoctorInputValidator.Validate(updateDoctorInput);

                if (!validationResult.IsValid)
                    return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

                var existingDoctor = await this._unitOfWork.DoctorRepository.GetByIdAsync(doctorId, ct);

                if (existingDoctor == null)
                    return Result.Fail("Médico não encontrado.");

                await this._unitOfWork.DoctorRepository.UpdateAsync(new Doctor
                {
                    Id = doctorId,
                    CrmNumber = updateDoctorInput.CrmNumber ?? existingDoctor.CrmNumber,
                    CrmUf = updateDoctorInput.CrmUf ?? existingDoctor.CrmUf,
                    Name = updateDoctorInput.Name ?? existingDoctor.Name,
                    HashedPassword = string.IsNullOrEmpty(updateDoctorInput.Password) ? existingDoctor.HashedPassword : BCryptHelper.HashPassword(updateDoctorInput.Password, BCryptHelper.GenerateSalt()),
                    Email = updateDoctorInput.Email ?? existingDoctor.Email,
                    MedicalSpecialty = updateDoctorInput.MedicalSpecialty ?? existingDoctor.MedicalSpecialty
                },
                ct);
            }
            catch (Exception e)
            {
                return Result.Fail($"Erro ao atualizar médico: '{e.Message}'");
            }
            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken ct)
        {
            try
            {
                var doctor = await this._unitOfWork.DoctorRepository.GetByIdAsync(id, ct);

                if (doctor == null)
                    return Result.Fail("Médico não encontrado.");

                await this._unitOfWork.DoctorRepository.DeleteAsync(id, ct);

                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail($"Erro ao excluir médico: '{e.Message}'");
            }
        }
    }
}
