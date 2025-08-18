using HotelReservationSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace HotelReservationSystem.Domain.Entities
{
    public class Room:BaseEntity
    {
        [Required]
        public RoomType Type {  get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int MaxOccupancy { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<RoomFacilities> RoomFacilities { get; set;} = new List<RoomFacilities>();
    }
}
