namespace HotelReservationSystem.Application.Interfaces
{
    public interface ICachService
    {
        Task<T?> GetAsync<T>(string key) where T : class;
        Task Set<T>(string key, T value);
        Task Remove(string key);
    }
}
