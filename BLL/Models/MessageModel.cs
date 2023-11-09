using DAL.Models;

namespace BLL.Models
{
    public class MessageModel
    {
        public Guid Id { get; }

        public string? Text { get; set; }

        public UserModel Sender { get; set; } = null!;

        public ChatModel Chat { get; set; } = null!;

        //TODO relation with document
    }
}
