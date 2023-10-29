using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Annotations
{
    public class ProjectAnnotation : BaseEntityAnnotation<Project>
    {
        public ProjectAnnotation(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void Annotate()
        {
            ModelBuilder.HasKey(p => p.Id);
            ModelBuilder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
            ModelBuilder.Property(p => p.Name).IsRequired();
            ModelBuilder.Property(p => p.Description);
            ModelBuilder.Property(p => p.CreatedDate).ValueGeneratedOnAdd();
            ModelBuilder.Property(p => p.ModifiedDate);
            ModelBuilder.HasMany(p => p.Users).WithMany(u => u.Projects);
            
        }
    }
}
