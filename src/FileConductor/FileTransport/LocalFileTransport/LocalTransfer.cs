using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Helpers;

namespace FileConductor.FileTransport.Implementations
{
    public class LocalTransfer : ITransfer
    {
        public List<string> Receive(TargetTransformData targetData, string regex)
        {
            string sourcePath = targetData.Path;
            string destinyPath = ProxyFile.ProxyPath;

            string[] files = Directory.GetFiles(sourcePath, regex);

            List<string> movedFiles = new List<string>();
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string combinedName = Path.Combine(destinyPath, fileName);
                File.Move(file, combinedName);

                movedFiles.Add(combinedName);
            }
            return movedFiles;
        }

        public void Send(TargetTransformData targetData, List<string> files, string regex)
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