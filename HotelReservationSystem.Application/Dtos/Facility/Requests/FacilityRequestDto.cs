using HotelReservationSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.Dtos.Facility.Requests
{
    public class FacilityRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
