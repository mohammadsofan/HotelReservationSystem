using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
            existingRoom.UpdatedAt = DateTime.UtcNow;
            await _roomRepository.UpdateAsync(existingRoom,cancellationToken);
            return Unit.Value;
        }
    }
}
