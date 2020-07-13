using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;

using FilesStorage.DAL.EF.EF;

namespace FilesStorage.DAL.EF.EFConfig
{
    public class AppDbConfig : DbConfiguration
    {
        private static string _sqlQueriesLogPath = AppDomain.CurrentDomain.BaseDirectory 
            + @"..\Logs\sql_queries_journal.txt";

        protected internal AppDbConfig()
        {
            AddInterceptor(new DatabaseLogger(_sqlQueriesLogPath, true));
            SetDefaultConnectionFactory(new SqlConnectionFactory());
            SetDatabaseInitializer(new FilesStorageDbInitializer());
            SetProviderServices("System.Data.SqlClient", System.Data.Entity.SqlServer.SqlProviderServices.Instance);
        }
    }
}
