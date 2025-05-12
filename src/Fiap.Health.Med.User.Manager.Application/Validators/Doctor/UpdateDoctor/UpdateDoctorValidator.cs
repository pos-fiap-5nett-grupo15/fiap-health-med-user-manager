using Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.UpdateDoctor;
using FluentValidation;

namespace Fiap.Health.Med.User.Manager.Application.Validators.Doctor.UpdateDoctor
{
    public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorInput>
    {
        public UpdateDoctorValidator()
        {
            When(x => x.CrmNumber is not null, () =>
            {
                RuleFor(m => m.CrmNumber)
                    .GreaterThan(0).WithMessage("CRM deve ser um número positivo.");
            });

            When(x => x.CrmUf is not null, () =>
            {
                RuleFor(m => m.CrmUf)
                    .NotEmpty().WithMessage("UF do CRM é obrigatória.")
                    .Length(2).WithMessage("UF deve ter 2 caracteres.");
            });

            When(x => x.Name is not null, () =>
            {
                RuleFor(m => m.Name)
                    .NotEmpty().WithMessage("Nome é obrigatório.")
                    .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres.");
            });

            When(x => x.Email is not null, () =>
            {
                RuleFor(m => m.Email)
                    .NotEmpty().WithMessage("Email é obrigatório.")
                    .EmailAddress().WithMessage("Email inválido.");
            });

            When(x => x.MedicalSpecialty is not null, () =>
            {
                RuleFor(m => m.MedicalSpecialty)
                    .IsInEnum().WithMessage("Especialidade inválida.");
            });

            When(x => x.Password is not null, () =>
            {
                RuleFor(m => m.Password)
                    .NotEmpty().WithMessage("A senha é obrigatória.");
            });
        }
    }
}
