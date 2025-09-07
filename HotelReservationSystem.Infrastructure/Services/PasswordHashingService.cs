using HotelReservationSystem.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Infrastructure.Services
{
    internal class PasswordHashingService : IPasswordHashingService
    {
        private readonly IPasswordHasher<object> _passwordHasher;
        private readonly ILogger<PasswordHashingService> _logger;

        public PasswordHashingService(IPasswordHasher<object> passwordHasher, ILogger<PasswordHashingService> logger)
        {
            _passwordHasher = passwordHasher;
            _logger = logger;
        }
        public string HashPassword(string password)
        {
            _logger.LogInformation("Hashing password.");
            var hashed = _passwordHasher.HashPassword(new { },password);
            _logger.LogInformation("Password hashed.");
            return hashed;
        }

        public bool VerifyHashedPassword(string password, string hashedPassword)
        {
            _logger.LogInformation("Verifying hashed password.");
            var result = _passwordHasher.VerifyHashedPassword(new { }, hashedPassword, password);
            var success = result == PasswordVerificationResult.Success;
            _logger.LogInformation("Password verification result: {Result}", success);
            return success;
        }
    }
}
