using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;

namespace HotelReservationSystem.Application.Handlers.Facility
{
    public class DeleteFacilityHandler : IRequestHandler<DeleteFacilityCommand, Unit>
    {
        private readonly IFacilityRepository _facilityRepository;

        public DeleteFacilityHandler(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }
        public async Task<Unit> Handle(DeleteFacilityCommand request, CancellationToken cancellationToken)
        {
            var facilityId = request.FacilityId;
            var deleted = await _facilityRepository.DeleteAsync(facilityId, cancellationToken);
            if(!deleted)
            {
                throw new NotFoundException($"Facility with ID {facilityId} not found.");
            }
            return Unit.Value;
        }
    }
}
