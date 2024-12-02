using System.IO.Ports;
using System.Linq;
using System.Text;
using HidSharp;

namespace RetailWay.Integration.LibPCBS
{
    using Exceptions;
    
    public partial class Device
    {
        private readonly ConnType Type;
        private readonly string Address;
        private readonly CachedType<HidStream> hid;
        private readonly CachedType<SerialPort> com;

        internal Device(ConnType connType, string address) : this()
        {
            Type = connType;
            Address = address;
            if (Type == ConnType.Hid)
                hid = new CachedType<HidStream>(() =>
                {
                    var devices = DeviceList.Local.GetHidDevices();
                    foreach (var dev in devices)
                    {
                        if (dev.DevicePath == Address && dev.TryOpen(out var stream))
                            return stream;
                    }

                    throw new DeviceDisconnectedException();
                }, port => port.Dispose());
            else
                com = new CachedType<SerialPort>(() =>
                {
                    var port = new SerialPort(Address);
                    port.Open();
                    return port;
                }, port => port.Dispose());
        }
        
        public static bool TryConnect(ConnType connType, string path, out Device dev)
        {
            dev = new Device(connType, path);
            try
            {
                return !string.IsNullOrWhiteSpace(dev.General.SerialNumber);
            }
            catch
            {
                dev = null;
                return false;
            }
        }

        internal void Set(string command, string value)
        {
            var data = $"{command}{value}.";
            _ = Type == ConnType.Hid ? SendHid(data) : SendCom(data);
        }
        internal string Get(string command)
        {
            var data = $"{command}?.";
            return Type == ConnType.Hid ? SendHid(data) : SendCom(data);
        }

        private string SendHid(string command)
        {
            if (command.Contains(';') || command.Count(c => c == '.') > 1)
                throw new InvalidCommandException();
            if (command.Length > 59) throw new CommandTooLongException();
            var req = new byte[64];
            new byte[] { 0xfd, (byte)(command.Length + 3), 0xff, 0x4d, 0x0d }.CopyTo(req, 0);
            Encoding.ASCII.GetBytes(command).CopyTo(req, 5);
            hid.Value.Write(req);
            var resp = new byte[64];
            hid.Value.Read(resp);
            var strResp = Encoding.ASCII.GetString(resp).TrimEnd('\0');
            return strResp.Substring(11, strResp.Length-13);
        }

        private string SendCom(string command)
        {
            if (command.Contains(';') || command.Count(c => c == '.') > 1)
                throw new InvalidCommandException();
            var req = new byte[] { 128, 37, 0, 0, 0, 0, 8 };
            com.Value.Write(req, 0, req.Length);
            req = new byte[] { 0, 194, 1, 0, 0, 0, 8 };
            com.Value.Write(req, 0, req.Length);
            req = new byte[3+command.Length];
            new byte[] { 255, 77, 13 }.CopyTo(req,0);
            Encoding.ASCII.GetBytes(command).CopyTo(req, 3);
            com.Value.Write(req, 0, req.Length);
            var resp = new byte[64];
            com.Value.Read(resp, 0, resp.Length);
            var strResp = Encoding.ASCII.GetString(resp).TrimEnd('\0');
            return strResp.Substring(6, strResp.Length-8);
        }
    }
}