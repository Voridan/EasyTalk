using DAL.Data;
using DAL.Models;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(EasyTalkContext _context) : base(_context)
        {
        }
    }
}
