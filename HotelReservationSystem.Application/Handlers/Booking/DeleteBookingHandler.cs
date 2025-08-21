using HotelReservationSystem.Application.Commands.Booking;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Booking
{
    public class DeleteBookingHandler : IRequestHandler<DeleteBookingCommand, Unit>
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var bookingId = request.BookingId;
            var deleted = await _bookingRepository.DeleteAsync(bookingId, cancellationToken);
            if (!deleted)
            {
                throw new NotFoundException($"Booking with ID {bookingId} not found.");
            }
            return Unit.Value;
        }
    }
}
