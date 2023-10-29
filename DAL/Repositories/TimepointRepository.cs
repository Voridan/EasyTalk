using DAL.Data;
using DAL.Models;

namespace DAL.Repositories
{
    public class TimepointRepository : GenericRepository<Timepoint>
    {
        public TimepointRepository(EasyTalkContext _context) : base(_context)
        {
        }
    }
}
