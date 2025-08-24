using HotelReservationSystem.Application.Dtos.Booking.Responses;
using MediatR;
using System.Linq.Expressions;

namespace HotelReservationSystem.Application.Queries.Booking
{
    public class GetBookingsByFilterQuery:IRequest<ICollection<BookingResponseDto>>
    {
        public Expression<Func<Domain.Entities.Booking, bool>>? Filter { get; }
        public GetBookingsByFilterQuery(Expression<Func<Domain.Entities.Booking, bool>>? filter = null)
        {
            Filter = filter;
        }
    }
}
