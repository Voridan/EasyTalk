using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class EasyTalkContext : DbContext
    {
        public EasyTalkContext() : base() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("connection string");
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
