using BCrypt;
using Fiap.Health.Med.User.Manager.Domain.Enum;
using Fiap.Health.Med.User.Manager.Application.Common;
using Fiap.Health.Med.User.Manager.Application.DTOs.Auth.UserSearch;
using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.CreatePatient;
using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.GetPatient;
using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.UpdatePatient;
using Fiap.Health.Med.User.Manager.Application.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Patient;
using FluentValidation;
using System.Net;

namespace Fiap.Health.Med.User.Manager.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreatePatientInput> _createPatientInputValidator;
        private readonly IValidator<UpdatePatientInput> _updatePatientInputValidator;

        public PatientService(
            IUnitOfWork unitOfWork,
            IValidator<CreatePatientInput> createPatientInputValidator,
            IValidator<UpdatePatientInput> updatePatientInputValidator)
        {
            this._unitOfWork = unitOfWork;
            _createPatientInputValidator = createPatientInputValidator;
            _updatePatientInputValidator = updatePatientInputValidator;
        }

        public async Task<Result<List<GetPatientOutput>>> GetAllAsync()
        {
            (var retorno, var errorMessage) = await this._unitOfWork.PatientRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(errorMessage))
                return Result<List<GetPatientOutput>>.Fail($"Erro ao buscar pacientes: '{errorMessage}'");

            var responseDto = retorno.Select(patient => new GetPatientOutput
            {
                Id = patient.Id,
                Document = patient.Document,
                Name = patient.Name,
                Email = patient.Email
            });

            return Result<List<GetPatientOutput>>.Ok(responseDto.ToList());
        }

        public async Task<Result<GetPatientOutput>> GetByIdAsync(int id)
        {
            (var patient, var errorMessage) = await this._unitOfWork.PatientRepository.GetByIdAsync(id);

            if (patient == null)
                return Result<GetPatientOutput>.Fail("Paciente não encontrado.");

            if (!string.IsNullOrWhiteSpace(errorMessage))
                return Result<GetPatientOutput>.Fail($"Erro ao buscar paciente: '{errorMessage}'");

            var responseDto = new GetPatientOutput
            {
                Id = patient.Id,
                Document = patient.Document,
                Name = patient.Name,
                Email = patient.Email,
                HashedPassword = patient.HashedPassword
            };

            return Result<GetPatientOutput>.Ok(responseDto);
        }

        public async Task<Result<UserSearchResponseDto>> GetByDocumentAsync(long document)
        {
            (var patient, var errorMessage) = await this._unitOfWork.PatientRepository.GetByDocumentAsync(document);

            if (patient is null)
                return Result<UserSearchResponseDto>.Fail($"Paciente não encontrado: {errorMessage}");

            var responseDto = new UserSearchResponseDto
            {
                Username = $"{patient.Document}",
                HashPassword = patient.HashedPassword,
                UserType = EUserType.Patient
            };

            return Result<UserSearchResponseDto>.Ok(responseDto);
        }

        public async Task<Result> AddAsync(CreatePatientInput createPatientInput)
        {
            var validationResult = await _createPatientInputValidator.ValidateAsync(createPatientInput);

            if (!validationResult.IsValid)
                return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            (bool success, string errorMessage) = await this._unitOfWork.PatientRepository.AddAsync(new Patient
            {
                Document = createPatientInput.Document,
                Name = createPatientInput.Name,
                HashedPassword = BCryptHelper.HashPassword(createPatientInput.Password, BCryptHelper.GenerateSalt()),
                Email = createPatientInput.Email
            });

            return success
                ? Result.Ok()
                : Result.Fail($"Erro ao criar paciente: '{errorMessage}'");
        }

        public async Task<Result> UpdateAsync(int patientId, UpdatePatientInput updatePatientInput)
        {
            var validationResult = _updatePatientInputValidator.Validate(updatePatientInput);

            if (!validationResult.IsValid)
                return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            (var existingPatient, var getByIdErrorMessage) = await this._unitOfWork.PatientRepository.GetByIdAsync(patientId);

            if (existingPatient == null)
                return Result.Fail("Paciente não encontrado.");

            if (!string.IsNullOrWhiteSpace(getByIdErrorMessage))
                return Result.Fail($"Erro ao atualizar paciente: '{getByIdErrorMessage}'");

            (var succes, string? errorMessage) = await this._unitOfWork.PatientRepository.UpdateAsync(new Patient
            {
                Id = patientId,
                Document = updatePatientInput.Document ?? existingPatient.Document,
                Name = updatePatientInput.Name ?? existingPatient.Name,
                HashedPassword = updatePatientInput.HashedPassword ?? existingPatient.HashedPassword,
                Email = updatePatientInput.Email ?? existingPatient.Email
            });

            if (!succes)
                return Result.Fail($"Erro ao atualizar paciente: '{errorMessage}'");

            return Result.Ok();
        }

        public async Task<Common.V2.Result> DeleteAsync(int id)
        {
            (var patient, var errorMessage) = await this._unitOfWork.PatientRepository.GetByIdAsync(id);

            if (!string.IsNullOrWhiteSpace(errorMessage))
                return Common.V2.Result.Fail(HttpStatusCode.BadRequest, $"Erro ao excluir paciente: '{errorMessage}'");

            if (patient == null)
                return Common.V2.Result.Fail(HttpStatusCode.NotFound, "Paciente não encontrado.");

            await this._unitOfWork.PatientRepository.DeleteAsync(id);

            return Common.V2.Result.Success(HttpStatusCode.NoContent);
        }
    }
}
