using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand,Unit>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<DeleteRoomHandler> _logger;

        public DeleteRoomHandler(IRoomRepository roomRepository, ILogger<DeleteRoomHandler> logger)
        {
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete room with ID {RoomId}", request.RoomId);
            var roomId = request.RoomId;
            var deleted = await _roomRepository.DeleteAsync(roomId);
            if (!deleted)
            {
                _logger.LogWarning("Room with id {RoomId} not found for deletion.", roomId);
                throw new NotFoundException($"Room with id {roomId} not found.");
            }
            _logger.LogInformation("Room with ID {RoomId} deleted successfully.", roomId);
            return Unit.Value;
        }
    }
}
