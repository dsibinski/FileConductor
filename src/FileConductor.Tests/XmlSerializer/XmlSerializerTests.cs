using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using NUnit.Framework;

namespace FileConductor.Tests.XmlSerializer
{
    [TestFixture]
    public class XmlSerializerTests
    {

        [Test]
        public void ShouldContainProperXmlElements()
        {
            // given
            var xmlFilePath = "TestConfig.xml";

            var deserializer = new XmlFileDeserializer<ConfigurationData>(xmlFilePath);

            // when
            deserializer.Deserialize();

            // then
        }
    }
}
