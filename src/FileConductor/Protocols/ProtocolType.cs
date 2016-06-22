using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileConductor.Protocols
{
    public enum ProtocolType
    {
        [XmlEnum(Name = "Local")]
        Local,

        [XmlEnum(Name = "FTP")]
        Ftp,

        [XmlEnum(Name = "SFTP")]
        Sftp
    }
}
