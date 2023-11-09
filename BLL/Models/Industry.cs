using DAL.Models;

namespace BLL.Models
{
    public class Industry
    {
        public Guid Id { get; }

        public Industries Name { get; set; } = Industries.None ;
    }
}
