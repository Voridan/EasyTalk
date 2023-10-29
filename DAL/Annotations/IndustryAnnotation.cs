using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Annotations
{
    public class IndustryAnnotation : BaseEntityAnnotation<Industry>
    {
        public IndustryAnnotation(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void Annotate()
        {
            ModelBuilder.HasKey(i => i.Id);
            ModelBuilder.Property(i => i.Id).ValueGeneratedOnAdd().IsRequired();
            ModelBuilder.Property(i => i.Name).IsRequired();
        }
    }
}
