using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Annotations
{
    public class DocumentAnnotation : BaseEntityAnnotation<Document>
    {
        public DocumentAnnotation(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void Annotate()
        {
            ModelBuilder.HasKey(d => d.Id);
            ModelBuilder.Property(d => d.Id).ValueGeneratedOnAdd().IsRequired();
            ModelBuilder.Property(d => d.Name).IsRequired();
            ModelBuilder.Property(d => d.StoragePath).IsRequired();
            ModelBuilder.Property(d => d.CreatedDate).ValueGeneratedOnAdd().IsRequired().HasColumnName("created_date");
            ModelBuilder.Property(d => d.ModifiedDate).HasColumnName("modified_date");
        }

    }
}
