namespace HotelReservationSystem.Application.Dtos.Review.Responses
{
    public class ReviewResponseDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoomId { get; set; }
        public string? Comment { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
