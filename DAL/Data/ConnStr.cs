using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class ConnStr
    {
        public static string Get()
        {
            var uriString = ReadConnStrFromFileAsync("CONN_STR");
            var uri = new Uri(uriString);
            var db = uri.AbsolutePath.Trim('/');
            var user = uri.UserInfo.Split(':')[0];
            var passwd = uri.UserInfo.Split(':')[1];
            var port = uri.Port > 0 ? uri.Port : 5432;
            var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                uri.Host, db, user, passwd, port);
            return connStr;
        }

        private static string ReadConnStrFromFileAsync(string key)
        {
            string[] lines = File.ReadAllLines(@"../../../../DAL/Data/secureData.txt");

            string connstr = "";
            foreach (var line in lines)
            {
                if (line.StartsWith(key.Trim()))
                {
                    connstr = line.Split("=")[1];
                    break;
                }
            }

            return connstr;
        }
    }

}
