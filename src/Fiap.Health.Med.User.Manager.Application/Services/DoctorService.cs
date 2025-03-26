using Fiap.Health.Med.User.Manager.Application.DTOs;
using Fiap.Health.Med.User.Manager.Application.Interfaces;
using Fiap.Health.Med.User.Manager.Application.Validators;
using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using FluentValidation;

namespace Fiap.Health.Med.User.Manager.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Doctor> _validator;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _validator = new DoctorValidator();
        }

        public async Task<Result<List<DoctorResponseDto>>> GetAllAsync()
        {
            try
            {
                var retorno = await this._unitOfWork.DoctorRepository.GetAllAsync();

                var responseDto = retorno.Select(m => new DoctorResponseDto
                {
                    Id = m.Id,
                    CrmNumber = m.CrmNumber,
                    CrmUf = m.CrmUf,
                    Name = m.Name,
                    Email = m.Email,
                    MedicalSpecialty = m.MedicalSpecialty
                });

                return Result<List<DoctorResponseDto>>.Ok(responseDto.ToList());
            }
            catch (Exception)
            {
                return Result<List<DoctorResponseDto>>.Fail("Erro ao buscar médicos.");
            }
        }

        public async Task<Result<DoctorResponseDto>> GetByIdAsync(int id) {

            try
            {
                var doctor = await this._unitOfWork.DoctorRepository.GetByIdAsync(id);

                if (doctor == null)
                    return Result<DoctorResponseDto>.Fail("Médico não encontrado.");

                var responseDto = new DoctorResponseDto
                {
                    Id = doctor.Id,
                    CrmNumber = doctor.CrmNumber,
                    CrmUf = doctor.CrmUf,
                    Name = doctor.Name,
                    Email = doctor.Email,
                    MedicalSpecialty = doctor.MedicalSpecialty
                };

                return Result<DoctorResponseDto>.Ok(responseDto);
            }
            catch (Exception)
            {
                return Result<DoctorResponseDto>.Fail("Erro ao buscar médico.");
            }

        }

        public async Task<Result<int>> AddAsync(Doctor doctor)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(doctor);

                if (!validationResult.IsValid)
                    return Result<int>.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

                int doctorId = await this._unitOfWork.DoctorRepository.AddAsync(doctor);

                return Result<int>.Ok(doctorId);
            }
            catch (Exception)
            {
                return Result<int>.Fail("Erro ao criar médico.");
            }
        }

        public async Task<Result> UpdateAsync(Doctor doctor)
        {

            try
            {
                var validationResult = _validator.Validate(doctor);

                if (!validationResult.IsValid)
                    return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

                var existingDoctor = await this._unitOfWork.DoctorRepository.GetByIdAsync(doctor.Id);

                if (existingDoctor == null)
                    return Result.Fail("Médico não encontrado.");

                await this._unitOfWork.DoctorRepository.UpdateAsync(doctor);
            }

            catch (Exception)
            {
                return Result.Fail("Erro ao atualizar médico.");
            }
               
            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                var doctor = await this._unitOfWork.DoctorRepository.GetByIdAsync(id);

                if (doctor == null)
                    return Result.Fail("Médico não encontrado.");

                await this._unitOfWork.DoctorRepository.DeleteAsync(id);

                return Result.Ok();
            }
            catch (Exception)
            {
                return Result.Fail("Erro ao excluir médico.");
            }
        }
    }
}
