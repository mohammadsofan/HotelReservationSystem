namespace HotelReservationSystem.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string key, T value);
        Task RemoveAsync(string key);
    }
}
