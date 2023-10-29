using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Annotations
{
    public class ChatAnnotation : BaseEntityAnnotation<Chat>
    {
        public ChatAnnotation(ModelBuilder modelBuilder) : base(modelBuilder) { }

        public override void Annotate()
        {
            ModelBuilder.HasKey(c => c.Id);
            ModelBuilder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
            ModelBuilder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);
            ModelBuilder.Property(c => c.Description);
            ModelBuilder.Property(c => c.CreatedDate).ValueGeneratedOnAdd();
            ModelBuilder.Property(c => c.ModifiedDate);
            ModelBuilder.HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId);
            ModelBuilder.HasMany(c => c.Users).WithMany(u => u.Chats);
            
        }
    }
}
