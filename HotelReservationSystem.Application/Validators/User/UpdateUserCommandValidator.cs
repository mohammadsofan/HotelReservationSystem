using FluentValidation;
using HotelReservationSystem.Application.Commands.User;
namespace HotelReservationSystem.Application.Validators.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.RequestDto.Username).NotEmpty().WithMessage("UserName is required")
                .Length(3, 100).WithMessage("UserName length must be withen 3 to 100 character");
            RuleFor(c => c.RequestDto.FirstName).NotEmpty().WithMessage("FirstName is required")
                .Length(3, 100).WithMessage("FirstName length must be withen 3 to 100 character");
            RuleFor(c => c.RequestDto.LastName).NotEmpty().WithMessage("LastName is required")
                .Length(3, 100).WithMessage("LastName length must be withen 3 to 100 character");
            RuleFor(c => c.RequestDto.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");

        }
    }
}
