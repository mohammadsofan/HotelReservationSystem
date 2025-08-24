using System.ComponentModel.DataAnnotations;
namespace HotelReservationSystem.Domain.Entities
{
    public class Facility:BaseEntity
    {

        [Required]
        [StringLength(100,MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; } = string.Empty;
        public ICollection<RoomFacilities> RoomFacilities { get; set; } = new List<RoomFacilities>();

    }
}
