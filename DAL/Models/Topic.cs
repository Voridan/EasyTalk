﻿
namespace DAL.Models
{
    public class Topic : BaseEntity
    {
        public string Name { get; set; } = null!;

        public Guid? ProjectId { get; set; }
        
        public Project? Project { get; set; }

        public ICollection<Timepoint> Timepoints { get; } = null!;
    }
}
