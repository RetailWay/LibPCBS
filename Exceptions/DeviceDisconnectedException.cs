using System.IO;

namespace RetailWay.Integration.LibPCBS.Exceptions
{
    public sealed class DeviceDisconnectedException: IOException
    {
        public DeviceDisconnectedException(): base("Устройство с данным адресом отключено") { }
    }
}