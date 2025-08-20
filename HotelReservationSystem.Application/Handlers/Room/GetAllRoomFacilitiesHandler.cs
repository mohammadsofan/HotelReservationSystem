using HotelReservationSystem.Application.Dtos.Facility.Responses;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Queries.Room;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Room
{
    public class GetAllRoomFacilitiesHandler : IRequestHandler<GetAllRoomFacilitiesQuery, ICollection<FacilityResponseDto>>
    {
        private readonly IRoomFacilitiesRepository _roomFacilitiesRepository;

        public GetAllRoomFacilitiesHandler(IRoomFacilitiesRepository roomFacilitiesRepository) {
            _roomFacilitiesRepository = roomFacilitiesRepository;
        }
        public async Task<ICollection<FacilityResponseDto>> Handle(GetAllRoomFacilitiesQuery request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomId;
            var roomfacilites = await _roomFacilitiesRepository.GetAllByFilterAsync(rf=>rf.RoomId ==roomId,rf=>rf.Facility!);
            var facilities = roomfacilites.Select(rf => rf.Facility).ToList();
            return facilities.Adapt< ICollection<FacilityResponseDto>>().ToList();
        }
    }
}
