using FluentValidation;
using HotelReservationSystem.Application.Commands.User;

namespace HotelReservationSystem.Application.Validators.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage("User Id is required.")
                .GreaterThan(0).WithMessage("User Id must be greater than zero");
            RuleFor(c=>c.RequestDto).SetValidator(new UserDtoValidator());
        }
    }
}
