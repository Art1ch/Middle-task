using Shared.Abstractions.Repository.Abstract;

namespace Shared.Abstractions.Repository;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
