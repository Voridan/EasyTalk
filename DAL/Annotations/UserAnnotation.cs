
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Annotations
{
    public class UserAnnotation : BaseEntityAnnotation<User>
    {
        public UserAnnotation(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void Annotate()
        {
            ModelBuilder.HasKey(u => u.Id);
            ModelBuilder.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();
            ModelBuilder.Property(u => u.FirstName).IsRequired().HasColumnName("first_name");
            ModelBuilder.Property(u => u.LastName).IsRequired().HasColumnName("last_name");
            ModelBuilder.Property(u => u.NickName).IsRequired();
            ModelBuilder.Property(u => u.Email).IsRequired();
            ModelBuilder.Property(u => u.Role).IsRequired();
            ModelBuilder.Property(u => u.Password).IsRequired();
            ModelBuilder.Property(u => u.Photo);
            ModelBuilder.Property(u => u.CreatedDate).ValueGeneratedOnAdd().IsRequired().HasColumnName("created_date");
            ModelBuilder.Property(u => u.ModifiedDate).HasColumnName("modified_date");
            ModelBuilder.HasMany(u => u.Industries).WithMany(i => i.Users);
            ModelBuilder.HasMany(u => u.Messages).WithOne(m => m.Sender);
            ModelBuilder.HasMany(u => u.Chats).WithMany(c => c.Users);
            ModelBuilder.ToTable("Person");
        }
    }
}
