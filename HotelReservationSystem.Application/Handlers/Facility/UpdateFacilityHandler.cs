using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
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
            var existingFacility = await _facilityRepository.GetOneByFilterAsync(f => f.Id == facilityId);
            if(existingFacility == null)
            {
                throw new NotFoundException($"Facility with ID {facilityId} not found.");
            }
            var requestDto = request.RequestDto;
            existingFacility.Description = requestDto.Description;
            existingFacility.Name = requestDto.Name;
            existingFacility.UpdatedAt = DateTime.UtcNow;
            await _facilityRepository.UpdateAsync(existingFacility, cancellationToken);
            return Unit.Value;
        }
    }
}
