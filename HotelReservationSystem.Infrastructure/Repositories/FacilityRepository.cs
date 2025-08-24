using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.Extensions.Logging;
namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class FacilityRepository : Repository<Facility>, IFacilityRepository
    {
        private readonly ApplicationDbContext _context;

        public FacilityRepository(ApplicationDbContext context, ILogger<Repository<Facility>> logger) : base(context, logger)
        {
            _context = context;
        }
    }
}
