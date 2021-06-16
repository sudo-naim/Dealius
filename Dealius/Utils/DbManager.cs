using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealius.Utils
{
    public class DbManager
    {
        private const int SystemUserId = 100;
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DealiusConnectionString"]?.ConnectionString;

        public DbManager()
        {
        }

        public void DeleteAllData()
        {
            var sql = @"
                update [Company] set [DeletedDate] = GETUTCDATE(), [DeletedBy] = @userId;
                update [Deal] set [DeletedDate] = GETUTCDATE(), [DeletedBy] = @userId;";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { userId = SystemUserId });
            }
        }
    }
}
