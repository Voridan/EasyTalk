using System.Linq.Expressions;

namespace DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAsync(
             Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            string includeProperties);

        Task<T?> GetByIdAsync(Guid id);

        Task AddAsync(T obj);

        void Update(T obj);

        Task DeleteAsync(Guid id);

        void Delete(T entity);

        Task SaveAsync();
    }

}
