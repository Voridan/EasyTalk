
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
            ModelBuilder.HasOne(u => u.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.FromId);
            ModelBuilder.HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);
            ModelBuilder.HasOne(m => m.Document).WithOne(d => d.Message);
            ModelBuilder.Property(m => m.CreatedDate).ValueGeneratedOnAdd();
            ModelBuilder.Property(m => m.ModifiedDate);
        }
    }
}
