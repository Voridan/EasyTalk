namespace DAL.Data
{
    public static class EnvUtils
    {

        public static string GetConnectionString(bool local=true) 
        {
            var dbUserName = Environment.GetEnvironmentVariable("PostgresUserName");
            var dbPassword = Environment.GetEnvironmentVariable("PostgresPassword");
            
            if (local )
            {
                return $"Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=EasyTalk";
            }
          
            return ConnStr.Get();
        }
    }
}
