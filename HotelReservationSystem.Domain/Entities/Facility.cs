using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
