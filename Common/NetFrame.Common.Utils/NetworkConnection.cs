using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;

namespace NetFrame.Common.Utils
{
    /// <summary>
    /// Developed to create a network connection using a different network credential than the user of the application.
    /// Detaylı bilgi için: https://gist.github.com/AlanBarber/92db36339a129b94b7dd
    /// 
    /// Kullanımı:
    /// .
    /// .
    /// var networkPath = @"//server/share";
    /// var credentials = new NetworkCredential("username", "password");
    /// 
    /// using (new NetworkConnection(networkPath, credentials))
    /// {
    ///     var fileList = Directory.GetFiles(networkPath);
    ///     foreach (var file in fileList)
    ///     {
    ///         Console.WriteLine("{0}", Path.GetFileName(file));
    ///     }
    ///     .
    ///     .
    /// }
    /// .
    /// .
    /// </summary>
    public class NetworkConnection : IDisposable
    {
        private readonly string _networkName;

        /// <summary>
        /// Ağ paylaşım adresi ve credential bilgisi ile Yeni NetworkConncetion objesi oluşturur.
        /// </summary>
        /// <param name="networkName"></param>
        /// <param name="credentials"></param>
        public NetworkConnection(string networkName, NetworkCredential credentials)
        {
            _networkName = networkName;

            var netResource = new NetResource
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = networkName
            };

            var userName = string.IsNullOrEmpty(credentials.Domain)
                ? credentials.UserName
                : $@"{credentials.Domain}\{credentials.UserName}";

            var result = WNetAddConnection2(
                netResource,
                credentials.Password,
                userName,
                0);

            if (result != 0)
            {
                throw new Win32Exception(result, "Error connecting to remote share");
            }
        }

        ~NetworkConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(_networkName, 0, true);
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource,
            string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags,
            bool force);

        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplaytype DisplayType;
            public int Usage;
            public string LocalName = string.Empty;
            public string RemoteName = string.Empty;
            public string Comment = string.Empty;
            public string Provider = string.Empty;
        }

        public enum ResourceScope
        {
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
        };

        public enum ResourceType
        {
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
        }

        public enum ResourceDisplaytype
        {
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            Shareadmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            Ndscontainer = 0x0b
        }
    }
}
