using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
