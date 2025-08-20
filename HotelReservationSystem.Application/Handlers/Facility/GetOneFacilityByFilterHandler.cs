using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Facility;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Facility
{
    public class GetOneFacilityByFilterHandler : IRequestHandler<GetOneFacilityByFilterQuery, FacilityResponseDto>
    {
        private readonly IFacilityRepository _facilityRepository;

        public GetOneFacilityByFilterHandler(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        public async Task<FacilityResponseDto> Handle(GetOneFacilityByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var facility = await _facilityRepository.GetOneByFilterAsync(filter);
            if(facility == null)
            {
                throw new NotFoundException("Facility not found.");
            }
            return facility.Adapt<FacilityResponseDto>();
        }
    }
}
