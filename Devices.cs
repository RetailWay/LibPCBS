using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using HidSharp;

namespace RetailWay.Integration.LibPCBS
{
    public static class Devices
    {
        private static Device[] _devices = new Device[0];

        public static void Discover()
        {
            var serial = SerialDiscover();
            var hid = HidDiscover();
            _devices = new Device[serial.Count + hid.Count];
            serial.CopyTo(_devices,0);
            hid.CopyTo(_devices, serial.Count);
        }

        private static List<Device> SerialDiscover()
        {
            var devs = new List<Device>();
            var ports = SerialPort.GetPortNames();
            foreach (var p in ports)
            {
                var port = p.ToUpper();
                if(!port.StartsWith("COM") || port == "COM1") continue;
                var strResp = "";
                using (var serialConn = new SerialPort(port))
                {
                    serialConn.WriteTimeout = 1000;
                    serialConn.ReadTimeout = 10000;
                    serialConn.Open();
                    try
                    {
                        var req = new byte[] { 128, 37, 0, 0, 0, 0, 8 };
                        serialConn.Write(req, 0, req.Length);
                        req = new byte[] { 0, 194, 1, 0, 0, 0, 8 };
                        serialConn.Write(req, 0, req.Length);
                        req = new byte[] { 255, 77, 13, 56, 48, 48, 48, 48, 49, 49, 46 };
                        serialConn.Write(req, 0, req.Length);
                        var resp = new byte[9];
                        serialConn.Read(resp, 0, resp.Length);
                        strResp = Encoding.ASCII.GetString(resp);
                    }
                    catch
                    {
                        strResp = "";
                    }
                }
                if (strResp == "8000011\x06.") continue;
                devs.Add(new Device(ConnType.Serial, port));
            }

            return devs;
        }

        private static List<Device> HidDiscover()
        {
            var devs = new List<Device>();
            var devices = DeviceList.Local.GetHidDevices();
            foreach (var device in devices)
            {
                if(!device.TryOpen(out var stream)) continue;
                var resp = "";
                try
                {
                    var req = new byte[64];
                    var rawRequest = new byte[]
                    {
                        0xfd, 0x0b, 0xff, 0x4d, 0x0d, 0x38, 0x30, 0x30, 0x30, 0x30, 0x31, 0x31, 0x2e
                    };
                    rawRequest.CopyTo(req, 0);
                    stream.Write(req);
                    resp = Encoding.ASCII.GetString(stream.Read());
                }
                catch
                {
                    // todo 
                }
                finally
                {
                    stream.Dispose();
                    if (resp.Trim('\0') == "\x02" + "\t\0\0\08000011\x06.")
                        devs.Add(new Device(ConnType.Hid, device.DevicePath));
                }
            }
            
            return devs;
        }

        public static int Lenght => _devices.Length;

        public static Device Get(int index) => 
            _devices[index < 0 ? _devices.Length - index : index];
    }
}