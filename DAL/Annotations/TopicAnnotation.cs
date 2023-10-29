using DAL.Models;
using Microsoft.EntityFrameworkCore;



namespace DAL.Annotations
{
    public class TopicAnnotation : BaseEntityAnnotation<Topic>
    {
        public TopicAnnotation(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void Annotate()
        {
            ModelBuilder.HasKey(t => t.Id);
            ModelBuilder.Property(t => t.Id).ValueGeneratedOnAdd().IsRequired();
            ModelBuilder.Property(t => t.Name).IsRequired();
            ModelBuilder.Property(t => t.CreatedDate)
                .ValueGeneratedOnAdd().IsRequired().HasColumnName("created_date");
            ModelBuilder.Property(t => t.ModifiedDate).HasColumnName("modified_date");
            ModelBuilder.HasMany(t => t.Timepoints).WithOne(t => t.Topic).HasForeignKey(t => t.TopicId);
        }
    }
}
