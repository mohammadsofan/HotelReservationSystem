using HotelReservationSystem.Application.Dtos.Room.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Commands.Room
{
    public class UpdateRoomCommand:IRequest<Unit>
    {
        public long RoomId { get; set; }
        public RoomRequestDto RequestDto { get;}

        public UpdateRoomCommand(long roomId,RoomRequestDto requestDto)
        {
            RoomId = roomId;
            RequestDto = requestDto;
        }
    }
}
