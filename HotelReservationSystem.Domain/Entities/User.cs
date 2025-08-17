using HotelReservationSystem.Domain.Constants;
using System.ComponentModel.DataAnnotations;
namespace HotelReservationSystem.Domain.Entities
{
    public class User
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string HashedPassword { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string IdCard { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = ApplicationRoles.Guest;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
