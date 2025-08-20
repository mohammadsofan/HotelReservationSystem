using HotelReservationSystem.Application.Dtos.Room.Responses;
using MediatR;
using System.Linq.Expressions;
namespace HotelReservationSystem.Application.Queries.Room
{
    public class GetRoomsByFilterQuery:IRequest<ICollection<RoomResponseDto>>
    {
        public Expression<Func<Domain.Entities.Room, bool>>? Filter { get;}

        public GetRoomsByFilterQuery(Expression<Func<Domain.Entities.Room, bool>>? filter = null)
        {
            Filter = filter;
        }
    }
}
