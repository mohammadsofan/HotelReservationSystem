using FluentValidation;
using HotelReservationSystem.Application.Commands.User;

namespace HotelReservationSystem.Application.Validators.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.RequestDto).SetValidator(new UserDtoValidator());
        }
    }
}
