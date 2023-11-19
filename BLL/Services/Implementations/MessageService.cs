using BLL.Models;
using DAL.Models;

namespace BLL.Services.Implementations
{
    public class MessageService
    {
        public static MessageModel? DalMessageToBll(Message msg)
        {
            if(msg != null)
            {
                ChatModel? chatModel = ChatService.DalChatToBll(msg.Chat);
                UserModel? userModel = UserService.DALUserToBLLUser(msg.Sender);
                return new MessageModel { Id = msg.Id, Chat = chatModel!, Sender = userModel!, Text = msg.Text };
            }

            return null;
        }
    }
}
