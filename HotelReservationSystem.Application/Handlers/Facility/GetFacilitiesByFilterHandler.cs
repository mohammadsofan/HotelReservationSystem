using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Facility;
using Mapster;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.Facility
{
    public class GetFacilitiesByFilterHandler : IRequestHandler<GetFacilitiesByFilterQuery, ICollection<FacilityResponseDto>>
    {
        private readonly IFacilityRepository _facilityRepository;

        public GetFacilitiesByFilterHandler(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        public async Task<ICollection<FacilityResponseDto>> Handle(GetFacilitiesByFilterQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var facilities = await _facilityRepository.GetAllByFilterAsync(filter);
            return facilities.Select(r => r.Adapt<FacilityResponseDto>()).ToList();
        }
    }
}
