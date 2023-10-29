
namespace DAL.Models
{
    public class Message : BaseEntity
    {
        public string? Text { get; set; }
        
        public Guid FromId { get; set; }

        public User Sender { get; set; } = null!;
        
        public Guid ChatId { get; set; }
        
        public Chat Chat { get; set; } = null!;

        public Document? Document { get; }
    }
}
