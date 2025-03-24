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

        public async Task<IEnumerable<DoctorResponseDto>> GetAllAsync()
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

            return responseDto;
        }

        public async Task<DoctorResponseDto> GetByIdAsync(int id) {

            var retorno = await this._unitOfWork.DoctorRepository.GetByIdAsync(id);

            var responseDto = new DoctorResponseDto
            {
                Id = retorno.Id,
                CrmNumber = retorno.CrmNumber,
                CrmUf = retorno.CrmUf,
                Name = retorno.Name,
                Email = retorno.Email,
                MedicalSpecialty = retorno.MedicalSpecialty
            };

            return responseDto;
        }

        public async Task<int> AddAsync(Doctor doctor)
        {
            var validationResult = _validator.Validate(doctor);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return await this._unitOfWork.DoctorRepository.AddAsync(doctor);
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            var validationResult = _validator.Validate(doctor);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await this._unitOfWork.DoctorRepository.UpdateAsync(doctor);
        }

        public async Task DeleteAsync(int id) => await this._unitOfWork.DoctorRepository.DeleteAsync(id);
    }
}
