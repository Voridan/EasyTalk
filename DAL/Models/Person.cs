
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    //[Index(nameof(NickName))]
    public class Person : BaseEntity
    {
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }

        public string? NickName { get; set; }

        public string? Email { get; set; }
        
        public UserRole Role { get; set; }
        
        public string? Password { get; set; }
        
        public byte[]? Photo { get; set; }
        
        public virtual Industry? Industry { get; set; }
        
        public virtual ICollection<Message>? Messages { get; }
    }
}