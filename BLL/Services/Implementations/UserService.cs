using BLL.Models;
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

        public async Task<Result<UserModel>> RegisterUserAsync(UserModel user)
        {
            Result<UserModel> registerResult = await IsValidRegistrationData(user);
            if (!registerResult.IsError)
            {
                var hashedPassword = PasswordUtil.HashPassword(user.Password!);
                user.Password = hashedPassword;

                var dalUser = BLLUserToDALUser(user);

                await _userRepo.AddAsync(dalUser);

                dalUser = await _userRepo.GetOneAsync(u => u.NickName == user.NickName);

                return new Result<UserModel>(registerResult.IsError, "Register succeded.", DALUserToBLLUser(dalUser));
            }

            return registerResult;
        }

        public async Task<Result<UserModel>> LoginUserAsync(LoginUserModel user)
        {
            Result<UserModel> loginResult = await IsValidLoginData(user);
            if (loginResult.IsError)
            {
                return new Result<UserModel>(loginResult.IsError, loginResult.Message);
            }

            return loginResult;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync() //ok
        {
            var users = await _userRepo.GetAllAsync();
            var bllUsers = new List<UserModel>();
            foreach(var user in users)
            {
                bllUsers.Add(DALUserToBLLUser(user));
            }

            return bllUsers;
        }

        public async Task<IEnumerable<User>> GetUserAsync(
            Expression<Func<User, bool>> filter,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy,
            string includeProperties)
        {
            return await _userRepo.GetAsync(filter, orderBy, includeProperties);
        }

        public async Task<User?> GetUserByIdAsync(Guid id) //ok
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task UpdateUser(UserModel user)
        {
            var dalUser = BLLUserToDALUser(user);
            await _userRepo.Update(dalUser);
        }
        
        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepo.DeleteAsync(id);
        }

        public async Task DeleteUser(UserModel user)
        {
            var dalUser = BLLUserToDALUser(user);
            await _userRepo.Delete(dalUser);
        }

        private async Task<Result<UserModel>> IsValidRegistrationData(UserModel user)
        {
            string message = "";
            bool passwordValidity = user.Password != null
                && new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").IsMatch(user.Password);
            message += passwordValidity ? "" : "Invalid password.\t";

            bool emailValidity = user.Email != null && new Regex("^\\S+@\\S+\\.\\S+$").IsMatch(user.Email);
            message += emailValidity ? "" : "Invalid email.\t";

            bool requiredFieldsPresent = user.NickName != null && user.FirstName != null && user.LastName != null;
            message += requiredFieldsPresent ? "" : "Required fields are missing.";

            User existingUser = await UserExists(user.NickName);
            if (existingUser != null)
            {
                return new Result<UserModel>(true, "User with provided nickname already exists");
            }

            return new Result<UserModel>(!(passwordValidity && emailValidity && requiredFieldsPresent), message);
        }

        private async Task<Result<UserModel>> IsValidLoginData(LoginUserModel user)
        {
            if (user.Password != null && user.NickName != null)
            {
                var password = user.Password;
                User existingUser = await UserExists(user.NickName);
                if (existingUser == null)
                {
                    return new Result<UserModel>(true, "User doesn`t exist.");
                }
                bool res = PasswordUtil.IsValidPassword(password, existingUser.Password);
                if (res)
                {
                    var bllUser = DALUserToBLLUser(existingUser);
                    return new Result<UserModel>(!res, "Login Success", bllUser);
                }

                return new Result<UserModel>(true, "Invalid Password");
            }

            return new Result<UserModel>(true, "Nickname and password field were not provided");
        }

        public async Task<bool> ChatExists(Guid user1, Guid user2)
        {
            var user1Dal = await _userRepo.GetByIdAsync(user1);
            if(user1Dal.Chats == null) 
                return false;

            return user1Dal.Chats.Any(chat => chat.Users.Any(user => user.Id == user2));
        }
        
        public static User? BLLUserToDALUser(UserModel user)
        {
            var dalUser = new User()
            {
              Id = user.Id,
              FirstName = user.FirstName,
              LastName = user.LastName,
              NickName = user.NickName,
              Email = user.Email,
              Password = user.Password
            };

            //foreach (var property in typeof(UserModel).GetProperties())
            //{
            //    if (property.Name == "Id") continue;

            //    var value = property.GetValue(user);
            //    if (value != null)
            //    {
            //        property.SetValue(dalUser, value);
            //    }
            //}

            return dalUser;
        }

        public static UserModel? DALUserToBLLUser(User user) //ok
        {
            if (user != null)
            {
                return new UserModel(user.Id, user.NickName, user.FirstName, user.LastName, user.Email, user.Password);
            }

            return null;
        }

        private async Task<User?> UserExists(string nickname)
        {
            var userData = await _userRepo.GetOneAsync(filter: u => u.NickName == nickname);
            return userData;

        }
    }
}
