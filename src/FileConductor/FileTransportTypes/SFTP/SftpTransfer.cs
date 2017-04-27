using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using FileConductor.Attributes;
using FileConductor.Operations;
using Renci.SshNet;

namespace FileConductor.FileTransport.SFTP
{
    /// <summary>
    /// FTP transferable provider
    /// </summary>
    [FileTransferType]
    public class SftpTransfer : ITransfer
    {
        /// <summary>
        /// Used for getting files from sourceData into targetPath
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetPath"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public string Name => "SFTP";

        public List<string> Receive(TargetTransformData sourceData, string targetPath, string regex)
        {
            List<string> result = new List<string>();
            string host = sourceData.IpAddress;
            string userName = sourceData.Login;
            string password = sourceData.Password;
            string sourcePath = sourceData.Path;
            string localPath = targetPath;

            WildcardPattern wildCard = new WildcardPattern(regex);

            using (var sftp = new SftpClient(host, userName, password))
            {
                sftp.Connect();
                var files = sftp.ListDirectory(sourcePath);
                foreach (var file in files)
                {
                    string fileName = file.Name;
                    if (wildCard.IsMatch(fileName))
                    {
                        Stream file1 = File.OpenRead(localPath);
                        sftp.DownloadFile(file.FullName, file1);
                        result.Add(localPath + file.Name);
                    }
                }
            }
            return result;
        }


        public void Send(TargetTransformData targetData, IList<string> files)
        {
            string host = targetData.IpAddress;
            string userName = targetData.Login;
            string password = targetData.Password;

            using (var sftp = new SftpClient(host, userName, password))
            {
                sftp.Connect();
                foreach (var file in files)
                { 
                    using (var fileStream = new FileStream(file, FileMode.Open))
                    {
                        sftp.BufferSize = 4 * 1024; // bypass Payload error large files
                        sftp.UploadFile(fileStream, Path.GetFileName(file));
                    }
                    File.Delete(file);
                }
            }
        }

    }
}