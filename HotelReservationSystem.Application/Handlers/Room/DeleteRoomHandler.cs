using HotelReservationSystem.Application.Commands.Room;
using HotelReservationSystem.Application.Exceptions;
using HotelReservationSystem.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Handlers.Room
{
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand>
    {
        private readonly IRoomRepository _roomRepository;

        public DeleteRoomHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomId;
            var room = await _roomRepository.GetOneByFilterAsync(r=>r.Id == roomId);
            if (room == null)
            {
                throw new NotFoundException($"Room with id {roomId} not found.");
            }
            await _roomRepository.DeleteAsync(roomId);
        }
    }
}
