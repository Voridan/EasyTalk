using System.Linq.Expressions;
using BLL.Models;
using BLL.Utils;
using DAL.Models;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        Task<IEnumerable<User>> GetUserAsync(
            Expression<Func<User, bool>> filter,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
            string includeProperties);

        Task<User?> GetUserByIdAsync(Guid id);

        Task<Result<UserModel>> RegisterUserAsync(UserModel user);

        Task<Result<UserModel>> LoginUserAsync(LoginUserModel user);

        Task UpdateUser(UserModel user);

        Task DeleteUserAsync(Guid id);

        Task DeleteUser(UserModel user);
    }
}
