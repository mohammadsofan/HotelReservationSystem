using FluentValidation;
using FluentValidation.Results;
using HotelReservationSystem.Application.Dtos.User.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Validators.User
{
    public class UserDtoValidator:AbstractValidator<CreateUserRequestDto>
    {
        public UserDtoValidator() {
            RuleFor(u => u.IdCard).NotEmpty().WithMessage("User id card is required");
            RuleFor(u => u.Username).NotEmpty().WithMessage("UserName is required")
                .Length(3, 100).WithMessage("UserName length must be withen 3 to 100 character");
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("FirstName is required")
                .Length(3, 100).WithMessage("FirstName length must be withen 3 to 100 character");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("LastName is required")
                .Length(3, 100).WithMessage("LastName length must be withen 3 to 100 character");
            RuleFor(u=>u.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required")
                .Length(6, 100).WithMessage("Password length must be withen 6 to 100 character");
            RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");         
        }
    }
}
