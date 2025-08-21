using HotelReservationSystem.Application.Dtos.Booking.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Booking;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Booking
{
    public class GetOneBookingByFilterHandler : IRequestHandler<GetOneBookingByFilterQuery, BookingResponseDto>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetOneBookingByFilterHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingResponseDto> Handle(GetOneBookingByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var booking = await _bookingRepository.GetOneByFilterAsync(filter);
            if (booking == null)
            {
                throw new NotFoundException("Booking not found.");
            }
            return booking.Adapt<BookingResponseDto>();
        }
    }
}
