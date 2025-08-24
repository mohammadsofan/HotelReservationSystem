namespace HotelReservationSystem.Application.Dtos.Booking.Requests
{
    public class BookingRequestDto
    {
        public long UserId { get; set; }
        public long RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int GuestsNumber { get; set; }
    }
}
