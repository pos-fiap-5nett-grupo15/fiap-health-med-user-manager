using Fiap.Health.Med.User.Manager.Domain.Models.Doctor;
using FluentValidation;

namespace Fiap.Health.Med.User.Manager.Application.Validators
{
    public class DoctorValidator : AbstractValidator<Doctor>
    {
        public DoctorValidator()
        {
            RuleFor(m => m.CrmNumber)
                    .GreaterThan(0).WithMessage("CRM deve ser um número positivo.");

            RuleFor(m => m.CrmUf)
                    .NotEmpty().WithMessage("UF do CRM é obrigatória.")
                    .Length(2).WithMessage("UF deve ter 2 caracteres.");

            RuleFor(m => m.Name)
                    .NotEmpty().WithMessage("Nome é obrigatório.")
                    .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres.");

            RuleFor(m => m.Email)
                    .NotEmpty().WithMessage("Email é obrigatório.")
                    .EmailAddress().WithMessage("Email inválido.");

            RuleFor(m => m.MedicalSpecialty)
                    .IsInEnum().WithMessage("Especialidade inválida.");

            RuleFor(m => m.HashedPassword)
             .NotEmpty().WithMessage("A senha é obrigatória.")
             .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
             .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
             .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
             .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número.")
             .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caractere especial.");
        }

    }
}
