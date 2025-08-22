using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand,Unit>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<UpdateRoomHandler> _logger;

        public UpdateRoomHandler(IRoomRepository roomRepository, ILogger<UpdateRoomHandler> logger) {
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting room update for room {RoomId}", request.RoomId);
            var roomId = request.RoomId;
            var requestDto = request.RequestDto;
            var room = requestDto.Adapt<Domain.Entities.Room>();
            var result = await _roomRepository.UpdateAsync(roomId, room, cancellationToken);
            if (!result)
            {
                _logger.LogWarning("Room with ID {RoomId} not found for update.", roomId);
                throw new NotFoundException($"Room with ID {roomId} not found.");
            }
            _logger.LogInformation("Room updated successfully for room {RoomId}", roomId);
            return Unit.Value;
        }
    }
}
