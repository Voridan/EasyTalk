using BLL.Services.Interfaces;
using BLL.Utils;
using DAL.Models;
using DAL.Repositories;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepo;

        public UserService(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task RegisterUserAsync(User user)
        {
            if (IsValidRegistrationData(user))
            {
                string hashedPassword = PasswordUtil.HashPassword(user.Password!);
                user.Password = hashedPassword;

                await _userRepo.AddAsync(user);
            }

            throw new Exception("Invalid Data");
        }

        public async Task LoginUserAsync(User user)
        {
            if (IsValidRegistrationData(user))
            {
                string hashedPassword = PasswordUtil.HashPassword(user.Password!);
                user.Password = hashedPassword;

                await _userRepo.AddAsync(user);
            }

            throw new Exception("Invalid Data");
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

        private bool IsValidRegistrationData(User user)
        {
            bool passwordValidity = user.Password != null
                && new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").IsMatch(user.Password);

            bool emailValidity = user.Email != null && new Regex("^\\S+@\\S+\\.\\S+$").IsMatch(user.Email);

            bool requiredFieldsPresent = user.NickName != null && user.FirstName != null && user.LastName != null;

            return passwordValidity && emailValidity && requiredFieldsPresent;
        }

        private async Task<bool> IsValidLoginData(LoginUser user)
        {
            if (user.Password != null && user.NickName != null)
            {
                var password = user.Password;
                try
                {
                    var userData = await _userRepo.GetOneAsync(filter: u => u.NickName == user.NickName);
                    var hashedPassword = user.Password;

                    return PasswordUtil.IsValidPassword(password, hashedPassword);
                }
                catch (InvalidOperationException)
                {
                    throw new Exception("User with provided nickname not found");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            throw new Exception("Nickname and password field were not provided");
        }
    }
}
