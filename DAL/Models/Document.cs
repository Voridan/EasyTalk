
namespace DAL.Models
{
    public class Document : BaseEntity
    {
        public string StoragePath { get; set; } = null!;

        public Message? Message { get; set; }
    }
}
