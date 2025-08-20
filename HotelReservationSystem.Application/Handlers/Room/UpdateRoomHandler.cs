using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
namespace HotelReservationSystem.Application.Handlers.Room
{
    public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand,Unit>
    {
        private readonly IRoomRepository _roomRepository;

        public UpdateRoomHandler(IRoomRepository roomRepository) {
            _roomRepository = roomRepository;
        }
        public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomId;
            var requestDto = request.RequestDto;
            var room = requestDto.Adapt<Domain.Entities.Room>();
            var result = await _roomRepository.UpdateAsync(roomId, room, cancellationToken);
            if (!result)
            {
                throw new NotFoundException($"Room with ID {roomId} not found.");
            }
            return Unit.Value;
        }
    }
}
