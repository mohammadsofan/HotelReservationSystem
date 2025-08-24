using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.Dtos.Review.Requests
{
    public class ReviewRequestDto
    {
        public long UserId { get; set; }
        public long RoomId { get; set; }
        public string? Comment { get; set; }
        public int Rate { get; set; }
    }
}
