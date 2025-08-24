using MediatR;

namespace HotelReservationSystem.Application.Commands.Room
{
    public class AssignFacilityToRoomCommand:IRequest<Unit>
    {
        public long RoomId { get; }
        public long FacilityId { get; }
        public AssignFacilityToRoomCommand(long roomId, long facilityId)
        {
            RoomId = roomId;
            FacilityId = facilityId;
        }
    }
}
