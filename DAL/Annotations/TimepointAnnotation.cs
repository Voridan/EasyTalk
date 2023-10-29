using DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL.Annotations
{
    public class TimepointAnnotation : BaseEntityAnnotation<Timepoint>
    {
        public TimepointAnnotation(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void Annotate()
        {
            ModelBuilder.HasKey(t => t.Id);
            ModelBuilder.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            ModelBuilder.Property(t => t.CreatedDate)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("created_date");
            ModelBuilder.Property(t => t.ModifiedDate).HasColumnName("modified_date");
        }
    }
}
