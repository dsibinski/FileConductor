using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.Operations
{
    public class ProcedureData
    {
        public ProcedureData(string host, string login, string password, string procedureName)
        {
            Host = host;
            Login = login;
            Password = password;
            ProcedureName = procedureName;
        }

        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ProcedureName { get; set; }
    }
}