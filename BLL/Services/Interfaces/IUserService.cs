﻿using BLL.Utils;
using BLL.Models;
using DAL.Models;
using System.Linq.Expressions;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        Task<IEnumerable<User>> GetUserAsync(
            Expression<Func<User, bool>> filter,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
            string includeProperties);

        Task<UserModel?> GetUserByIdAsync(Guid id);

        Task<Result<UserModel>> RegisterUserAsync(UserModel user);
        
        Task<Result<UserModel>> LoginUserAsync(LoginUserModel user);
        
        Task UpdateUser(UserModel user);

        Task DeleteUserAsync(Guid id);

        Task DeleteUser(UserModel user);
    }
}
