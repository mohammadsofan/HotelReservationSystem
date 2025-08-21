using HotelReservationSystem.Application.Dtos.Booking.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Booking;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Booking
{
    public class GetBookingsByFilterHandler : IRequestHandler<GetBookingsByFilterQuery, ICollection<BookingResponseDto>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingsByFilterHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<ICollection<BookingResponseDto>> Handle(GetBookingsByFilterQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetAllByFilterAsync(request.Filter);
            return bookings.Select(booking => booking.Adapt<BookingResponseDto>()).ToList();
        }
    }
}
