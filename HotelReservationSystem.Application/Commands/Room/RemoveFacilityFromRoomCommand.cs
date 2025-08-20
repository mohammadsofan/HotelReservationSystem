using MediatR;

namespace HotelReservationSystem.Application.Commands.Room
{
    public class RemoveFacilityFromRoomCommand : IRequest<Unit>
    {
        public long RoomId { get; }
        public long FacilityId { get; }
        public RemoveFacilityFromRoomCommand(long roomId, long facilityId)
        {
            RoomId = roomId;
            FacilityId = facilityId;
        }
    }
}
