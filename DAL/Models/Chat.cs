using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Chat : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
        
        public virtual ICollection<Message>? Messages { get; }

    }
}
