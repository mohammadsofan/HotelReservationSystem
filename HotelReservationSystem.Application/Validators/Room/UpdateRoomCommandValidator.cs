using FluentValidation;
using HotelReservationSystem.Application.Commands.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Validators.Room
{
    public class UpdateRoomCommandValidator:AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(c=>c.RoomId).NotEmpty().WithMessage("Room Id is required.")
                .GreaterThan(0).WithMessage("Room Id must be greater than zero");
            RuleFor(c=>c.RequestDto).SetValidator(new RoomDtoValidator());
        }
    }
}
