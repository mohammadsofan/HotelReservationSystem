using HotelReservationSystem.Application.Dtos.Booking.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Booking;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Booking
{
    public class GetOneBookingByFilterHandler : IRequestHandler<GetOneBookingByFilterQuery, BookingResponseDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<GetOneBookingByFilterHandler> _logger;

        public GetOneBookingByFilterHandler(IBookingRepository bookingRepository, ILogger<GetOneBookingByFilterHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        public async Task<BookingResponseDto> Handle(GetOneBookingByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving single booking with provided filter.");
            var filter = request.Filter;
            var booking = await _bookingRepository.GetOneByFilterAsync(filter);
            if (booking == null)
            {
                _logger.LogWarning("Booking not found for provided filter.");
                throw new NotFoundException("Booking not found.");
            }
            _logger.LogInformation("Booking found with ID {BookingId}.", booking.Id);
            return booking.Adapt<BookingResponseDto>();
        }
    }
}
