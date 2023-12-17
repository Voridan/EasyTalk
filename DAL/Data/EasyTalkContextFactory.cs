using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace DAL.Data
{
    public class EasyTalkContextFactory : IDesignTimeDbContextFactory<EasyTalkContext>
    {
        public EasyTalkContext CreateDbContext(string[] args)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<EasyTalkContext>();
            optionsBuilder.UseNpgsql(EnvUtils.GetConnectionString(),
                    options => options.EnableRetryOnFailure());

            return new EasyTalkContext(optionsBuilder.Options);
        }
    }
}
