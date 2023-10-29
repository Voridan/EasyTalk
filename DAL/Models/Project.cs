
namespace DAL.Models
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = null!;
        
        public string? Description { get; set; }

        public ICollection<User> Users { get; set; } = null!;

        public ICollection<Topic> Topics { get; } = null!;

        public ICollection<Industry> Industries { get; } = null!;
    }
}
