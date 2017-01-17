using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Helpers;
using FileConductor.Protocols;

namespace FileConductor.FileTransport.Implementations
{
    public class LocalTransfer : ITransfer
    {
        public string Name => "Local";

        public List<string> Receive(TargetTransformData sourceData, string targetPath, string regex)
        {
            string sourcePath = sourceData.Path;

            string[] files = Directory.GetFiles(sourcePath, regex);

            List<string> movedFiles = new List<string>();
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string combinedName = Path.Combine(targetPath, fileName);
                File.Move(file, combinedName);

                movedFiles.Add(combinedName);
            }
            return movedFiles;
        }

        public void Send(TargetTransformData targetData, List<string> files)
        {
            string destinyPath = targetData.Path;

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string combinedName = Path.Combine(destinyPath, fileName);
                File.Move(file, combinedName);
            }
        }
    }
}