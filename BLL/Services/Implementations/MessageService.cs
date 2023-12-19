using BLL.Models;
using DAL.Models;

namespace BLL.Services.Implementations
{
    public class MessageService
    {
        public static MessageModel? DalMessageToBll(Message msg)
        {
            if (msg != null)
            {
                return new MessageModel
                {
                    Id = msg.Id,
                    ChatId = msg.ChatId,
                    SenderId = msg.SenderId,
                    Text = msg.Text,
                    CreatedDate = msg.CreatedDate,
                    ModifiedDate = msg.ModifiedDate,
                };
            }

            return null;
        }

        public static Message? BllMessageToDal(MessageModel msg, User user)
        {
            if (msg != null)
            {
                return new Message
                {
                    Id = msg.Id,
                    ChatId = msg.ChatId,
                    Sender = user,
                    Text = msg.Text,
                    CreatedDate = msg.CreatedDate,
                    ModifiedDate = msg.ModifiedDate,
                };
            }

            return null;
        }
    }
}
