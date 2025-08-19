using FluentValidation;
using HotelReservationSystem.Application.Commands.User;

namespace HotelReservationSystem.Application.Validators.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(u => u.RequestDto.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
            RuleFor(u => u.RequestDto.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
