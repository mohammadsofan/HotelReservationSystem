using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class RemoveFacilityFromRoomHandler : IRequestHandler<RemoveFacilityFromRoomCommand, Unit>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IRoomFacilitiesRepository _roomFacilitiesRepository;
        private readonly ILogger<RemoveFacilityFromRoomHandler> _logger;
        public RemoveFacilityFromRoomHandler(IRoomRepository roomRepository,
            IFacilityRepository facilityRepository,
            IRoomFacilitiesRepository roomFacilitiesRepository,
            ILogger<RemoveFacilityFromRoomHandler> logger)
        {
            _roomRepository = roomRepository;
            _facilityRepository = facilityRepository;
            _roomFacilitiesRepository = roomFacilitiesRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(RemoveFacilityFromRoomCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Removing facility {FacilityId} from room {RoomId}", request.FacilityId, request.RoomId);
            var roomId = request.RoomId;
            var facilityId = request.FacilityId;
            var roomFacility = await _roomFacilitiesRepository.GetOneByFilterAsync(rf=>rf.FacilityId==facilityId&&rf.RoomId == roomId);
            if(roomFacility == null)
            {
                _logger.LogWarning("Facility with ID {FacilityId} is not associated with Room ID {RoomId} for removal.", facilityId, roomId);
                throw new NotFoundException($"Facility with ID {facilityId} is not associated with Room ID {roomId}.");
            }
            await _roomFacilitiesRepository.HardDeleteAsync(roomFacility.Id, cancellationToken);
            _logger.LogInformation("Facility {FacilityId} removed from room {RoomId} successfully.", facilityId, roomId);
            return Unit.Value;
        }
    }
}
