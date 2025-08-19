using FluentValidation;
using HotelReservationSystem.Application.Dtos.User.Requests;
namespace HotelReservationSystem.Application.Validators.User
{
    public class LoginUserDtoValidator:AbstractValidator<LoginUserRequestDto>
    {
        public LoginUserDtoValidator() {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
