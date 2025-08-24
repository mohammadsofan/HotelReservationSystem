using FluentValidation;
using HotelReservationSystem.Application.Commands.Booking;

namespace HotelReservationSystem.Application.Validators.Booking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(c => c.RequestDto).SetValidator(new BookingDtoValidator());
        }
    }
}
