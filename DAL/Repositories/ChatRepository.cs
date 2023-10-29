using DAL.Data;
using DAL.Models;

namespace DAL.Repositories
{
    public class ChatRepository : GenericRepository<Chat>
    {
        public ChatRepository(EasyTalkContext _context) : base(_context)
        {
        }
    }
}
