using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class AssignFacilityToRoomHandler : IRequestHandler<AssignFacilityToRoomCommand, Unit>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IRoomFacilitiesRepository _roomFacilitiesRepository;
        private readonly ILogger<AssignFacilityToRoomHandler> _logger;

        public AssignFacilityToRoomHandler(IRoomRepository roomRepository,
            IFacilityRepository facilityRepository,
            IRoomFacilitiesRepository roomFacilitiesRepository,
            ILogger<AssignFacilityToRoomHandler> logger)
        {
            _roomRepository = roomRepository;
            _facilityRepository = facilityRepository;
            _roomFacilitiesRepository = roomFacilitiesRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(AssignFacilityToRoomCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning facility {FacilityId} to room {RoomId}", request.FacilityId, request.RoomId);
            var roomId = request.RoomId;
            var facilityId = request.FacilityId;
            var room = await _roomRepository.GetOneByFilterAsync(r => r.Id == roomId);
            if(room == null)
            {
                _logger.LogWarning("Room with ID {RoomId} not found for facility assignment.", roomId);
                throw new NotFoundException($"Room with ID {roomId} not found");
            }
            var facility = await _facilityRepository.GetOneByFilterAsync(f => f.Id == facilityId);
            if(facility == null)
            {
                _logger.LogWarning("Facility with ID {FacilityId} not found for assignment.", facilityId);
                throw new NotFoundException($"Facility with ID {facilityId} not found");
            }
            var existingRoomFacility = await _roomFacilitiesRepository.GetOneByFilterAsync(rf => rf.RoomId == roomId && rf.FacilityId == facilityId);
            if(existingRoomFacility != null)
            {
                _logger.LogWarning("Facility with ID {FacilityId} is already assigned to Room with ID {RoomId}", facilityId, roomId);
                throw new ConflictException($"Facility with ID {facilityId} is already assigned to Room with ID {roomId}");
            }
            var roomFacility = new RoomFacilities()
            {
                FacilityId = facilityId,
                RoomId = roomId
            };
            await _roomFacilitiesRepository.CreateAsync(roomFacility, cancellationToken);
            _logger.LogInformation("Facility {FacilityId} assigned to room {RoomId} successfully.", facilityId, roomId);
            return Unit.Value;
        }
    }
}
