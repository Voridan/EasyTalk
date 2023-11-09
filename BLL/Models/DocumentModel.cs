using System.Reflection.Metadata;

namespace BLL.Models
{
    public class DocumentModel
    {
        public Guid Id { get; }
        
        public string Name { get; set; } = null!;
        
        FileExtension Extensions { get; set; }

        //TODO to resolve how document should be represented
    }
}
