using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.Extensions.Logging;
namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context, ILogger<Repository<User>> logger) : base(context, logger)
        {
            _context = context;
        }
    }
}
