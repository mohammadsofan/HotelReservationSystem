using System.ComponentModel.DataAnnotations;
namespace HotelReservationSystem.Domain.Entities
{
    public class Review:BaseEntity
    {
        [Required]
        public long UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public long RoomId { get; set; }
        public Room? Room { get; set; }
        [Required]
        [StringLength(1000)]
        public string? Comment { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rate {  get; set; }
    }
}
