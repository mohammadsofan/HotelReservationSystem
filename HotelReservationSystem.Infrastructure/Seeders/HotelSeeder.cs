using HotelReservationSystem.Application.Commands.User;
using HotelReservationSystem.Application.Dtos.User.Requests;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Infrastructure.Seeders
{
    public class HotelSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public HotelSeeder(ApplicationDbContext context,IMediator mediator)
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
            catch(Exception ex)
            {
                throw new Exception($"could not Migrate the database {ex.Message}");
            }

            try
            {
                if (await _context.Database.CanConnectAsync())
                {
                    if (!_context.Users.Any()) {
                        await _mediator.Send(new CreateUserCommand(
                             new CreateUserRequestDto()
                             {
                                 Email = "admin@admin.com",
                                 FirstName = "Admin",
                                 LastName = "Admin",
                                 Password = "Admin@123",
                                 IdCard = "00000000",
                                 PhoneNumber = "0000000000",
                                 Username = "Admin"
                             }));    
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not connect to the db {ex.Message}");
            }
        }
    }
}
