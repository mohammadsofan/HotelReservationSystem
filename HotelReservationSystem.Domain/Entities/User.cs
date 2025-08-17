using System.ComponentModel.DataAnnotations;
namespace HotelReservationSystem.Domain.Entities
{
    public class User
    {
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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
