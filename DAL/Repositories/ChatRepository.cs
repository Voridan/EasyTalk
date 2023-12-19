using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(EasyTalkContext _context) : base(_context)
        {
        }

        public async Task<Chat?> GetChatWithUsers(Guid chatId)
        {
            var chats = await _table
                .Include(c => c.Users)
                .ToListAsync();

            return chats.FirstOrDefault(c => c.Id == chatId);
        }

        public async Task<Chat?> GetChatWithMessages(Guid chatId)
        {
            var chats = await _table
                .Include(c => c.Messages)
                .ToListAsync();

            return chats.FirstOrDefault(c => c.Id == chatId);
        }
    }
}
