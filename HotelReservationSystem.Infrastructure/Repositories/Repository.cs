using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context) {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(id, cancellationToken);
            if(entity == null)
            {
                return false;
            }
            
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>>? filter = null)
        {

            return filter is null
                ?await _dbSet.ToListAsync()
                :await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<T?> GetOneByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> UpdateAsync(long id,T entity, CancellationToken cancellationToken = default)
        {
            var existingEntity = await _dbSet.FindAsync(id, cancellationToken);
            if(existingEntity == null)
            {
                return false;
            }
            entity.Id = existingEntity.Id;
            entity.CreatedAt = existingEntity.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
           return true;
        }
    }
}
