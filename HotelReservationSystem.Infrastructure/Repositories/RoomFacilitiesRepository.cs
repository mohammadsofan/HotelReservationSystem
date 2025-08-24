using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.Extensions.Logging;
namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class RoomFacilitiesRepository : Repository<RoomFacilities>, IRoomFacilitiesRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomFacilitiesRepository(ApplicationDbContext context, ILogger<Repository<RoomFacilities>> logger) : base(context, logger)
        {
            _context = context;
        }
    }
}
