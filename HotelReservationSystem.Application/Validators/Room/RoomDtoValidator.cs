using FluentValidation;
using HotelReservationSystem.Application.Dtos.Room.Requests;
using HotelReservationSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Validators.Room
{
    public class RoomDtoValidator:AbstractValidator<CreateRoomRequestDto>
    {
        public RoomDtoValidator() {
            RuleFor(r => r.Type).NotEmpty().WithMessage("Room type required.");
            RuleFor(r => r.PricePerNight).NotEmpty().WithMessage("Price required.")
                .GreaterThanOrEqualTo(0).WithMessage("Price can't be negative.");
            RuleFor(r => r.MaxOccupancy).NotEmpty().WithMessage("Max Occupancy required.")
                .GreaterThanOrEqualTo(1).WithMessage("Max Occupancy can't be less than 1.");
        }
    }
}
