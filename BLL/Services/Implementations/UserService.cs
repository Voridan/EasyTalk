using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepo;

        public UserService(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepo.AddAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepo.DeleteAsync(id);
        }

        public void DeleteUser(User user)
        {
            _userRepo.Delete(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<IEnumerable<User>> GetUserAsync(
            Expression<Func<User, bool>> filter,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
            string includeProperties)
        {
            return await _userRepo.GetAsync(filter, orderBy, includeProperties);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public void UpdateUser(User user)
        {
            _userRepo.Update(user);
        }
    }
}
