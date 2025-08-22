using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.Extensions.Logging;
namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context, ILogger<Repository<Room>> logger) : base(context, logger)
        {
            _context = context;
        }
    }
}
