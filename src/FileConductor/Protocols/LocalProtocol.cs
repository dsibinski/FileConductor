using System;
using System.IO;
using FileConductor.Configuration.XmlData;

namespace FileConductor.Protocols
{
    public class LocalProtocol : IProtocol
    {
        public void Execute(TargetData sourceData, TargetData destinationData, string regex)
        {
            string sourcePath = sourceData.Path;
            string destinyPath = destinationData.Path;

            string[] files = Directory.GetFiles(sourcePath, regex);

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                File.Move(file, destinyPath + fileName);
            }
        }
    }
}