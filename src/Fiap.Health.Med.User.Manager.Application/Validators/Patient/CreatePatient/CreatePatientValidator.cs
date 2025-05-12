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

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
                .Matches(@"[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
                .Matches(@"[0-9]").WithMessage("A senha deve conter ao menos um número.")
                .Matches("[!@#$%^&*(),.?\":{ }|<>]").WithMessage("A senha deve conter ao menos um caractere especial.");
        }
    }
}
