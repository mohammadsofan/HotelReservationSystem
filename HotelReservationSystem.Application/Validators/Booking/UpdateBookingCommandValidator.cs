using FluentValidation;
using HotelReservationSystem.Application.Commands.Booking;
using HotelReservationSystem.Application.Dtos.Booking.Requests;

namespace HotelReservationSystem.Application.Validators.Booking
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
    public UpdateBookingCommandValidator()
        {
            RuleFor(c=>c.BookingId)
                .NotEmpty().WithMessage("Booking ID is required.")
                .GreaterThan(0).WithMessage("Booking ID must be greater than zero.");
            RuleFor(c=>c.RequestDto).SetValidator(new BookingDtoValidator());
        }
    }
}
