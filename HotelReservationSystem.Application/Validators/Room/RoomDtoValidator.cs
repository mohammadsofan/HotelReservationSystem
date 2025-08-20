using FluentValidation;
using HotelReservationSystem.Application.Dtos.Room.Requests;
namespace HotelReservationSystem.Application.Validators.Room
{
    public class RoomDtoValidator:AbstractValidator<RoomRequestDto>
    {
        public RoomDtoValidator() {
            RuleFor(r => r.Type).NotEmpty().WithMessage("Room type is required.");
            RuleFor(r => r.FloorNumber).NotEmpty().WithMessage("FloorNumber is required");
            RuleFor(r=>r.RoomNumber).NotEmpty().WithMessage("RoomNumber is required")
                .Length(1,50).WithMessage("RoomNumber length must be between 1 and 50 characters.");
            RuleFor(r => r.PricePerNight).NotEmpty().WithMessage("Price is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Price can't be negative.");
            RuleFor(r => r.MaxOccupancy).NotEmpty().WithMessage("Max Occupancy is required.")
                .GreaterThanOrEqualTo(1).WithMessage("Max Occupancy can't be less than 1.");
        }
    }
}
