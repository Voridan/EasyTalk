namespace DAL.Data
{
    public static class EnvUtils
    {

        public static string GetConnectionString() 
        {
            var dbUserName = Environment.GetEnvironmentVariable("PostgresUserName");
            var dbPassword = Environment.GetEnvironmentVariable("PostgresPassword");

            return $"Host=localhost;Port=5432;Username=postgres;Password=87vordan10;Database=EasyTalk";
        }
    }
}
