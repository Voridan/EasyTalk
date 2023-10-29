
namespace DAL.Models
{
    public class Timepoint : BaseEntity
    {
        public Guid TopicId { get; set; }

        public Topic Topic { get; set; } = null!;
    }
}
