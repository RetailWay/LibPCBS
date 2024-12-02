using System.Text;

namespace RetailWay.Integration.LibPCBS.PropertyValues
{
    using Enums;
    using PropertyTypes;
    
    public sealed class General
    {
        private readonly Device _dev;

        public PropertyEnum<ConnInterface> Interface { get; }
        public PropertyBoolean UseHidCommand { get; }

        public string SerialNumber => _dev.Get("811005");
        public string VersionFirmware => _dev.Get("809004");

        public General(Device device)
        {
            _dev = device;

            Interface = new PropertyEnum<ConnInterface>(_dev, "881001", ConnInterface.Hid);
            UseHidCommand = new PropertyBoolean(_dev, "942109", true);
        }

        public void ResetSettings() => _dev.Set("800006", null);
        public void BeginSettings() => _dev.Set("800010", null);
        public void EndSettings() => _dev.Set("800011", null);
        public void CancelSettings() => _dev.Set("800007", null);
        public void Save() => _dev.Set("800002", null);
        public void Cancel() => _dev.Set("800000", null);

        public void SetPrefix(BarCode code, params byte[] prefix) =>
            _dev.Set("889002", GetValueChars(code, prefix));

        public void SetSuffix(BarCode code, params byte[] suffix) =>
            _dev.Set("888002", GetValueChars(code, suffix));

        public void ClearPrefixes() => _dev.Set("889003", null);
        public void ClearSuffixes() => _dev.Set("888003", null);

        public void ClearPrefix(BarCode code) => _dev.Set("889004", $"{code:X}");
        public void ClearSuffix(BarCode code) => _dev.Set("888004", $"{code:X}");

        private static string GetValueChars(BarCode code, byte[] chars)
        {
            var value = new StringBuilder($"{code:x}");
            foreach (var e in chars)
                value.Append($"{e:x2}");
            return value.ToString();
        }
    }
}