using BLL.Models;
using BLL.Services.Implementations;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace EasyTalk.Tests.Services
{
    public class UserServiceTests
    {
        private Mock<UserRepository>? _userRepo;
        private UserService? _userService;
        private EasyTalkContext? _dbContext;

        public UserServiceTests()
        {
            _dbContext = Mock.Of<EasyTalkContext>();
            _userRepo = new Mock<UserRepository>(_dbContext);
            _userService = new UserService(_userRepo.Object);
        }

        [Fact]
        public async Task UserService_GetUserByIdAsync_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var existingUser = new User()
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                NickName = "john_doe",
                Email = "john.doe@example.com",
                Password = "Password123#",
            };

            _userRepo.Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(existingUser);
            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
            result.Id.Should().Be(userId);
            result.FirstName.Should().Be("John");
            result.LastName.Should().Be("Doe");
            result.NickName.Should().Be("john_doe");
            result.Email.Should().Be("john.doe@example.com");
            result.Password.Should().Be("Password123#");
        }

        [Fact]
        public async Task UserService_GetUserByIdAsync_ReturnsNull()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _userRepo.Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void UserService_DALUserToBLLUser_ReturnsUserModel()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                FirstName = "John",
                LastName = "Doe",
                NickName = "john_doe",
                Email = "john.doe@example.com",
                Password = "Password123#",
            };

            // Act
            var result = UserService.DALUserToBLLUser(user);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserModel>();

            result.FirstName.Should().Be(user.FirstName);
            result.LastName.Should().Be(user.LastName);
            result.NickName.Should().Be(user.NickName);
            result.Email.Should().Be(user.Email);
            result.Password.Should().Be(user.Password);
        }

        [Fact]
        public void UserService_DALUserToBLLUser_ReturnsNull()
        {
            // Arrange
            User user = null;

            // Act
            var result = UserService.DALUserToBLLUser(user);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void UserService_BLLUserToDALUser_ReturnsUser()
        {
            // Arrange
            var userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                NickName = "john_doe",
                Email = "john.doe@example.com",
                Password = "Password123#",
                Role = UserRole.Admin,
            };

            var userDal = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                NickName = "john_doe",
                Email = "john.doe@example.com",
                Password = "Password123#",
                Role = UserRole.Admin,
            };
            _userRepo.Setup(u => u.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(userDal);

            // Act
            var result = await _userService.BLLUserToDALUserAsync(userModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<User>();

            result.Id.Should().NotBe(userModel.Id);
            result.FirstName.Should().Be(userModel.FirstName);
            result.LastName.Should().Be(userModel.LastName);
            result.NickName.Should().Be(userModel.NickName);
            result.Email.Should().Be(userModel.Email);
            result.Password.Should().Be(userModel.Password);
            result.Role.Should().Be(userModel.Role);
        }

        [Fact]
        public async void UserService_BLLUserToDALUser_ReturnsNull()
        {
            // Arrange
            UserModel? userModel = null;

            // Act
            var result = await _userService.BLLUserToDALUserAsync(userModel);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UserService_GetAllUsersAsync_ReturnsListOfUserModels()
        {
            // Arrange
            var usersFromRepository = new List<User>
            {
                new User { FirstName = "Name1", LastName = "LName1", Email = "NameLName1@example.com" },
                new User { FirstName = "Name2", LastName = "LName2", Email = "NameLName2@example.com" },
                new User { FirstName = "Name3", LastName = "LName3",Email = "NameLName3@example.com" }
            };

            _userRepo.Setup(x => x.GetAllAsync())
                .ReturnsAsync(usersFromRepository);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(usersFromRepository.Count);

            foreach (var userModel in result)
            {
                var user = usersFromRepository
                    .FirstOrDefault(u => u.Email == userModel.Email);

                user.Should().NotBeNull();
                userModel.FirstName.Should().Be(user.FirstName);
                userModel.LastName.Should().Be(user.LastName);

            }

            _userRepo.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task UserService_UpdateUser_CallsRepositoryUpdate()
        {
            // Arrange
            var userModel = new UserModel
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                NickName = "john_doe",
                Email = "john.doe@example.com",
                Password = "Password123#"
            };

            var userDal = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                NickName = "john_doe",
                Email = "john.doe@example.com",
                Password = "Password123#"
            };
            _userRepo.Setup( u => u.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(userDal);
            _userRepo.Setup(x => x.Update(It.IsAny<User>()));

            // Act
            await _userService.UpdateUser(userModel);

            // Assert
            _userRepo.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UserService_DeleteAsync_DeletesExistingEntityByID()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe"
            };
            _userRepo.Setup(x => x.DeleteAsync(userId)).Returns(Task.CompletedTask);

            // Act
            await _userService.DeleteUserAsync(userId);

            // Assert
            _userRepo.Verify(x => x.DeleteAsync(userId), Times.Once);
        }

        [Fact]
        public async Task UserService_DeleteAsync_DeletesExistingEntityByUser()
        {
            // Arrange
            Guid userID = Guid.NewGuid();
            var user = new User
            {
                Id = userID,
                FirstName = "John",
                LastName = "Doe"
            };
            _userRepo.Setup(u => u.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
            _userRepo.Setup(x => x.Delete(user)).Returns(Task.CompletedTask);

            // Act
            var bllUser = UserService.DALUserToBLLUser(user);
            await _userService.DeleteUser(bllUser);

            // Assert
            _userRepo.Verify(x => x.Delete(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UserService_RegisterUserAsync_ValidUser_ReturnsSuccessResult()
        {
            // Arrange
            var user = new UserModel
            {
                FirstName = "John",
                LastName = "Doe",
                NickName = "JohnDoe",
                Email = "john.doe@example.com",
                Password = "ValidPassword123#",
                Role = UserRole.User
            };
            _userRepo.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.RegisterUserAsync(user);

            // Assert
            result.IsError.Should().BeFalse();
            result.Message.Should().Be("Register succeded.");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task UserService_RegisterUserAsync_InValidPassword()
        {
            // Arrange
            var user = new UserModel
            {
                FirstName = "John",
                LastName = "Doe",
                NickName = "JohnDoe",
                Email = "john.doe@example.com",
                Password = "invalidpassword123",
                Role = UserRole.User
            };
            _userRepo.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.RegisterUserAsync(user);

            // Assert
            result.IsError.Should().BeTrue();
            result.Message.Should().Be("Invalid password.\t");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task UserService_RegisterUserAsync_InValidEmail()
        {
            // Arrange
            var user = new UserModel
            {
                FirstName = "John",
                LastName = "Doe",
                NickName = "JohnDoe",
                Email = "invalidEmail",
                Password = "ValidPassword123#",
                Role = UserRole.User
            };
            _userRepo.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.RegisterUserAsync(user);

            // Assert
            result.IsError.Should().BeTrue();
            result.Message.Should().Be("Invalid email.\t");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task UserService_RegisterUserAsync_UnpresentRequiredFieldsNickName()
        {
            // Arrange
            var user = new UserModel
            {
                FirstName = "John",
                LastName = "Doe",
                NickName = null,
                Email = "john.doe@example.com",
                Password = "ValidPassword123#",
                Role = UserRole.User
            };
            _userRepo.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.RegisterUserAsync(user);

            // Assert
            result.IsError.Should().BeTrue();
            result.Message.Should().Be("Required fields are missing.");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task UserService_RegisterUserAsync_UnpresentRequiredFieldsFirstName()
        {
            // Arrange
            var user = new UserModel
            {
                FirstName = null,
                LastName = "Doe",
                NickName = "JohnDoe",
                Email = "john.doe@example.com",
                Password = "ValidPassword123#",
                Role = UserRole.User
            };
            _userRepo.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.RegisterUserAsync(user);

            // Assert
            result.IsError.Should().BeTrue();
            result.Message.Should().Be("Required fields are missing.");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task UserService_RegisterUserAsync_UnpresentRequiredFieldsLastName()
        {
            // Arrange
            var user = new UserModel
            {
                FirstName = "John",
                LastName = null,
                NickName = "JohnDoe",
                Email = "john.doe@example.com",
                Password = "ValidPassword123#",
                Role = UserRole.User
            };
            _userRepo.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.RegisterUserAsync(user);

            // Assert
            result.IsError.Should().BeTrue();
            result.Message.Should().Be("Required fields are missing.");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task UserService_LoginUserAsync_UnpresentPassword()
        {
            // Arrange
            var user = new LoginUserModel("JohnDoe", null);

            // Act
            var result = await _userService.LoginUserAsync(user);

            // Assert
            result.IsError.Should().BeTrue();
            result.Message.Should().Be("Nickname and password field were not provided");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task UserService_LoginUserAsync_UnpresentNickName()
        {
            // Arrange
            var user = new LoginUserModel(null, "ValidPassword123#");

            // Act
            var result = await _userService.LoginUserAsync(user);

            // Assert
            result.IsError.Should().BeTrue();
            result.Message.Should().Be("Nickname and password field were not provided");
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task UserService_LoginUserAsync_UserDoesNotExist()
        {
            // Arrange
            var loginUserModel = new LoginUserModel("JohnDoe", "ValidPassword123#");
            _userRepo.Setup(x => x.GetOneAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.LoginUserAsync(loginUserModel);

            // Assert
            result.IsError.Should().BeTrue();
            result.Message.Should().Be("User doesn`t exist.");
            result.Value.Should().BeNull();

        }

        [Fact]
        public async Task GetUserAsync_ReturnsFilteredUsers()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userName = "TestUser";
            var filteredUsers = new List<User>
            {
                new User { Id = userId, FirstName = userName, /* other properties */ },
                // Add more mocked users as needed
            };

            // Mock the user repository
            _userRepo.Setup(repo => repo.GetAsync(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                    It.IsAny<string>())
                )
                .ReturnsAsync(filteredUsers);

            // Define a filter
            Expression<Func<User, bool>> filter = u => u.Id == userId;

            // Act
            var result = await _userService.GetUserAsync(filter, null, "");

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<User>>(result);
            Assert.Single(result); // Ensure that only one user is returned based on the filter
            Assert.Equal(userId, result.First().Id); // Ensure the correct user is returned
            // Add more assertions as needed
        }

        [Fact]
        public async Task ChatExists_ReturnsTrueIfChatExists()
        {
            // Arrange
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            // Mock the user repository to return a user with a chat
            var user1WithChat = new User { Id = user1Id, Chats = new List<Chat> { new Chat { Users = new List<User> { new User { Id = user2Id } } } } };
            _userRepo.Setup(repo => repo.GetByIdAsync(user1Id)).ReturnsAsync(user1WithChat);

            // Act
            var result = await _userService.ChatExists(user1Id, user2Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ChatExists_ReturnsFalseIfChatDoesNotExist()
        {
            // Arrange
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            // Mock the user repository to return a user without a chat
            var user1WithoutChat = new User { Id = user1Id, Chats = null };
            _userRepo.Setup(repo => repo.GetByIdAsync(user1Id)).ReturnsAsync(user1WithoutChat);

            // Act
            var result = await _userService.ChatExists(user1Id, user2Id);

            // Assert
            Assert.False(result);
        }

    }
}
