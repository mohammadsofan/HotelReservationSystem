using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Facility
{
    public class DeleteFacilityHandler : IRequestHandler<DeleteFacilityCommand, Unit>
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly ILogger<DeleteFacilityHandler> _logger;

        public DeleteFacilityHandler(IFacilityRepository facilityRepository, ILogger<DeleteFacilityHandler> logger)
        {
            _facilityRepository = facilityRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteFacilityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete facility with ID {FacilityId}", request.FacilityId);
            var facilityId = request.FacilityId;
            var deleted = await _facilityRepository.DeleteAsync(facilityId, cancellationToken);
            if(!deleted)
            {
                _logger.LogWarning("Facility with ID {FacilityId} not found for deletion.", facilityId);
                throw new NotFoundException($"Facility with ID {facilityId} not found.");
            }
            _logger.LogInformation("Facility with ID {FacilityId} deleted successfully.", facilityId);
            return Unit.Value;
        }
    }
}
