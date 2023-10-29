
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Annotations
{
    public class MessageAnnotation : BaseEntityAnnotation<Message>
    {
        public MessageAnnotation(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void Annotate()
        {
            ModelBuilder.HasKey(m => m.Id);
            ModelBuilder.Property(m => m.Id).ValueGeneratedOnAdd().IsRequired();
            ModelBuilder.Property(m => m.Text);
            ModelBuilder.Property(m => m.CreatedDate).ValueGeneratedOnAdd().HasColumnName("created_date");
            ModelBuilder.Property(m => m.ModifiedDate).HasColumnName("modified_date");
            ModelBuilder.HasOne(m => m.Document).WithOne(d => d.Message);
        }
    }
}
