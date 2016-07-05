using System.IO;
using System.Timers;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.Properties;
using FileConductor.Protocols;
using NLog;

namespace FileConductor
{
    internal class FileConductorService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public bool Start()
        {
            logger.Trace(Resources.InfoServiceInitializationStarted);
            logger.Trace(Resources.InfoConfigFileReadingStarted);

            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();

            logger.Trace(Resources.InfoConfigFileReadingFinished);


            var operationProp = new OperationProperties()
            {
                SchedulerTimer = new Timer(10000),
                DestinyPath = "C:/Destiny/",
                SourcePath = "C:/Source/",
                Regex = "*.csv"
            };

            var operation2Prop = new OperationProperties()
            {
                SchedulerTimer = new Timer(10000),
                DestinyPath = "C:/Source/",
                SourcePath = "C:/Destiny/",
                Regex = "*.csv"
            };

            var operation = new Operation(new LocalProtocol(), operationProp);
            var operation2 = new Operation(new LocalProtocol(), operation2Prop);
            var operatio = new OperationScheduler();
            operatio.AssignOperation(operation);
            operatio.AssignOperation(operation2);

            logger.Trace(Resources.InfoServiceInitializationFinished);
            return true;
        }

        public bool Stop()
        {
            logger.Trace(Resources.InfoServiceStopped);
            // Service's disposing logic
            // Executed when the service is stopped/closed
            return true;
        }
    }
}