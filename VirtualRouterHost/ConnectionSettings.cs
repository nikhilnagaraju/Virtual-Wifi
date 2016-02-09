
using System.Runtime.Serialization;

namespace VirtualRouterHost
{
    [DataContract]
    public class ConnectionSettings
    {
        [DataMember]
        public string SSID { get; set; }
        [DataMember]
        public int MaxPeerCount { get; set; }
    }
}
