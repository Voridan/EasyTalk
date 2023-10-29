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
            ModelBuilder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            ModelBuilder.Property(p => p.Name)
                .IsRequired();
            ModelBuilder.Property(p => p.Description);
            ModelBuilder.Property(p => p.CreatedDate)
                .ValueGeneratedOnAdd()
                .HasColumnName("created_date");
            ModelBuilder.Property(p => p.ModifiedDate)
                .HasColumnName("modified_date");
            ModelBuilder.HasMany(p => p.Users).WithMany(u => u.Projects);
            ModelBuilder.HasMany(p => p.Topics)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);
            ModelBuilder.HasMany(p => p.Industries).WithMany(u => u.Projects);
        }
    }
}
