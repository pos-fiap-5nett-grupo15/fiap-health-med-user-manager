using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.CreatePatient;
using FluentValidation;

namespace Fiap.Health.Med.User.Manager.Application.Validators.Patient.CreatePatient
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientInput>
    {
        public CreatePatientValidator()
        {
            RuleFor(x => x.Document)
                .GreaterThan(0)
                .WithMessage("Documento do paciente deve ser um número positivo.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.HashedPassword)
                .NotEmpty().WithMessage("A senha é obrigatória.");
        }
    }
}
