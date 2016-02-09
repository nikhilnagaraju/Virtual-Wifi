
using System.Collections.Generic;
using System.ServiceModel;

namespace VirtualRouterHost
{
    [ServiceContract(Namespace = "http://VirtualRouter")]
    public interface IVirtualRouterHost
    {
        [OperationContract]
        bool Start(SharableConnection sharedConnection);

        [OperationContract]
        bool Stop();

        [OperationContract]
        bool SetConnectionSettings(string ssid, int maxPeerCount);

        [OperationContract]
        ConnectionSettings GetConnectionSettings();

        [OperationContract]
        IEnumerable<SharableConnection> GetSharableConnections();

        [OperationContract]
        bool SetPassword(string password);

        [OperationContract]
        string GetPassword();

        [OperationContract]
        bool IsStarted();

        [OperationContract]
        IEnumerable<ConnectedPeer> GetConnectedPeers();

        [OperationContract]
        SharableConnection GetSharedConnection();

        [OperationContract]
        string GetLastError();
    }
}
