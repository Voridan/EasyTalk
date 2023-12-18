using BLL.Models;
using BLL.Utils;
using DAL.Models;
using DAL.Repositories;

namespace BLL.Services.Implementations
{
    public class ChatService
    {
        private readonly ChatRepository chatRepository;
        private readonly UserRepository userRepository;
        private readonly MessageRepository msgRepository;

        public ChatService(ChatRepository _repo, UserRepository _userRepo, MessageRepository _msgRepo)
        {
            chatRepository = _repo;
            userRepository = _userRepo;
            msgRepository = _msgRepo;
        }

        public async Task<IEnumerable<ChatModel>?> GetChatsForUser(Guid userId)
        {
            var chats = await chatRepository.GetAsync(c => c.Users.Any(u => u.Id == userId));
            var tempchat = await userRepository.GetByIdAsync(userId);
            var bllChats = new List<ChatModel>();
            foreach (var chat in chats)
            {
                bllChats.Add(DalChatToBll(chat)!);
            }

            return bllChats;
        }

        public async Task<ICollection<MessageModel>> GetMessages(Guid chatId)
        {
            var chat = await chatRepository.GetChatWithMessages(chatId);
            var bllMessages = new List<MessageModel>();
            if (chat != null)
            {
                foreach (var message in chat.Messages)
                {
                    bllMessages.Add(MessageService.DalMessageToBll(message)!);
                }
            }

            return bllMessages;
        }

        public async Task<IEnumerable<UserModel>?> GetUsersForChat(Guid chatId)
        {
            var chat = await chatRepository.GetChatWithUsers(chatId);

            List<UserModel> users = new ();
            foreach (var item in chat!.Users)
            {
                users.Add(UserService.DALUserToBLLUser(item)!);
            }

            return users;
        }

        public async Task<ChatModel?> GetChat(Guid chatId)
        {
            var chat = await chatRepository.GetByIdAsync(chatId);

            return DalChatToBll(chat!);
        }

        public async Task<Result<ChatModel>> CreateChat(ChatModel chat, Guid user1Id, Guid user2Id)
        {
            try
            {
                var user1 = await userRepository.GetByIdAsync(user1Id);
                var user2 = await userRepository.GetByIdAsync(user2Id);
                List<User> users = new List<User>() { user1!, user2! };
                var newChat = new Chat() { Name = chat.Name, Description = chat.Description, CreatedDate = DateTime.UtcNow, Users = users, Messages = new List<Message>() };
                await chatRepository.AddAsync(newChat);
                return new Result<ChatModel>(false, "Chat created seccessfully");
            }
            catch (Exception)
            {
                return new Result<ChatModel>(true, "Chat creation failed");
            }
        }

        public async Task<Result<ChatModel>> UpdateChat(ChatModel chat)
        {
            var chatToUpdate = await chatRepository.GetByIdAsync(chat.Id);

            if (chatToUpdate == null)
            {
                return new Result<ChatModel>(true, "Chat does not exists.");
            }

            chatToUpdate.Name = chat.Name;
            chatToUpdate.Description = chat.Description;
            await chatRepository.Update(chatToUpdate);

            return new Result<ChatModel>(false, "Updated successfuly.", chat);
        }

        public async Task DeleteChatById(Guid chatId)
        {
            await chatRepository.DeleteAsync(chatId);
            await chatRepository.SaveAsync();
        }

        public async Task SaveMessages(Guid chatId, Guid sendrId, ICollection<MessageModel> messages)
        {
            var chat = await chatRepository.GetByIdAsync(chatId);
            var sender = chat!.Users.Where(u => u.Id == sendrId).FirstOrDefault();
            var dalMessages = new List<Message>();

            foreach (var message in messages)
            {
                var dalMessage = MessageService.BllMessageToDal(message, sender!);
                if (dalMessage != null)
                {
                    dalMessages.Add(dalMessage);
                }
            }

            await msgRepository.SaveMessages(dalMessages);

            if (chat.Messages == null)
            {
                chat.Messages = dalMessages;
            }
            else
            {
                dalMessages.ForEach(msg => chat.Messages.Add(msg));
            }

            await chatRepository.Update(chat);
        }

        public static ChatModel? DalChatToBll(Chat chat)
        {
            if (chat != null)
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
