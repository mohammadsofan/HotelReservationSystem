using HotelReservationSystem.Application.Dtos.Booking.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Booking;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Booking
{
    public class GetBookingsByFilterHandler : IRequestHandler<GetBookingsByFilterQuery, ICollection<BookingResponseDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<GetBookingsByFilterHandler> _logger;

        public GetBookingsByFilterHandler(IBookingRepository bookingRepository, ILogger<GetBookingsByFilterHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        public async Task<ICollection<BookingResponseDto>> Handle(GetBookingsByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving bookings with provided filter.");
            var bookings = await _bookingRepository.GetAllByFilterAsync(request.Filter);
            _logger.LogInformation("Retrieved {Count} bookings.", bookings.Count());
            return bookings.Select(booking => booking.Adapt<BookingResponseDto>()).ToList();
        }
    }
}
