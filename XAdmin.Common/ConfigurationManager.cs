using System.IO;
using Microsoft.Extensions.Configuration;

namespace XAdmin.Common
{
    public class ConfigurationManager
    {
        public readonly static IConfiguration Configuration;
        static ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

        /// <summary>
        /// 获得配置文件的对象值
        /// </summary>
        /// <param name="key">json某个对象</param>
        /// <returns></returns>
        public static string GetJson(string key)
        {
            return Configuration.GetSection(key).Value;
        }

        /// <summary>
        /// 获得配置文件的对象值
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public static string GetJson(string key1, string key2)
        {
            return Configuration.GetSection(key1).GetSection(key2).Value;
        }

        /// <summary>
        /// SQLServer连接字符串
        /// </summary>
        public static string MSSQLConn
        {
            get {return Configuration.GetConnectionString("MSSQL"); }
        }

        /// <summary>
        /// MySQL连接字符串
        /// </summary>
        public static string MySQLConn
        {
            get { return Configuration.GetConnectionString("MySQL"); }
        }

        /// <summary>
        /// SQLite连接字符串
        /// </summary>
        public static string SQLiteConn
        {
            get { return Configuration.GetConnectionString("SQLite"); }
        }
    }
}
