using System.Linq.Expressions;
namespace HotelReservationSystem.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T,bool>>? filter = null, params Expression<Func<T, object>>[] includes);
        Task<T?> GetOneByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(long Id, T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
        Task<bool> HardDeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}
