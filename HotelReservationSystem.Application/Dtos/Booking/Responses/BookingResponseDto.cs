namespace HotelReservationSystem.Application.Dtos.Booking.Responses
{
    public class BookingResponseDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int GuestsNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
