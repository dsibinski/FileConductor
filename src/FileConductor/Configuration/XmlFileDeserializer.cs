using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileConductor.Configuration
{
    public class XmlFileDeserializer<T>  where T : class
    {
        public T XmlData { get; set; }

        private XmlSerializer Serializer => new XmlSerializer(typeof(T));

        private string FilePath { get; set; }

        public XmlFileDeserializer(string xmlFilePath)
        {
            FilePath = xmlFilePath;
        }

        public void Deserialize()
        {
            using (var reader = new StreamReader(FilePath))
            {
                XmlData = (T)Serializer.Deserialize(reader);
                reader.Close();
            }
        }
        
    }
}
