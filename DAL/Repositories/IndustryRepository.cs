using DAL.Data;
using DAL.Models;

namespace DAL.Repositories
{
    public class IndustryRepository : GenericRepository<Industry>
    {
        public IndustryRepository(EasyTalkContext _context) : base(_context)
        {
        }
    }
}
