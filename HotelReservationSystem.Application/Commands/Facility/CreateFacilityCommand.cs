using HotelReservationSystem.Application.Dtos.Facility.Requests;
using HotelReservationSystem.Application.Dtos.Facility.Responses;
using MediatR;

namespace HotelReservationSystem.Application.Commands.Facility
{
    public class CreateFacilityCommand:IRequest<FacilityResponseDto>
    {
        public FacilityRequestDto RequestDto { get; }

        public CreateFacilityCommand(FacilityRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
}
