using DeliveryBlazor.UseCase.Features.ApplicationUsers.Commands;
using FluentValidation;

namespace DeliveryBlazor.UseCase.Features.ApplicationUsers.Validators
{
    public class CreateApplicationUserCommandValidator : AbstractValidator<CreateApplicationUserCommand>
    {
        public CreateApplicationUserCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters");
            RuleFor(x => x.Role).IsInEnum().WithMessage("Invalid role");
        }
    }
}
