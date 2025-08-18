using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Room;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class GetRoomsByFilterHandler : IRequestHandler<GetRoomsByFilterQuery, ICollection<RoomResponseDto>>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomsByFilterHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<ICollection<RoomResponseDto>> Handle(GetRoomsByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var rooms = await _roomRepository.GetAllByFilterAsync(filter);
            return rooms.Select(r => r.Adapt<RoomResponseDto>()).ToList();
        }
    }
}
