using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class FacilityRepository : Repository<Facility>, IFacilityRepository
    {
        private readonly ApplicationDbContext _context;

        public FacilityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
