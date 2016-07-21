using System;
using System.IO;
using System.Timers;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.Helpers;
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

            FileConductorInitializer initializer = new FileConductorInitializer();
            initializer.InitializeOperations();

            logger.Trace(Resources.InfoConfigFileReadingFinished);

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