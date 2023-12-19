using BLL.Models;
using BLL.Services.Implementations;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTalk.Tests.Services
{
    public class MessageServiceTests
    {
        [Fact]
        public void ProjectService_DalMessageToBll_ReturnsProjectModel()
        {
            // Arrange
            var message = new Message
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Text = "Hi it is EasyTalk",
                SenderId = Guid.NewGuid(),
                Sender = new User(),
                ChatId = Guid.NewGuid(),
                Chat = new Chat()
            };

            // Act
            var result = MessageService.DalMessageToBll(message);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<MessageModel>();

            result.Id.Should().Be(message.Id);
            result.ChatId.Should().Be(message.ChatId);
            result.SenderId.Should().Be(message.SenderId);
            result.Text.Should().Be(message.Text);
        }

        [Fact]
        public void ProjectService_DalMessageToBll_ReturnsNull()
        {
            // Arrange
            Message message = null;

            // Act
            var result = MessageService.DalMessageToBll(message);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void BllMessageToDal_ReturnsDalMessageWhenMessageModelIsNotNull()
        {
            // Arrange
            var messageId = Guid.NewGuid();
            var chatId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var messageModel = new MessageModel
            {
                Id = messageId,
                ChatId = chatId,
                Text = "Test message",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
            };

            var user = new User { Id = userId };

            // Act
            var result = MessageService.BllMessageToDal(messageModel, user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Message>(result);
            Assert.Equal(messageId, result.Id);
            Assert.Equal(chatId, result.ChatId);
            Assert.Equal(user, result.Sender);
            Assert.Equal("Test message", result.Text);
            Assert.Equal(messageModel.CreatedDate, result.CreatedDate);
            Assert.Equal(messageModel.ModifiedDate, result.ModifiedDate);
        }

        [Fact]
        public void BllMessageToDal_ReturnsNullWhenMessageModelIsNull()
        {
            // Arrange
            MessageModel messageModel = null;
            var user = new User { Id = Guid.NewGuid() };

            // Act
            var result = MessageService.BllMessageToDal(messageModel, user);

            // Assert
            Assert.Null(result);
        }

    }
}
