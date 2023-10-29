
namespace DAL.Models
{
    public class Industry 
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public ICollection<Project> Projects { get; } = null!;
        
        public ICollection<User> Users { get; } = null!;
    }
}
