using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace HotelReservationSystem.Infrastructure.Repositories
{
    internal class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<Repository<T>> _logger;
        public Repository(ApplicationDbContext context, ILogger<Repository<T>> logger) {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Creating entity of type {EntityType}", typeof(T).Name);
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Entity created with ID {EntityId}", entity.Id);
            return entity;
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Deleting entity of type {EntityType} with ID {EntityId}", typeof(T).Name, id);
            var entity = await _dbSet.FindAsync(id, cancellationToken);
            if(entity == null)
            {
                _logger.LogWarning("Entity of type {EntityType} with ID {EntityId} not found for deletion.", typeof(T).Name, id);
                return false;
            }
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Entity of type {EntityType} with ID {EntityId} marked as deleted.", typeof(T).Name, id);
            return true;
        }
        public async Task<bool> HardDeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Hard deleting entity of type {EntityType} with ID {EntityId}", typeof(T).Name, id);
            var entity = await _dbSet.FindAsync(id, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("Entity of type {EntityType} with ID {EntityId} not found for hard deletion.", typeof(T).Name, id);
                return false;
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Entity of type {EntityType} with ID {EntityId} hard deleted.", typeof(T).Name, id);
            return true;
        }
        public async Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
        {
            _logger.LogInformation("Retrieving all entities of type {EntityType} with filter.", typeof(T).Name);
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }   
            if (filter != null)
            {
                query = query.Where(filter);
            }
            var result = await query.AsNoTracking().ToListAsync();
            _logger.LogInformation("Retrieved {Count} entities of type {EntityType}.", result.Count, typeof(T).Name);
            return result;
        }

        public async Task<T?> GetOneByFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            _logger.LogInformation("Retrieving single entity of type {EntityType} with filter.", typeof(T).Name);
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var entity = await query.Where(filter).FirstOrDefaultAsync();
            if(entity == null)
            {
                _logger.LogWarning("Entity of type {EntityType} not found for provided filter.", typeof(T).Name);
            }
            else
            {
                _logger.LogInformation("Entity of type {EntityType} found with ID {EntityId}.", typeof(T).Name, entity.Id);
            }
            return entity;
        }

        public async Task<bool> UpdateAsync(long id,T entity, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Updating entity of type {EntityType} with ID {EntityId}", typeof(T).Name, id);
            var existingEntity = await _dbSet.FindAsync(id, cancellationToken);
            if(existingEntity == null)
            {
                _logger.LogWarning("Entity of type {EntityType} with ID {EntityId} not found for update.", typeof(T).Name, id);
                return false;
            }
            entity.Id = existingEntity.Id;
            entity.CreatedAt = existingEntity.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Entity of type {EntityType} with ID {EntityId} updated successfully.", typeof(T).Name, id);
           return true;
        }
    }
}
