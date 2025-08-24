using HotelReservationSystem.Application.Dtos.Booking.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Commands.Booking
{
    public class UpdateBookingCommand:IRequest<Unit>
    {
        public long BookingId { get; } 
        public BookingRequestDto RequestDto { get; }
        public UpdateBookingCommand(long bookingId,BookingRequestDto requestDto)
        {
            BookingId = bookingId;
            RequestDto = requestDto;
        }
    }
}
