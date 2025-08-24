using HotelReservationSystem.Application.Dtos.Room.Responses;
using MediatR;
using System.Linq.Expressions;
namespace HotelReservationSystem.Application.Queries.Room
{
    public class GetOneRoomByFilterQuery:IRequest<RoomResponseDto>
    {
        public Expression<Func<Domain.Entities.Room,bool>> Filter { get; }

        public GetOneRoomByFilterQuery(Expression<Func<Domain.Entities.Room, bool>> filter)
        {
            Filter = filter;
        }
    }
}
