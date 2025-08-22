using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Room;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class GetOneRoomByFilterHandler : IRequestHandler<GetOneRoomByFilterQuery, RoomResponseDto>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<GetOneRoomByFilterHandler> _logger;

        public GetOneRoomByFilterHandler(IRoomRepository roomRepository, ILogger<GetOneRoomByFilterHandler> logger)
        {
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public async Task<RoomResponseDto> Handle(GetOneRoomByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving single room with provided filter.");
            var filter = request.Filter;
            var room = await _roomRepository.GetOneByFilterAsync(filter);
            if (room == null) {
                _logger.LogWarning("Room not found for provided filter.");
                throw new NotFoundException($"Room not found.");
            }
            _logger.LogInformation("Room found with ID {RoomId}.", room.Id);
            return room.Adapt<RoomResponseDto>();
        }
    }
}
