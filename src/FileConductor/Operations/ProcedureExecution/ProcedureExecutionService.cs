using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.Operations.ProcedureExecution
{
    public class ProcedureExecutionService : IProcedureExecutionService
    {
        public void ExecuteProcedure(string host, string user, string password, string procedureName)
        {
            string connectionString = string.Format("Data Source={0};User ID={1};Password={2};", host, password, procedureName);

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(procedureName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {

                }
            }
        }

        public void ExecuteProcedure(IOperation operation)
        {
            string host = operation.Properties.ProcedureData.Host;
            string login = operation.Properties.ProcedureData.Login;
            string password = operation.Properties.ProcedureData.Password;
            string procedureName = operation.Properties.ProcedureData.ProcedureName;

            ExecuteProcedure(host,login,password,procedureName);

        }
    }
}
