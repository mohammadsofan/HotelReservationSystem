using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Dtos.Room.Responses;
using HotelReservationSystem.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, RoomResponseDto>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<CreateRoomHandler> _logger;

        public CreateRoomHandler(IRoomRepository roomRepository, ILogger<CreateRoomHandler> logger) {
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public async Task<RoomResponseDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting room creation.");
            var requestDto = request.RequestDto;
            var room = requestDto.Adapt<Domain.Entities.Room>();
            room = await _roomRepository.CreateAsync(room, cancellationToken);
            _logger.LogInformation("Room created successfully with ID {RoomId}.", room.Id);
            return room.Adapt<RoomResponseDto>();
        }
    }
}
