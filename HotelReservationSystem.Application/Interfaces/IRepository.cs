using System.Linq.Expressions;
namespace HotelReservationSystem.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T,bool>>? filter = null);
        Task<T?> GetOneByFilterAsync(Expression<Func<T, bool>> filter);
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(long Id, T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);

    }
}
