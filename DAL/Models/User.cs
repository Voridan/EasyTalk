
namespace DAL.Models
{
    public class User : BaseEntity
    {
        public string? NickName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public UserRole Role { get; set; }
       
        public byte[]? Photo { get; set; }
        
        public ICollection<Industry> Industries { get; set; } = null!;
        
        public ICollection<Message> Messages { get; } = null!;

        public ICollection<Chat> Chats { get; set; } = null!;

        public ICollection<Project> Projects { get; } = null!;
    }
}