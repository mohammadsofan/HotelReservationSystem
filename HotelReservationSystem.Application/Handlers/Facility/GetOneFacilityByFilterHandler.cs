using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Facility;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Facility
{
    public class GetOneFacilityByFilterHandler : IRequestHandler<GetOneFacilityByFilterQuery, FacilityResponseDto>
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly ILogger<GetOneFacilityByFilterHandler> _logger;

        public GetOneFacilityByFilterHandler(IFacilityRepository facilityRepository, ILogger<GetOneFacilityByFilterHandler> logger)
        {
            _facilityRepository = facilityRepository;
            _logger = logger;
        }

        public async Task<FacilityResponseDto> Handle(GetOneFacilityByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving single facility with provided filter.");
            var filter = request.Filter;
            var facility = await _facilityRepository.GetOneByFilterAsync(filter);
            if(facility == null)
            {
                _logger.LogWarning("Facility not found for provided filter.");
                throw new NotFoundException("Facility not found.");
            }
            _logger.LogInformation("Facility found with ID {FacilityId}.", facility.Id);
            return facility.Adapt<FacilityResponseDto>();
        }
    }
}
