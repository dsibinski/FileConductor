using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;

namespace FileConductor
{
    public interface IFileConductor
    {
        void Start(ConfigurationData configurationData);
        void Stop();
    }
}