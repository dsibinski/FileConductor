using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FileConductor
{
    /// <summary>
    /// </summary>
    public class OperationProperties
    {
        public string SourcePath;
        public string DestinyPath;
        public string Regex;
        public Timer ShedulerTimer;  
    }
}