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

        public async Task<Result> RegisterUserAsync(User user)
        {
            Result registerResult = IsValidRegistrationData(user);
            if (!registerResult.IsError)
            {
                string hashedPassword = PasswordUtil.HashPassword(user.Password!);
                user.Password = hashedPassword;

                await _userRepo.AddAsync(user);
                return new Result(registerResult.IsError, "Register succeded.");
            }

            return registerResult;
        }

        public async Task<Result> LoginUserAsync(LoginUser user)
        {
            Result loginResult = await IsValidLoginData(user);
            if (loginResult.IsError)
            {
                return new Result(loginResult.IsError, loginResult.Message);
            }

            return loginResult;
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

        private Result IsValidRegistrationData(User user)
        {
            string message="";
            bool passwordValidity = user.Password != null
                && new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").IsMatch(user.Password);
            message += passwordValidity ? "" : "Invalid password.\t"; 

            bool emailValidity = user.Email != null && new Regex("^\\S+@\\S+\\.\\S+$").IsMatch(user.Email);
            message += emailValidity ? "" : "Invalid email.\t";

            bool requiredFieldsPresent = user.NickName != null && user.FirstName != null && user.LastName != null;
            message += requiredFieldsPresent ? "" : "Required fields are missing.";

            return new Result(passwordValidity  && emailValidity && requiredFieldsPresent,message);
        }

        private async Task<Result> IsValidLoginData(LoginUser user)
        {

            if (user.Password != null && user.NickName != null)
            {
                var password = user.Password;
                try
                {
                    var userData = await _userRepo.GetOneAsync(filter: u => u.NickName == user.NickName);
                    var hashedPassword = user.Password;
                    bool res = PasswordUtil.IsValidPassword(password, hashedPassword);
                    return new Result(!res);
                }
                catch (InvalidOperationException)
                {
                    return new Result(true, "User with provided nickname not found");
                }
                catch (Exception ex)
                {
                    return new Result(true, "Something gone wrong!");
                }
            }

            return new Result(true, "Nickname and password field were not provided");
        }
    }
}
