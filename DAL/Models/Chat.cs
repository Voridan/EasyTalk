
namespace DAL.Models
{
    public class Chat : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Message> Messages { get; } = null!;

        public ICollection<User> Users { get; } = null!;
    }
}
