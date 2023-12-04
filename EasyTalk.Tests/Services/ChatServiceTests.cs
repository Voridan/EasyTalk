using BLL.Models;
using BLL.Services.Implementations;
using BLL.Services.Interfaces;
using BLL.Utils;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyTalk.Tests.Services
{
    public class ChatServiceTests
    {
        private Mock<ChatRepository>? _chatRepo;
        private ChatService? _chatService;
        private EasyTalkContext? _dbContext;

        public ChatServiceTests()
        {
            _dbContext = Mock.Of<EasyTalkContext>();
            _chatRepo = new Mock<ChatRepository>(_dbContext);
            _chatService = new ChatService(_chatRepo.Object);
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
                Description = "About chat"
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
        public async Task ChatService_CreateChat_ReturnsFailedResult()
        {
            // Arrange
            var userModel1 = new UserModel()
            {
                FirstName = "User1",
            };

            var userModel2 = new UserModel()
            {
                FirstName = "User2",
            };
            _chatRepo.Setup(x => x.AddAsync(It.IsAny<Chat>()));

            // Act
            var result = await _chatService.CreateChat(null, userModel1, userModel2);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Result<ChatModel>>();
            result.IsError.Should().BeTrue();
            result.Message.Should().Be("Chat creation failed");
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
                Description = "About chat"
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
