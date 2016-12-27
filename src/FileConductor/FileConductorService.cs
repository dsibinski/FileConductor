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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public bool Start()
        {

            Logger.Trace(Resources.InfoServiceInitializationStarted);
            Logger.Trace(Resources.InfoConfigFileReadingStarted);

            FileConductorInitializer initializer = new FileConductorInitializer();
            initializer.InitializeOperations();

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
    }
}