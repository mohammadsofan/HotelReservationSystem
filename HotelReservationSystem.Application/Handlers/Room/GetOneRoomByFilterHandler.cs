using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Room;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Room
{
    public class GetOneRoomByFilterHandler : IRequestHandler<GetOneRoomByFilterQuery, RoomResponseDto>
    {
        private readonly IRoomRepository _roomRepository;

        public GetOneRoomByFilterHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<RoomResponseDto> Handle(GetOneRoomByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var room = await _roomRepository.GetOneByFilterAsync(filter);
            if (room == null) {
                throw new NotFoundException($"Room not found.");
            }
            return room.Adapt<RoomResponseDto>();
        }
    }
}
