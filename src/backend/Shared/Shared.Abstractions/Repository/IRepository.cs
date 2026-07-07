using Shared.Abstractions.Repository.Abstract;

namespace Shared.Abstractions.Repository;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
