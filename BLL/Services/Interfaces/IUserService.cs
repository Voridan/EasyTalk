using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<IEnumerable<User>> GetUserAsync(
            Expression<Func<User, bool>> filter,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
            string includeProperties);

        Task<User?> GetUserByIdAsync(Guid id);

        Task AddUserAsync(User user);

        void UpdateUser(User user);

        Task DeleteUserAsync(Guid id);

        void DeleteUser(User user);
    }
}
