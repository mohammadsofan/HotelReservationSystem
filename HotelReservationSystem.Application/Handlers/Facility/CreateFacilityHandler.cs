using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Facility
{
    internal class CreateFacilityHandler : IRequestHandler<CreateFacilityCommand, FacilityResponseDto>
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly ILogger<CreateFacilityHandler> _logger;

        public CreateFacilityHandler(IFacilityRepository facilityRepository, ILogger<CreateFacilityHandler> logger)
        {
            _facilityRepository = facilityRepository;
            _logger = logger;
        }

        public async Task<FacilityResponseDto> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting facility creation.");
            var requestDto = request.RequestDto;
            var facility = requestDto.Adapt<Domain.Entities.Facility>();
            facility = await _facilityRepository.CreateAsync(facility, cancellationToken);
            _logger.LogInformation("Facility created successfully with ID {FacilityId}.", facility.Id);
            return facility.Adapt<FacilityResponseDto>();
        }
    }
}
