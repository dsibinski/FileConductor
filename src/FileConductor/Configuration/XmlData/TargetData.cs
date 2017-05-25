using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class TargetData
    {

        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("server")]
        public string Server { get; set; }

        [XmlElement("path")]
        public string Path { get; set; }

    }
}
