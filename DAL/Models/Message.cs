using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{

    // add anotations for foreign keys
    public class Message : BaseEntity
    {
        public string? Text { get; set; }
        
        public Guid FromId { get; set; }
        
        public Guid ToId { get; set; }
        
        public Person Sender { get; set; } = null!;
        
        public Person Receiver { get; set; } = null!;
        
        public Guid ChatId { get; set; }
        
        public Chat Chat { get; set; } = null!;
    }
}
