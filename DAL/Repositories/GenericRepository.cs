using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private EasyTalkContext _context;

        private DbSet<T> _table = default!;

        public GenericRepository()
        {
            _context = new EasyTalkContext();
            _table = _context.Set<T>();
        }

        public GenericRepository(EasyTalkContext _context)
        {
            this._context = _context;
            _table = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _table.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            string includeProperties = "")
        {
            IQueryable<T> query = _table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task InsertAsync(T obj)
        {
            await _table.AddAsync(obj);
        }

        public virtual void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            T existing = await _table.FindAsync(id);
            if (existing != null)
            {
                _table.Remove(existing);
            }
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _table.Attach(entity);
            }
            _table.Remove(entity);
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
