using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor
{
    class FileConductorService
    {
        public bool Start()
        {
            // Service's initialization logic
            // Executed when the service starts
            return true;
        }

        public bool Stop()
        {
            // Service's disposing logic
            // Executed when the service is stopped/closed
            return true;
        }
    }
}
