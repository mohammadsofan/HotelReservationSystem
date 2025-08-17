using HotelReservationSystem.Domain.Enums;
using HotelReservationSystem.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
namespace HotelReservationSystem.Domain.Entities
{
    public class Room
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public RoomType Type {  get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int MaxOccupancy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
