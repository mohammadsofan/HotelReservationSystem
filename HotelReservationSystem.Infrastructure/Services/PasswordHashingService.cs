using HotelReservationSystem.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Infrastructure.Services
{
    internal class PasswordHashingService : IPasswordHashingService
    {
        private readonly IPasswordHasher<object> _passwordHasher;

        public PasswordHashingService(IPasswordHasher<object> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(new { },password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(new { }, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
