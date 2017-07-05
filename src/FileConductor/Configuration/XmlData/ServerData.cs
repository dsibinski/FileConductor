using System;
using System.Xml.Serialization;
using FileConductor.Protocols;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class ServerData
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("protocol")]
        public string Protocol { get; set; }

        [XmlElement("ip")]
        public string Ip  { get; set; }

        [XmlElement("user")]
        public string User { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }

        [XmlElement("port", IsNullable = true)]
        public string Port { get; set; }


    }
}
