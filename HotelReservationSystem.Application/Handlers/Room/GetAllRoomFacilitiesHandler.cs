using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Room;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class GetAllRoomFacilitiesHandler : IRequestHandler<GetAllRoomFacilitiesQuery, ICollection<FacilityResponseDto>>
    {
        private readonly IRoomFacilitiesRepository _roomFacilitiesRepository;
        private readonly ILogger<GetAllRoomFacilitiesHandler> _logger;

        public GetAllRoomFacilitiesHandler(IRoomFacilitiesRepository roomFacilitiesRepository, ILogger<GetAllRoomFacilitiesHandler> logger) {
            _roomFacilitiesRepository = roomFacilitiesRepository;
            _logger = logger;
        }
        public async Task<ICollection<FacilityResponseDto>> Handle(GetAllRoomFacilitiesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving facilities for room {RoomId}", request.RoomId);
            var roomId = request.RoomId;
            var roomfacilites = await _roomFacilitiesRepository.GetAllByFilterAsync(rf=>rf.RoomId ==roomId,rf=>rf.Facility!);
            var facilities = roomfacilites.Select(rf => rf.Facility).ToList();
            _logger.LogInformation("Retrieved {Count} facilities for room {RoomId}.", facilities.Count, roomId);
            return facilities.Adapt< ICollection<FacilityResponseDto>>().ToList();
        }
    }
}
