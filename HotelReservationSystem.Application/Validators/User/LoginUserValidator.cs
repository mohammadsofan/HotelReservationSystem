using FluentValidation;
using HotelReservationSystem.Application.Dtos.User.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Validators.User
{
    public class LoginUserValidator:AbstractValidator<LoginUserRequestDto>
    {
        public LoginUserValidator() {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
