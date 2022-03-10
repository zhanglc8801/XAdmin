using SqlSugar;
using System;
using System.Collections.Generic;
using XAdmin.Common;

namespace XAdmin.SqlSugar
{
    public class SugarContext
    {
        private static SqlSugarClient db;

        /// <summary>
        /// 获取SQL Server实例
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetMSSqlInstance()
        {
            db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConfigurationManager.MSSQLConn,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType=InitKeyType.SystemTable,
                MoreSettings = new ConnMoreSettings
                {
                    IsAutoRemoveDataCache = true,
                    IsWithNoLockQuery = true
                }
            });
            return db;
        }

        /// <summary>
        /// 获取MySQL实例
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetMySqlInstance()
        {
            db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConfigurationManager.MySQLConn,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                MoreSettings = new ConnMoreSettings
                {
                    IsAutoRemoveDataCache = true,
                    IsWithNoLockQuery = true
                }
            });
            return db;
        }

        /// <summary>
        /// 获取SQLite实例
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetSQLiteInstance()
        {
            //string dbPath = Environment.CurrentDirectory + "\\Test.db3";
            //string connStr = "Data Source=" + dbPath + ";Pooling = true;FaillfMissing = false";
            db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConfigurationManager.SQLiteConn,
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                MoreSettings = new ConnMoreSettings
                {
                    IsAutoRemoveDataCache = true,
                    IsWithNoLockQuery = true
                }
            });
            return db;
        }

        public List<T> GetAll<T>()
        {
            List<T> result = new List<T>();
            return result;
        }

    }
}
