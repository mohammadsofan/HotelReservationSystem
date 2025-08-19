using FluentValidation;
using HotelReservationSystem.Application.Commands.User;

namespace HotelReservationSystem.Application.Validators.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(lu=>lu.RequestDto).SetValidator(new LoginUserDtoValidator());
        }
    }
}
