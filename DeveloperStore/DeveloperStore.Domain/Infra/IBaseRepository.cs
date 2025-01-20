using DeveloperStore.Domain.Models;

namespace DeveloperStore.Domain.Infra
{
    public interface IBaseRepository<TEntity> where TEntity : BaseModel
    {
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetByAllAsync(CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    }
}
