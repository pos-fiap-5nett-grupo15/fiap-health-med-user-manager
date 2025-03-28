using Fiap.Health.Med.User.Manager.Application.DTOs.Patient.UpdatePatient;
using FluentValidation;

namespace Fiap.Health.Med.User.Manager.Application.Validators.Patient.UpdatePatient
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientInput>
    {
        public UpdatePatientValidator()
        {
            When(x => x.Document is not null, () =>
            {
                RuleFor(x => x.Document)
                    .GreaterThan(0)
                    .WithMessage("Documento do paciente deve ser um número positivo.");
            });

            When(x => x.Name is not null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Nome é obrigatório.")
                    .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres.");
            });

            When(x => x.Email is not null, () =>
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email é obrigatório.")
                    .EmailAddress().WithMessage("Email inválido.");
            });

            When(x => x.HashedPassword is not null, () =>
            {
                RuleFor(x => x.HashedPassword)
                    .NotEmpty().WithMessage("A senha é obrigatória.");
            });
        }
    }
}
