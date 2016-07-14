using System;
using System.IO;

namespace FileConductor.Protocols
{
    public class LocalProtocol : Protocol
    {
       // public OperationProperties Properties { get; set; }

        public override void Execute()
        {
                TryToMove();
        }

        private void TryToMove()
        {
            string sourcePath = Properties.SourcePath;
            string destinyPath = Properties.DestinyPath;
            string regex = Properties.Regex;

            string[] files = Directory.GetFiles(sourcePath, regex);

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                File.Move(file, destinyPath+fileName);
            }
        }
    }
}