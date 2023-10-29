using DAL.Data;
using DAL.Models;

namespace DAL.Repositories
{
    
    public class MessageRepository : GenericRepository<Message>
    {
        public MessageRepository(EasyTalkContext _context) : base(_context)
        {
        }
    }
}
