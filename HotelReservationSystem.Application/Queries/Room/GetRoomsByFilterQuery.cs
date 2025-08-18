using HotelReservationSystem.Application.Dtos.Room.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
