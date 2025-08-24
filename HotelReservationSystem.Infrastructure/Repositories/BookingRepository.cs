using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.Extensions.Logging;
namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context,ILogger<Repository<Booking>> logger) : base(context,logger)
        {
            _context = context;
        }
    }
}
