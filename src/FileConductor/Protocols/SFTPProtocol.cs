﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration.XmlData;
using Renci.SshNet;

namespace FileConductor.Protocols
{
    public class SFTPProtocol : IProtocol
    {
        //public override void Execute()
        //{
        //    TryToMove();
        //    // some test change
        //}

        //private void TryToMove()
        //{
        //    var sourcePath = Properties.SourceTarget.Path;
        //    var destinyPath = Properties.DestinationTarget.Path;
        //    var regex = Properties.Regex;

        //    // SFTP data
        //    //TODO: protocol should not be executing any tasks
        //    //TODO: rething / redesign the concept of SFTP
        //   // var user = Properties.Ser

        //    using (var sftp = new SftpClient("host", "username", "password"))
        //    {
        //        sftp.Connect();
        //        //var file = sftp.

        //    }
        //}

        public void Execute(TargetData sourceData, TargetData destinationData, string regex)
        {
            throw new NotImplementedException();
        }
    }
}
