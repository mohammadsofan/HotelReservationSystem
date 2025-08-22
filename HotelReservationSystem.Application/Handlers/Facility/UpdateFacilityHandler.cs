using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Facility
{
    public class UpdateFacilityHandler : IRequestHandler<UpdateFacilityCommand, Unit>
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly ILogger<UpdateFacilityHandler> _logger;

        public UpdateFacilityHandler(IFacilityRepository facilityRepository, ILogger<UpdateFacilityHandler> logger)
        {
            _facilityRepository = facilityRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateFacilityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting facility update for facility {FacilityId}", request.FacilityId);
            var facilityId = request.FacilityId;
            var requestDto = request.RequestDto;
            var facility = requestDto.Adapt<Domain.Entities.Facility>();
            var result = await _facilityRepository.UpdateAsync(facilityId, facility, cancellationToken);
            if (!result)
            {
                _logger.LogWarning("Facility with ID {FacilityId} not found for update.", facilityId);
                throw new NotFoundException($"Facility with ID {facilityId} not found.");
            }
            _logger.LogInformation("Facility updated successfully for facility {FacilityId}", facilityId);
            return Unit.Value;
        }
    }
}
