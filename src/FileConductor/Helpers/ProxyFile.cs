using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.Helpers
{
    /// <summary>
    /// Represents a proxy file used as a temporary file during various operations
    /// (e.g. copying between different types of targets, where direct copying is not possible)
    /// </summary>
    public class ProxyFile
    {
        private static string _proxyPath;

        public static string ProxyPath
        {
            get
            {
                if (String.IsNullOrEmpty(_proxyPath))
                {
                    var assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                    var localPath = new Uri(assemblyPath).LocalPath;

                    var combined = Path.Combine(localPath, "Files/");

                    Directory.CreateDirectory(combined);

                    _proxyPath = combined;
                }

                return _proxyPath;
            }
            set { _proxyPath = value; }
        }
    }
}