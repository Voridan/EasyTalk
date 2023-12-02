using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(EasyTalkContext _context) : base(_context)
        {
        }
        public override async Task Update(User obj)
        {
           
            if (!_context.Set<User>().Local.Any(e => e.Id == obj.Id))
            {
                // If not tracked, attach it
                _table.Attach(obj);
            }
            else
            {
                // If already tracked, update its state to Modified
                _context.Entry(obj).State = EntityState.Modified;
            }

            // Save changes asynchronously
            await SaveAsync();
        }
    }
}
