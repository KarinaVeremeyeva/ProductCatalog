using ProductCatalog.DAL.Entities;

namespace ProductCatalog.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(Guid id);

        Task<TEntity> UpdateAsync(TEntity entity);
    
        Task RemoveAsync(Guid id);
    }
}
