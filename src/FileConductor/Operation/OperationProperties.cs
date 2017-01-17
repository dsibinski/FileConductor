﻿using System.Diagnostics;
using System.Timers;
using FileConductor.Configuration.XmlData;

namespace FileConductor
{
    public class OperationProperties
    {
        // public WatcherData Data;
        //  public ServerData DestinationServer;
        public TargetTransformData DestinationTarget;
        public OperationSchedule NotificationSettings;
        public string Regex;
        //public ServerData SourceServer;
        public TargetTransformData SourceTarget;

        public void AssignOperationHandler(ElapsedEventHandler afterOperationElapsedHandler)
        {
            NotificationSettings.OnElapsed += afterOperationElapsedHandler;
        }
    }
}