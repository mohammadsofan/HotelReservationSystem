using FluentValidation;
using HotelReservationSystem.Application.Commands.User;

namespace HotelReservationSystem.Application.Validators.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.RequestDto.IdCard).NotEmpty().WithMessage("User id card is required");
            RuleFor(c => c.RequestDto.Username).NotEmpty().WithMessage("UserName is required")
                .Length(3, 100).WithMessage("UserName length must be withen 3 to 100 character");
            RuleFor(c => c.RequestDto.FirstName).NotEmpty().WithMessage("FirstName is required")
                .Length(3, 100).WithMessage("FirstName length must be withen 3 to 100 character");
            RuleFor(c => c.RequestDto.LastName).NotEmpty().WithMessage("LastName is required")
                .Length(3, 100).WithMessage("LastName length must be withen 3 to 100 character");
            RuleFor(c => c.RequestDto.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
            RuleFor(c => c.RequestDto.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
            RuleFor(c => c.RequestDto.Password).NotEmpty().WithMessage("Password is required")
                .Length(6, 100).WithMessage("Password length must be withen 6 to 100 character");
            RuleFor(c => c.RequestDto.ConfirmPassword)
                    .Equal(c => c.RequestDto.Password)
                    .WithMessage("Passwords do not match");
        }
    }
}
