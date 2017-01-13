using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileConductor;
using FileConductor.Helpers;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = GetFileList();

            string myRegex = "*.csv";
            Regex pattern = new Regex("^" + myRegex.Replace("*", ".*") + "$");
           

          /*  List<string> result = files.Where(x=>pattern.IsMatch(x)).ToList();*/

            
         Receive(new TargetTransformData("ftp://localhost/", "", "", ""),ProxyFile.ProxyPath,"*csv");
        }
        public static List<string> Receive(TargetTransformData sourceData, string targetPath, string regex)
        {

            var allFiles = GetFileList();

            string myRegex = "*elo.csv";
            Regex pattern = new Regex("^" + myRegex.Replace("*", ".*") + "$");

            List<string> files = allFiles.Where(x => pattern.IsMatch(x)).ToList();

            string ip = sourceData.IpAddress;

            List<string> result = new List<string>();
            foreach (var file in files)
            {
                var pathName = ip + file;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(pathName);

                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential();

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                using (StreamWriter destination = new StreamWriter(@"C:\Destiny\"+file))
                {
                    destination.Write(reader.ReadToEnd());
                    destination.Flush();
                }

                request = (FtpWebRequest)WebRequest.Create(pathName);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential();
                using (var response = (FtpWebResponse)request.GetResponse()) ;

            }
            return result;
        }

        public static string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://localhost/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential();
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch(Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }

    }
}
