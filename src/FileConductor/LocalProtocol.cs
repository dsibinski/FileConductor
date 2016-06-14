using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor
{
    public class LocalProtocol : IProtocol
    {
        public OperationProperties Properties { get; set; }

        public void ExecuteProcess()
        {
            try
            {
                TryToMove();
            }
            catch (Exception ex)
            {
                //TODO: Here some loging mechanism 
                throw;
            }
        }

        private void TryToMove()
        {
            string sourcePath = Properties.SourcePath;
            string destinyPath = Properties.DestinyPath;
            string regex = Properties.Regex;

            string[] files = Directory.GetFiles(sourcePath, regex);

            foreach (var file in files)
            {
                File.Move(sourcePath + file, destinyPath);
            }
        }
    }
}