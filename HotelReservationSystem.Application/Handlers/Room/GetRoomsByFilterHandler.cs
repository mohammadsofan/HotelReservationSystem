using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Room;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class GetRoomsByFilterHandler : IRequestHandler<GetRoomsByFilterQuery, ICollection<RoomResponseDto>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<GetRoomsByFilterHandler> _logger;

        public GetRoomsByFilterHandler(IRoomRepository roomRepository, ILogger<GetRoomsByFilterHandler> logger)
        {
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public async Task<ICollection<RoomResponseDto>> Handle(GetRoomsByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving rooms with provided filter.");
            var filter = request.Filter;
            var rooms = await _roomRepository.GetAllByFilterAsync(filter);
            _logger.LogInformation("Retrieved {Count} rooms.", rooms.Count());
            return rooms.Select(r => r.Adapt<RoomResponseDto>()).ToList();
        }
    }
}
