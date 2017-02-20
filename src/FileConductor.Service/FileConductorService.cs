using System;
using System.IO;
using System.Reflection;
using System.Timers;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.Helpers;
using FileConductor.Protocols;
using FileConductor.Service.Properties;
using NLog;

namespace FileConductor.Service
{
    public class FileConductorService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public bool Start()
        {
            Logger.Trace(Resources.InfoServiceInitializationStarted);
            Logger.Trace(Resources.InfoConfigFileReadingStarted);

            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();
            var configurationData = deserializer.XmlData;

            FileConductor fileConductor = new FileConductor();
            fileConductor.Initialize(configurationData);

            Logger.Trace(Resources.InfoConfigFileReadingFinished);

            Logger.Trace(Resources.InfoServiceInitializationFinished);
            return true;
        }

        public bool Stop()
        {
            Logger.Trace(Resources.InfoServiceStopped);
            // Service's disposing logic
            // Executed when the service is stopped/closed
            return true;
        }

        public static string GetExecutionAssembly()
        {
            return Assembly.GetExecutingAssembly().Location;
        }
    }
}