using System.ServiceModel;

namespace NetFrame.Common.Utils
{
    /// <summary>
    /// Proxy bilgilerini döndüren sınıf
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WcfProxyHelper<T>
    {
        /// <summary>
        /// Proxy verisi yaratır.
        /// </summary>
        /// <returns>Yaratılan proxy verisi döner</returns>
        public static T CreateChannel(string address)
        {
            //string serviceBaseAddress = ConfigurationManager.AppSettings["ServiceAddress"];
            //string address = string.Format(serviceBaseAddress, typeof (T).Name.Substring(1));

            var binding = new BasicHttpBinding { MaxReceivedMessageSize = Int32.MaxValue };
            var channel = new ChannelFactory<T>(binding, new EndpointAddress(address));

            return channel.CreateChannel();
        }
    }
}
