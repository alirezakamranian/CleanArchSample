using FluentValidation;

namespace Presentation.User.SignInUser
{
    public class SignInUserRequestValidator : AbstractValidator<SignInUserRequest>
    {
        public SignInUserRequestValidator()
        {
            RuleFor(s => s.UserName).NotNull()
                .NotEmpty().MinimumLength(3);

            RuleFor(s => s.Password).NotNull().NotEmpty()
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }
    }
}
