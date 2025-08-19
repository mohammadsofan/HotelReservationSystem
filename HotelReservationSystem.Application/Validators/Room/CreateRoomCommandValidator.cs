using FluentValidation;
using HotelReservationSystem.Application.Commands.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Validators.Room
{
    public class CreateRoomCommandValidator:AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator() {
            RuleFor(c => c.RequestDto).SetValidator(new RoomDtoValidator());
        }
    }
}
