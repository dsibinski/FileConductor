using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.Helpers
{
    public class ProxyFile
    {
        private static string _proxyPath;

        public static string ProxyPath
        {
            get
            {
                if (String.IsNullOrEmpty(_proxyPath))
                {
                    string assemblyPath = System.IO.Path.GetDirectoryName(
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                    string localPath = new Uri(assemblyPath).LocalPath;

                    var combined = Path.Combine(localPath, "Files");

                    System.IO.Directory.CreateDirectory(combined);

                    _proxyPath = combined;
                }

                return _proxyPath;
            }
            set { _proxyPath = value; }
        }
    }
}