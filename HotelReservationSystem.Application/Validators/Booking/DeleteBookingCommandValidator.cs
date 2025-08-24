using FluentValidation;
using HotelReservationSystem.Application.Commands.Booking;

namespace HotelReservationSystem.Application.Validators.Booking
{
    public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidator()
        {
            RuleFor(c => c.BookingId)
                .NotEmpty().WithMessage("Booking ID is required.")
                .GreaterThan(0).WithMessage("Booking ID must be greater than zero.");
        }
    }
}
