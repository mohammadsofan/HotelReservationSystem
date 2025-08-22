using HotelReservationSystem.Application.Commands.Booking;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Booking
{
    public class DeleteBookingHandler : IRequestHandler<DeleteBookingCommand, Unit>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<DeleteBookingHandler> _logger;

        public DeleteBookingHandler(IBookingRepository bookingRepository, ILogger<DeleteBookingHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete booking with ID {BookingId}", request.BookingId);
            var bookingId = request.BookingId;
            var deleted = await _bookingRepository.DeleteAsync(bookingId, cancellationToken);
            if (!deleted)
            {
                _logger.LogWarning("Booking with ID {BookingId} not found for deletion.", bookingId);
                throw new NotFoundException($"Booking with ID {bookingId} not found.");
            }
            _logger.LogInformation("Booking with ID {BookingId} deleted successfully.", bookingId);
            return Unit.Value;
        }
    }
}
