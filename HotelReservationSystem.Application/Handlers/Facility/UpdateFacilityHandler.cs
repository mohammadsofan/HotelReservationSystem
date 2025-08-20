using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Facility
{
    public class UpdateFacilityHandler : IRequestHandler<UpdateFacilityCommand, Unit>
    {
        private readonly IFacilityRepository _facilityRepository;

        public UpdateFacilityHandler(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }
        public async Task<Unit> Handle(UpdateFacilityCommand request, CancellationToken cancellationToken)
        {
            var facilityId = request.FacilityId;
            var requestDto = request.RequestDto;
            var facility = requestDto.Adapt<Domain.Entities.Facility>();
            var result = await _facilityRepository.UpdateAsync(facilityId, facility, cancellationToken);
            if (!result)
            {
                throw new NotFoundException($"Facility with ID {facilityId} not found.");
            }
            return Unit.Value;
        }
    }
}
