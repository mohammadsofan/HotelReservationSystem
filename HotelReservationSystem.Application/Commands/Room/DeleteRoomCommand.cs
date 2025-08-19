using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Commands.Room
{
    public class DeleteRoomCommand:IRequest<Unit>
    {
        public long RoomId { get; }

        public DeleteRoomCommand(long roomId)
        {
            RoomId = roomId;
        }
    }
}
