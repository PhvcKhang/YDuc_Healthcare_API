
using FluentValidation;

namespace HealthCareApplication.Identity.Resources
{
    public class CredentialValidator:AbstractValidator<Credential>
    {
        public CredentialValidator()
        {
            RuleFor(vm => vm.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(vm => vm.Password).Length(6, 12).WithMessage("Password must be between 6 and 12 characters");
        }
    }
}
