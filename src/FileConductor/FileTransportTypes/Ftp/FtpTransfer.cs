using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using FileConductor.Attributes;
using FileConductor.Operations;

namespace FileConductor.FileTransport.Ftp
{
    /// <summary>
    /// FTP transferable provider
    /// </summary>
    [FileTransferType]
    public class FtpTransfer : ITransfer
    {
        /// <summary>
        /// Used for getting files from sourceData into targetPath
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="targetPath"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public string Name => "FTP";

        public List<string> Receive(TargetTransformData sourceData, string targetPath, string regex)
        {
            var allFiles = GetFileList(sourceData);

            WildcardPattern wildCard = new WildcardPattern(regex);

            List<string> files = allFiles.Where(x => wildCard.IsMatch(x)).ToList();

            List<string> result = new List<string>();
            foreach (var file in files)
            {
                var pathName = sourceData.IpAddress + sourceData.Path + file;
                FtpWebRequest request = (FtpWebRequest) WebRequest.Create(pathName);

                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential();
                string proxyFile = targetPath + file;
                if (File.Exists(proxyFile)) throw new Exception(String.Format("file <{0}> already exists", proxyFile));

                using (FtpWebResponse response = (FtpWebResponse) request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                using (StreamWriter destination = new StreamWriter(proxyFile))
                {
                    destination.Write(reader.ReadToEnd());
                    destination.Flush();
                }

                request = (FtpWebRequest) WebRequest.Create(pathName);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential();
                using ((FtpWebResponse) request.GetResponse()){}
                result.Add(proxyFile);
            }
            return result;
        }


        public void Send(TargetTransformData targetData, IList<string> files)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var pathName = targetData.IpAddress;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathName + '/' +fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential();

                FileInfo fi = new FileInfo(file);
                request.ContentLength = fi.Length;
                byte[] buffer = new byte[4097];
                int bytes = 0;
                int total_bytes = (int)fi.Length;
                using (FileStream fs = fi.OpenRead())
                using (Stream rs = request.GetRequestStream())
                {
                    while (total_bytes > 0)
                    {
                        bytes = fs.Read(buffer, 0, buffer.Length);
                        rs.Write(buffer, 0, bytes);
                        total_bytes = total_bytes - bytes;
                    }
                }

                using (FtpWebResponse uploadResponse = (FtpWebResponse)request.GetResponse())
                {
                    var response = uploadResponse.StatusDescription;
                }
                File.Delete(file);
            }
        }

        private List<string> GetFileList(TargetTransformData sourceData)
        {
            List<string> result = new List<string>();
            WebResponse response = null;
            StreamReader reader = null;

            var url = sourceData.IpAddress + sourceData.Path;
            FtpWebRequest reqFTP = (FtpWebRequest) FtpWebRequest.Create(new Uri(url));
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential();
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            reqFTP.Proxy = null;
            reqFTP.KeepAlive = false;
            reqFTP.UsePassive = false;
            response = reqFTP.GetResponse();
            using (reader = new StreamReader(response.GetResponseStream()))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Add(line);
                    line = reader.ReadLine();
                }
            }
            return result;
        }
    }
}