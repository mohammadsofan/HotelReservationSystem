using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Requests;
using HotelReservationSystem.Domain.Constants;
using HotelReservationSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace HotelReservationSystem.Infrastructure.Seeders
{
    public class HotelSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public HotelSeeder(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task Seed()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"could not Migrate the database", ex);
            }

            try
            {
                if (await _context.Database.CanConnectAsync())
                {
                    if (!_context.Users.Any())
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not connect to the db", ex);
            }
        }
    }
}
