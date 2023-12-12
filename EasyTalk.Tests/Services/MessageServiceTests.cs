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
            result.Chat.Should().BeOfType<ChatModel>();
            result.Sender.Should().BeOfType<UserModel>();
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
    }
}
