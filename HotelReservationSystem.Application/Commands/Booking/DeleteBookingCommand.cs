using MediatR;
namespace HotelReservationSystem.Application.Commands.Booking
{
    public class DeleteBookingCommand:IRequest<Unit>
    {
        public long BookingId { get; }

        public DeleteBookingCommand(long bookingId)
        {
            BookingId = bookingId;
        }
    }
}
