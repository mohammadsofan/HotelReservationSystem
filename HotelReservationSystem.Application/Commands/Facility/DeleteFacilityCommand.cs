using MediatR;

namespace HotelReservationSystem.Application.Commands.Facility
{
    public class DeleteFacilityCommand:IRequest<Unit>
    {
        public long FacilityId { get; }

        public DeleteFacilityCommand(long facilityId)
        {
            FacilityId = facilityId;
        }
    }
}
