using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Entities
{
    public class RoomFacilities:BaseEntity
    {
        [Required]
        public long FacilityId { get; set; }
        public Facility? Facility { get; set; }
        [Required]
        public long RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
