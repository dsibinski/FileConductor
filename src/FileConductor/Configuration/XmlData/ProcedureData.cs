using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class ProcedureData : IConfigurationElement
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("host")]
        public string Host { get; set; }

        [XmlElement("user")]
        public string User { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("databaseName")]
        public string DatabaseName { get; set; }
    }
}
