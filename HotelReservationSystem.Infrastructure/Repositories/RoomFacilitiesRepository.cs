using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class RoomFacilitiesRepository : Repository<RoomFacilities>, IRoomFacilitiesRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomFacilitiesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
