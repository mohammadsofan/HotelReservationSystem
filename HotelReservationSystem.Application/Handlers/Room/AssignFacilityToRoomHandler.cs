using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class AssignFacilityToRoomHandler : IRequestHandler<AssignFacilityToRoomCommand, Unit>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IRoomFacilitiesRepository _roomFacilitiesRepository;

        public AssignFacilityToRoomHandler(IRoomRepository roomRepository,
            IFacilityRepository facilityRepository,
            IRoomFacilitiesRepository roomFacilitiesRepository)
        {
            _roomRepository = roomRepository;
            _facilityRepository = facilityRepository;
            _roomFacilitiesRepository = roomFacilitiesRepository;
        }

        public async Task<Unit> Handle(AssignFacilityToRoomCommand request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomId;
            var facilityId = request.FacilityId;
            var room = await _roomRepository.GetOneByFilterAsync(r => r.Id == roomId);
            if(room == null)
            {
                throw new NotFoundException($"Room with ID {roomId} not found");
            }
            var facility = await _facilityRepository.GetOneByFilterAsync(f => f.Id == facilityId);
            if(facility == null)
            {
                throw new NotFoundException($"Facility with ID {facilityId} not found");
            }
            var existingRoomFacility = await _roomFacilitiesRepository.GetOneByFilterAsync(rf => rf.RoomId == roomId && rf.FacilityId == facilityId);
            if(existingRoomFacility != null)
            {
                throw new ConflictException($"Facility with ID {facilityId} is already assigned to Room with ID {roomId}");
            }
            var roomFacility = new RoomFacilities()
            {
                FacilityId = facilityId,
                RoomId = roomId
            };
            await _roomFacilitiesRepository.CreateAsync(roomFacility, cancellationToken);
            return Unit.Value;
        }
    }
}
