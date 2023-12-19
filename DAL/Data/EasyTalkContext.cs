using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class EasyTalkContext : DbContext
    {
        public EasyTalkContext(): base() { }

        public EasyTalkContext(DbContextOptions options, bool isTesting = false) : base(options) 
        {
            if (!isTesting)
            {
                Database.Migrate();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { 
                optionsBuilder.UseNpgsql(EnvUtils.GetConnectionString(false));
            }
        }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Industry> Industries { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Timepoint> Timepoints { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
