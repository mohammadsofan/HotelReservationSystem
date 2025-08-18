using HotelReservationSystem.Application.Dtos.Room.Requests;
using HotelReservationSystem.Application.Dtos.Room.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Commands.Room
{
    public class CreateRoomCommand:IRequest<RoomResponseDto>
    {
        public CreateRoomRequestDto RequestDto { get; }

        public CreateRoomCommand(CreateRoomRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
}
