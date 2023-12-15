using DAL.Models;

namespace BLL.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }

        public string? Text { get; set; }

        public Guid SenderId { get; set; }

        public Guid ChatId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        //TODO relation with document
    }
}
