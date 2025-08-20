using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class RemoveFacilityFromRoomHandler : IRequestHandler<RemoveFacilityFromRoomCommand, Unit>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IRoomFacilitiesRepository _roomFacilitiesRepository;
        public RemoveFacilityFromRoomHandler(IRoomRepository roomRepository,
            IFacilityRepository facilityRepository,
            IRoomFacilitiesRepository roomFacilitiesRepository)
        {
            _roomRepository = roomRepository;
            _facilityRepository = facilityRepository;
            _roomFacilitiesRepository = roomFacilitiesRepository;
        }
        public async Task<Unit> Handle(RemoveFacilityFromRoomCommand request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomId;
            var facilityId = request.FacilityId;
            var roomFacility = await _roomFacilitiesRepository.GetOneByFilterAsync(rf=>rf.FacilityId==facilityId&&rf.RoomId == roomId);
            if(roomFacility == null)
            {
                throw new NotFoundException($"Facility with ID {facilityId} is not associated with Room ID {roomId}.");
            }
            await _roomFacilitiesRepository.HardDeleteAsync(roomFacility.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
