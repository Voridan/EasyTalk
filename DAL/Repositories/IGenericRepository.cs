using System.Linq.Expressions;

namespace DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAsync(
             Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");

        Task<T?> GetByIdAsync(Guid id);

        Task AddAsync(T obj);

        Task Update(T obj);

        Task DeleteAsync(Guid id);

        Task Delete(T entity);

        Task SaveAsync();
    }

}
