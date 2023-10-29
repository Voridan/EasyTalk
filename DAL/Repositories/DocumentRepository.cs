using DAL.Data;
using DAL.Models;

namespace DAL.Repositories
{
    public class DocumentRepository : GenericRepository<Document>
    {
        public DocumentRepository(EasyTalkContext _context) : base(_context)
        {
        }
    }
}
