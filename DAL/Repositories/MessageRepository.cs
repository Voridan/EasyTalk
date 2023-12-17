using DAL.Data;
using DAL.Models;

namespace DAL.Repositories
{
    
    public class MessageRepository : GenericRepository<Message>
    {
        public MessageRepository(EasyTalkContext _context) : base(_context)
        {
        }

        public async Task SaveMessages(ICollection<Message> msgs)
        {
            foreach (var msg in msgs)
            {
                _table.Add(msg);
            }

            await SaveAsync();
        }
    }
}
