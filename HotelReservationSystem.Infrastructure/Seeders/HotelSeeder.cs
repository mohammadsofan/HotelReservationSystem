using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Requests;
using HotelReservationSystem.Domain.Constants;
using HotelReservationSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Infrastructure.Seeders
{
    public class HotelSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly ILogger<HotelSeeder> _logger;

        public HotelSeeder(ApplicationDbContext context, IMediator mediator, ILogger<HotelSeeder> logger)
        {
            _context = context;
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Seed()
        {
            try
            {
                _logger.LogInformation("Starting database migration.");
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Database migration completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not migrate the database.");
                throw new Exception($"could not Migrate the database", ex);
            }

            try
            {
                _logger.LogInformation("Checking database connection.");
                if (await _context.Database.CanConnectAsync())
                {
                    _logger.LogInformation("Database connection successful.");
                    if (!_context.Users.Any())
                    {
                        _logger.LogInformation("No users found. Seeding admin user.");
                        var admin = await _mediator.Send(new CreateUserCommand(
                             new CreateUserRequestDto()
                             {
                                 Email = "admin@admin.com",
                                 FirstName = "Admin",
                                 LastName = "Admin",
                                 Password = "Admin@123",
                                 ConfirmPassword = "Admin@123",
                                 IdCard = "00000000",
                                 PhoneNumber = "0000000000",
                                 Username = "Admin"

                             }, ApplicationRoles.Admin));
                        _logger.LogInformation("Admin user seeded successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not connect to the database or seed admin user.");
                throw new Exception($"Could not connect to the db", ex);
            }
        }
    }
}
