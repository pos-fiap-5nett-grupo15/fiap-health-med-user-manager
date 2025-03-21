using Fiap.Health.Med.User.Manager.Application.DTOs;
using Fiap.Health.Med.User.Manager.Application.Validators;
using Fiap.Health.Med.User.Manager.Domain.Interfaces;
using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using FluentValidation;

namespace Fiap.Health.Med.User.Manager.Application.Services
{
    public class DoctorService
    {
        private readonly IDoctorRepository _repository;
        private readonly IValidator<Doctor> _validator;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
            _validator = new DoctorValidator();
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetAllAsync()
        {

            var retorno = await _repository.GetAllAsync();

            var response = retorno.Select(m => new DoctorResponseDto
            {
                Id = m.Id,
                CrmNumber = m.CrmNumber,
                CrmUf = m.CrmUf,
                Name = m.Name,
                Email = m.Email,
                MedicalSpecialty = m.MedicalSpecialty
            });

            return response;
        }

        public async Task<DoctorResponseDto> GetByIdAsync(int id) {

            var retorno = await _repository.GetByIdAsync(id);

            var response = new DoctorResponseDto
            {
                Id = retorno.Id,
                CrmNumber = retorno.CrmNumber,
                CrmUf = retorno.CrmUf,
                Name = retorno.Name,
                Email = retorno.Email,
                MedicalSpecialty = retorno.MedicalSpecialty
            };

            return response;
        }

        public async Task<int> AddAsync(Doctor doctor)
        {
            var validationResult = _validator.Validate(doctor);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return await _repository.AddAsync(doctor);
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            var validationResult = _validator.Validate(doctor);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _repository.UpdateAsync(doctor);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
