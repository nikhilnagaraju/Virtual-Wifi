
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using VirtualRouter.Wlan.WinAPI;
using VirtualRouter.Wlan;

namespace VirtualRouterHost
{
    [DataContract]
    public class ConnectedPeer
    {
        public ConnectedPeer()
        {
        }

        public ConnectedPeer(WlanStation peer)
            : this()
        {
            this.MacAddress = peer.MacAddress;
        }

        [DataMember]
        public string MacAddress { get; set; }
    }
}
