using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
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
            var existingRoom = await _roomRepository.GetOneByFilterAsync(r=>r.Id == roomId);
            if (existingRoom == null)
            {
                throw new NotFoundException($"Room with id {roomId} not found.");
            }
            existingRoom.PricePerNight = requestDto.PricePerNight;
            existingRoom.MaxOccupancy = requestDto.MaxOccupancy;
            existingRoom.Type = requestDto.Type;
            existingRoom.FloorNumber = requestDto.FloorNumber;
            existingRoom.RoomNumber = requestDto.RoomNumber;
            existingRoom.UpdatedAt = DateTime.UtcNow;
            await _roomRepository.UpdateAsync(existingRoom,cancellationToken);
            return Unit.Value;
        }
    }
}
