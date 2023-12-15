using DAL.Models;
using Newtonsoft.Json;

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
        
        public static string Serialize(MessageModel message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public static MessageModel? Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<MessageModel>(json);
        }
    }
}
