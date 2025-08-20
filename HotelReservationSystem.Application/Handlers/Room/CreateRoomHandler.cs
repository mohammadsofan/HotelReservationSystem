using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Room
{
    public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, RoomResponseDto>
    {
        private readonly IRoomRepository _roomRepository;

        public CreateRoomHandler(IRoomRepository roomRepository) {
            _roomRepository = roomRepository;
        }
        public async Task<RoomResponseDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var requestDto = request.RequestDto;
            var room = requestDto.Adapt<Domain.Entities.Room>();
            room = await _roomRepository.CreateAsync(room, cancellationToken);
            return room.Adapt<RoomResponseDto>();
        }
    }
}
