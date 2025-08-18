using HotelReservationSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Application.Dtos.Room.Requests
{
    public class CreateRoomRequestDto
    {
        public RoomType Type { get; set; }
        public decimal PricePerNight { get; set; }
        public int MaxOccupancy { get; set; }
    }
}
