using HotelReservationSystem.Application.Commands.Facility;
using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Facility
{
    internal class CreateFacilityHandler : IRequestHandler<CreateFacilityCommand, FacilityResponseDto>
    {
        private readonly IFacilityRepository _facilityRepository;

        public CreateFacilityHandler(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        public async Task<FacilityResponseDto> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;
            var facility = requestDto.Adapt<Domain.Entities.Facility>();
            facility = await _facilityRepository.CreateAsync(facility, cancellationToken);
            return facility.Adapt<FacilityResponseDto>();
        }
    }
}
