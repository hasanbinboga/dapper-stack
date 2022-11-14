using System.ServiceModel;

namespace NetFrame.Common.Utils
{
    /// <summary>
    /// Class that returns WCF Proxy information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WcfProxyHelper<T>
    {
        /// <summary>
        /// Create Proxy channel
        /// </summary>
        /// <returns>Yaratılan proxy verisi döner</returns>
        public static T CreateChannel(string address)
        {
            
            var binding = new BasicHttpBinding { MaxReceivedMessageSize = Int32.MaxValue };
            var channel = new ChannelFactory<T>(binding, new EndpointAddress(address));

            return channel.CreateChannel();
        }
    }
}
