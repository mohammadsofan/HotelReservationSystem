using HotelReservationSystem.Application.Dtos.Booking.Responses;
using MediatR;
using System.Linq.Expressions;
namespace HotelReservationSystem.Application.Queries.Booking
{
    public class GetOneBookingByFilterQuery:IRequest<BookingResponseDto>
    {
        public Expression<Func<Domain.Entities.Booking, bool>> Filter { get; }
        public GetOneBookingByFilterQuery(Expression<Func<Domain.Entities.Booking, bool>> filter)
        {
            Filter = filter;
        }
    }
}
