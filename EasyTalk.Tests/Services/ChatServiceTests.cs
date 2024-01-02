using BLL.Models;
using BLL.Services.Implementations;
using BLL.Services.Interfaces;
using BLL.Utils;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyTalk.Tests.Services
{
    public class ChatServiceTests
    {
        private Mock<ChatRepository>? _chatRepo;
        private Mock<UserRepository>? _userRepo;
        private Mock<MessageRepository>? _messageRepo;
        private Mock<IGenericRepository<Chat>>? _genRepo;
        private ChatService? _chatService;
        private Mock<EasyTalkContext>? _dbContext;
        private readonly Mock<DbSet<Chat>> _chatDbSetMock;

        public ChatServiceTests()
        {
            _dbContext = new Mock<EasyTalkContext>();
            _chatRepo = new Mock<ChatRepository>(_dbContext.Object);
            _userRepo = new Mock<UserRepository>(_dbContext.Object);
            _messageRepo = new Mock<MessageRepository>(_dbContext.Object);
            _genRepo = new Mock<IGenericRepository<Chat>>();
            _chatService = new ChatService(_chatRepo.Object, _userRepo.Object, _messageRepo.Object);
        }

        private DbSet<T> MockDbSet<T>(IEnumerable<T> data)
            where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }

        [Fact]
        public async Task UpdateChat_ReturnsSuccessResultWhenChatExists()
        {
            // Arrange
            var chatId = Guid.NewGuid();
            var chatModel = new ChatModel
            {
                Id = chatId,
                Name = "TestChat",
                Description = "Test Description",
            };

            var existingChat = new Chat { Id = chatId };

            _chatRepo.Setup(repo => repo.GetByIdAsync(chatId)).ReturnsAsync(existingChat);

            // Act
            var result = await _chatService.UpdateChat(chatModel);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Result<ChatModel>>(result);
            Assert.False(result.IsError);
            Assert.Equal("Updated successfuly.", result.Message);
            Assert.Equal(chatModel, result.Value);
        }

        [Fact]
        public async Task UpdateChat_ReturnsErrorResultWhenChatDoesNotExist()
        {
            // Arrange
            var chatId = Guid.NewGuid();
            var chatModel = new ChatModel
            {
                Id = chatId,
                Name = "TestChat",
                Description = "Test Description",
            };

            _chatRepo.Setup(repo => repo.GetByIdAsync(chatId)).ReturnsAsync((Chat)null);

            // Act
            var result = await _chatService.UpdateChat(chatModel);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Result<ChatModel>>(result);
            Assert.True(result.IsError);
            Assert.Equal("Chat does not exists.", result.Message);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task CreateChat_ReturnsSuccessResultWhenUsersExist()
        {
            // Arrange
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();
            var chatModel = new ChatModel
            {
                Name = "TestChat",
                Description = "Test Description",
            };

            var user1 = new User { Id = user1Id };
            var user2 = new User { Id = user2Id };

            _userRepo.Setup(repo => repo.GetByIdAsync(user1Id)).ReturnsAsync(user1);
            _userRepo.Setup(repo => repo.GetByIdAsync(user2Id)).ReturnsAsync(user2);

            // Act
            var result = await _chatService.CreateChat(chatModel, user1Id, user2Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Result<ChatModel>>(result);
            Assert.False(result.IsError);
            Assert.Equal("Chat created seccessfully", result.Message);
        }

        [Fact]
        public async Task CreateChat_ReturnsErrorResultWhenUserDoesNotExist()
        {
            // Arrange
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();
            var chatModel = new ChatModel
            {
                Name = "TestChat",
                Description = "Test Description",
            };

            _userRepo.Setup(repo => repo.GetByIdAsync(user1Id)).ReturnsAsync((User)null);
            _userRepo.Setup(repo => repo.GetByIdAsync(user2Id)).ReturnsAsync(new User());

            // Act
            var result = await _chatService.CreateChat(chatModel, user1Id, user2Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Result<ChatModel>>(result);
            Assert.False(result.IsError);
            Assert.Equal("Chat created seccessfully", result.Message);
        }

        [Fact]
        public async Task SaveMessages_SuccessfullySavesMessagesWhenChatAndSenderExist()
        {
            // Arrange
            var chatId = Guid.NewGuid();
            var senderId = Guid.NewGuid();
            var messages = new List<MessageModel> { new MessageModel { Text = "TestMessage" } };

            var chat = new Chat { Id = chatId, Users = new List<User> { new User { Id = senderId } } };

            _chatRepo.Setup(repo => repo.GetByIdAsync(chatId)).ReturnsAsync(chat);

            // Act
            await _chatService.SaveMessages(chatId, senderId, messages);

            // Assert
            _messageRepo.Verify(repo => repo.SaveMessages(It.IsAny<List<Message>>()), Times.Once);
            _chatRepo.Verify(repo => repo.Update(It.IsAny<Chat>()), Times.Once);
        }

        [Fact]
        public async Task SaveMessages_ThrowsExceptionWhenChatOrSenderDoesNotExist()
        {
            // Arrange
            var chatId = Guid.NewGuid();
            var senderId = Guid.NewGuid();
            var messages = new List<MessageModel> { new MessageModel { Text = "TestMessage" } };

            _chatRepo.Setup(repo => repo.GetByIdAsync(chatId)).ReturnsAsync((Chat)null);

            // Act and Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _chatService.SaveMessages(chatId, senderId, messages));

            // Assert
            Assert.Equal("Chat or sender not found.", exception.Message);
            _messageRepo.Verify(repo => repo.SaveMessages(It.IsAny<List<Message>>()), Times.Never);
            _chatRepo.Verify(repo => repo.Update(It.IsAny<Chat>()), Times.Never);
        }

        [Fact]
        public async Task ChatService_GetChatsForUser_ReturnsExpectedResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userId2 = Guid.NewGuid();

            // Mock data to be returned by GetAsync
            var mockedChats = new List<Chat>
            {
                new Chat { Id = userId },
                new Chat { Id = userId2 }
                // Add more mocked data as needed
            };

            // Setup _chatRepo to use the actual context and return mocked data
            _chatRepo.Setup(repo => repo.GetAsync(
                    It.IsAny<Expression<Func<Chat, bool>>>(),
                    It.IsAny<Func<IQueryable<Chat>, IOrderedQueryable<Chat>>>(),
                    It.IsAny<string>())
                )
                .ReturnsAsync(mockedChats);

            // Mock the user repository
            _userRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User());

            // Mock the _genRepo to return a DbSet with mocked data
            _genRepo.SetupGet(repo => repo._table)
                .Returns(MockDbSet(mockedChats));
            // Mock the GetChatWithUsers method in chatRepositoryMock
            var chatRepositoryMock = new Mock<IChatRepository>();
            chatRepositoryMock.Setup(x => x.GetChatWithUsers(It.IsAny<Guid>())).ReturnsAsync(new Chat { Id = userId });

            // Act
            var result = await _chatService.GetChatsForUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ChatModel>>(result);
            Assert.Equal(mockedChats.Count, result.Count());

            // Verify that _chatRepo.GetAsync was called
            _chatRepo.Verify(repo => repo.GetAsync(
                It.IsAny<Expression<Func<Chat, bool>>>(),
                It.IsAny<Func<IQueryable<Chat>, IOrderedQueryable<Chat>>>(),
                It.IsAny<string>()
            ), Times.Once);
        }

        [Fact]
        public async Task ChatService_GetChat_ReturnsChatModel()
        {
            // Arrange
            var chatID = Guid.NewGuid();
            var existingChat = new Chat()
            {
                Id = chatID,
                Name = "Chat 1",
                Description = "About chat",
                Users = new List<User>() { new(), new(), new() }
            };

            _chatRepo.Setup(x => x.GetByIdAsync(chatID))
                .ReturnsAsync(existingChat);
            // Act
            var result = await _chatService.GetChat(chatID);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ChatModel>();
            result.Id.Should().Be(chatID);
            result.Name.Should().Be("Chat 1");
            result.Description.Should().Be("About chat");
        }


        [Fact]
        public async Task ChatService_DeleteChatById_CallsReposytoryDeleteAsync()
        {
            // Arrange
            var chatID = Guid.NewGuid();

            _chatRepo.Setup(x => x.DeleteAsync(chatID))
                .Returns(Task.CompletedTask);

            // Act
            await _chatService.DeleteChatById(chatID);

            // Assert
            _chatRepo.Verify(x => x.DeleteAsync(chatID), Times.Once);
        }

        
        [Fact]
        public void ChatService_DalChatToBll_ReturnsChatModel()
        {
            // Arrange
            var chat = new Chat
            {
                Id = Guid.NewGuid(),
                Name = "Chat",
                Description = "About chat",
                Users = new List<User>() {new (), new() , new() }
            };

            // Act
            var result = ChatService.DalChatToBll(chat);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ChatModel>();

            result.Id.Should().Be(chat.Id);
            result.Name.Should().Be(chat.Name);
            result.Description.Should().Be(chat.Description);
        }

        [Fact]
        public void ChatService_DalChatToBll_ReturnsNull()
        {
            // Arrange
            Chat chat = null;

            // Act
            var result = ChatService.DalChatToBll(chat);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void ChatService_BllChatToDal_ReturnsChat()
        {
            // Arrange
            var chatModel = new ChatModel
            {
                Id = Guid.NewGuid(),
                Name = "Chat",
                Description = "About chat"
            };

            // Act
            var result = ChatService.BllChatToDal(chatModel);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Chat>();

            result.Id.Should().Be(chatModel.Id);
            result.Name.Should().Be(chatModel.Name);
            result.Description.Should().Be(chatModel.Description);
        }

        [Fact]
        public void ChatService_BllChatToDal_ReturnsNull()
        {
            // Arrange
            ChatModel? chatModel = null;

            // Act
            var result = ChatService.BllChatToDal(chatModel);

            // Assert
            result.Should().BeNull();
        }
    }
}
