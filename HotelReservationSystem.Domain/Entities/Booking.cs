using System.ComponentModel.DataAnnotations;
namespace HotelReservationSystem.Domain.Entities
{
    public class Booking:BaseEntity
    {
        [Required]
        public long UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public long RoomId { get; set; }
        public Room? Room { get; set; }
        
        [Required]
        public DateTime CheckIn { get; set; }
        
        [Required]
        public DateTime CheckOut { get; set; }
        
        [Required]
        [Range(1, 50)]
        public int GuestsNumber { get; set; }

    }
}
