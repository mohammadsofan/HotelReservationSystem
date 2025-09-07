using HotelReservationSystem.Application.Dtos.Booking.Requests;
using HotelReservationSystem.Application.Dtos.Booking.Responses;
using MediatR;

namespace HotelReservationSystem.Application.Commands.Booking
{
    public class CreateBookingCommand:IRequest<BookingResponseDto>
    {
        public BookingRequestDto RequestDto { get; }
        public CreateBookingCommand(BookingRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
}
