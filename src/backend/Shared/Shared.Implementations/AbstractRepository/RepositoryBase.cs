using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Repository;
using Shared.Abstractions.Repository.Abstract;

namespace Shared.Implementations.AbstractRepository;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected RepositoryBase(DbContext dbContext)
    {
        _context = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync();
    }

    public async Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);
        _dbSet.Remove(entity!);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);
        return entity!;
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}
