using BLL.Models;
using BLL.Utils;
using DAL.Models;
using DAL.Repositories;

namespace BLL.Services.Implementations
{
    public class ChatService
    {
        private readonly ChatRepository chatRepository;
        public ChatService(ChatRepository _repo)
        {
            chatRepository = _repo;
        }

        public async Task<IEnumerable<ChatModel>> GetChatsForUser(Guid userId)
        {
            var chats = await chatRepository.GetAsync(c => c.Users.Any(u => u.Id == userId));
            var bllChats = new List<ChatModel>();
            foreach (var chat in chats)
            {
                bllChats.Add(DalChatToBll(chat)!);
            }

            return bllChats;
        }

        public async Task<ChatModel?> GetChat(Guid chatId)
        {
            var chat = await chatRepository.GetByIdAsync(chatId);

            return DalChatToBll(chat);
        }

        public async Task<Result<ChatModel>> CreateChat(ChatModel chat, UserModel user1, UserModel user2)
        {
            var dalChat = BllChatToDal(chat);
            if (dalChat != null)
            {
                dalChat.Users.Add(UserService.BLLUserToDALUser(user1));
                dalChat.Users.Add(UserService.BLLUserToDALUser(user2));
                await chatRepository.AddAsync(dalChat);
                return new Result<ChatModel>(false, "Chat created seccessfully");
            }

            return new Result<ChatModel>(true, "Chat creation failed");
        }

        //public async Task DeleteChat(Chat chat)
        //{
        //    await chatRepository.Delete(chat);
        //    await chatRepository.SaveAsync();
        //}

        public async Task DeleteChatById(Guid chatId)
        {
            await chatRepository.DeleteAsync(chatId);
            await chatRepository.SaveAsync();
        }

        public async Task<ICollection<MessageModel>> GetMessages(Guid chatId)
        {
            var chat = await chatRepository.GetOneAsync(c => c.Id == chatId);
            var messages = chat.Messages;
            var bllMessages = new List<MessageModel>();
            foreach (var message in messages)
            {
                var bllMessage = MessageService.DalMessageToBll(message);
                if (bllMessage != null)
                    bllMessages.Add(bllMessage);
            }

            return bllMessages;
        }

        public static ChatModel? DalChatToBll(Chat chat)
        {
            if(chat != null)
            {
                return new ChatModel() { Id = chat.Id, Name = chat.Name, Description = chat.Description };
            }

            return null;
        }

        public static Chat? BllChatToDal(ChatModel chat)
        {
            if (chat != null)
            {
                return new Chat() { Id = chat.Id, Name = chat.Name, Description = chat.Description };
            }

            return null;
        }
    }
}
