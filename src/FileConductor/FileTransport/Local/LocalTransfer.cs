using System.Collections.Generic;
using System.IO;
using FileConductor.Attributes;

namespace FileConductor.FileTransport.LocalFileTransport
{
    [FileTransferType]
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

        public void Send(TargetTransformData targetData, IList<string> files)
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