using System;
using System.IO;
using System.Reflection;

namespace FileConductor.ProxyFile
{
    /// <summary>
    ///     Represents a proxy file used as a temporary file during various operations
    ///     (e.g. copying between different types of targets, where direct copying is not possible)
    /// </summary>
    public class ProxyFileProvider : IProxyFileProvider
    {
        private static string _proxyPath;

        public string ProxyPath
        {
            get
            {
                if (string.IsNullOrEmpty(_proxyPath))
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

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