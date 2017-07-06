using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class TargetData
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("serverId")]
        public int ServerId { get; set; }

        [XmlElement("path")]
        public string Path { get; set; }

    }
}