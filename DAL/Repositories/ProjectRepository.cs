using DAL.Data;
using DAL.Models;

namespace DAL.Repositories
{
    public class ProjectRepository : GenericRepository<Project>
    {
        public ProjectRepository(EasyTalkContext _context) : base(_context)
        {
        }
    }
}
