namespace HotelReservationSystem.Application.Interfaces
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
