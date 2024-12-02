namespace RetailWay.Integration.LibPCBS.PropertyValues
{
    using Enums;
    using PropertyTypes;

    public sealed class Interleaved2of5
    {
        public PropertyBoolean IsEnabled { get; }
        public PropertyInteger CharsMinCount { get; }
        public PropertyInteger CharsMaxCount { get; }
        public PropertyEnum<UsageCheckChar> UsageCheckChar { get; }
        
        private readonly Device _dev;

        internal Interleaved2of5(Device device)
        {
            _dev = device;

            UsageCheckChar = new PropertyEnum<UsageCheckChar>(_dev, "902001");
            IsEnabled = new PropertyBoolean(_dev, "902002", true);
            CharsMaxCount = new PropertyInteger(_dev, "902003", 80);
            CharsMinCount = new PropertyInteger(_dev, "902004", 4);
        }

        public void Reset() => _dev.Set("902000", null);
    }
}