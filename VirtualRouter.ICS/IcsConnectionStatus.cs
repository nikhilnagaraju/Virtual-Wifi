
namespace IcsMgr
{
    public enum IcsConnectionStatus : int
    {
        DISCONNECTED = 0,
        CONNECTING = 1,
        CONNECTED = 2,
        DISCONNECTING = 3,
        HARDWARE_NOT_PRESENT = 4,
        HARDWARE_DISABLED = 5,
        HARDWARE_MALFUNCTION = 6,
        MEDIA_DISCONNECTED = 7,
        AUTHENTICATING = 8,
        AUTHENTICATION_SUCCEEDED = 9,
        AUTHENTICATION_FAILED = 10,
        INVALID_ADDRESS = 11,
        CREDENTIALS_REQUIRED = 12,
    }
}
