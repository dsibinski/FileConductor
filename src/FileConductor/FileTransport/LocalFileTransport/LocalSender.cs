using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Helpers;

namespace FileConductor.FileTransport.Implementations
{
    public class LocalSender : ISender
    {
        public void Transfer(TargetTransformData targetData, string regex)
        {

            string sourcePath = ProxyFile.ProxyPath; 
            string destinyPath = targetData.Path;

            string[] files = Directory.GetFiles(sourcePath, regex);

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                File.Move(file, destinyPath + fileName);
            }
        }
    }
}
