using HotelReservationSystem.Application.Dtos.Facility.Responses;
using MediatR;
namespace HotelReservationSystem.Application.Queries.Room
{
    public class GetAllRoomFacilitiesQuery: IRequest<ICollection<FacilityResponseDto>>
    {
        public long RoomId { get;}
        public GetAllRoomFacilitiesQuery(long roomId)
        {
            RoomId = roomId;
        }
    }
}
