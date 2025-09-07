using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.Extensions.Logging;
namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context, ILogger<Repository<Review>> logger) : base(context, logger)
        {
            _context = context;
        }
    }
}
