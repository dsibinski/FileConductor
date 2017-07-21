using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.LoggingService;
using Ninject;

namespace FileConductor.Operations.ProcedureExecution
{
    public class ProcedureExecutionService : IProcedureExecutionService
    {
        [Inject]
        public ILoggingService LoggingService { get; set; }
        public void ExecuteProcedure(string host,string databaseName, string user, string password, string procedureName)
        {
            string connectionString = string.Format("Data Source={0};Initial Catalog = {1};User ID={2};Password={3};", host, databaseName, user, password);

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(procedureName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error during SQL procedure execution",ex);
                }
            }
        }

        public void ExecuteProcedure(IOperation operation)
        {
            string host = operation.Properties.ProcedureData.Host;
            string login = operation.Properties.ProcedureData.User;
            string password = operation.Properties.ProcedureData.Password;
            string procedureName = operation.Properties.ProcedureData.Name;
            string databaseName = operation.Properties.ProcedureData.DatabaseName;

            ExecuteProcedure(host,databaseName,login,password,procedureName);

        }
    }
}
