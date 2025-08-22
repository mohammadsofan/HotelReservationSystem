using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Facility;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Facility
{
    public class GetFacilitiesByFilterHandler : IRequestHandler<GetFacilitiesByFilterQuery, ICollection<FacilityResponseDto>>
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly ILogger<GetFacilitiesByFilterHandler> _logger;

        public GetFacilitiesByFilterHandler(IFacilityRepository facilityRepository, ILogger<GetFacilitiesByFilterHandler> logger)
        {
            _facilityRepository = facilityRepository;
            _logger = logger;
        }

        public async Task<ICollection<FacilityResponseDto>> Handle(GetFacilitiesByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving facilities with provided filter.");
            var filter = request.Filter;
            var facilities = await _facilityRepository.GetAllByFilterAsync(filter);
            _logger.LogInformation("Retrieved {Count} facilities.", facilities.Count());
            return facilities.Select(r => r.Adapt<FacilityResponseDto>()).ToList();
        }
    }
}
