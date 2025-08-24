using HotelReservationSystem.Application.Dtos.Facility.Requests;
using MediatR;

namespace HotelReservationSystem.Application.Commands.Facility
{
    public class UpdateFacilityCommand:IRequest<Unit>
    {
        public long FacilityId { get; }
        public FacilityRequestDto RequestDto { get; }
        public UpdateFacilityCommand(long facilityId, FacilityRequestDto requestDto)
        {
            FacilityId = facilityId;
            RequestDto = requestDto;
        }

    }
}
