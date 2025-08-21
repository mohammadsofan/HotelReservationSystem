using FluentValidation;
using HotelReservationSystem.Application.Dtos.Booking.Requests;
using MediatR;

namespace HotelReservationSystem.Application.Validators.Booking
{
    public class BookingDtoValidator : AbstractValidator<BookingRequestDto>
    {
        public BookingDtoValidator()
        {
            RuleFor(b => b.CheckIn)
                .NotEmpty().WithMessage("Check-in date is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Check-in date must be in the future.");
            RuleFor(b => b.CheckOut)
                .NotEmpty().WithMessage("Check-out date is required.")
                .GreaterThan(b => b.CheckIn).WithMessage("Check-out date must be after check-in date.");
            RuleFor(b => b.GuestsNumber)
                .GreaterThan(0).WithMessage("Guest count must be greater than zero.");
            RuleFor(b => b.RoomId)
                .NotEmpty().WithMessage("Room ID is required.")
                .GreaterThan(0).WithMessage("Room ID must be greater than zero.");
            RuleFor(b => b.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
        }
    }
}
