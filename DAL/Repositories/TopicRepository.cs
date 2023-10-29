using DAL.Data;
using DAL.Models;

namespace DAL.Repositories
{
    public class TopicRepository : GenericRepository<Topic>
    {
        public TopicRepository(EasyTalkContext _context) : base(_context)
        {
        }
    }
}
