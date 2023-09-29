
using FluentValidation;

namespace HealthCareApplication.Identity.Resources
{
    public class CredentialsResourceValidator:AbstractValidator<CredentialsResource>
    {
        public CredentialsResourceValidator()
        {
            RuleFor(vm => vm.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(vm => vm.Password).Length(6, 12).WithMessage("Password must be between 6 and 12 characters");
        }
    }
}
