namespace RetailWay.Integration.LibPCBS.PropertyValues
{
    using Enums;
    using PropertyTypes;

    public sealed class Nec2of5
    {
        public PropertyBoolean IsEnabled { get; }
        public PropertyInteger CharsMinCount { get; }
        public PropertyInteger CharsMaxCount { get; }
        public PropertyEnum<UsageCheckChar> UsageCheckChar { get; }
        
        private readonly Device _dev;

        internal Nec2of5(Device device)
        {
            _dev = device;

            UsageCheckChar = new PropertyEnum<UsageCheckChar>(_dev, "903001");
            IsEnabled = new PropertyBoolean(_dev, "903002", true);
            CharsMaxCount = new PropertyInteger(_dev, "903003", 80);
            CharsMinCount = new PropertyInteger(_dev, "903004", 4);
        }

        public void Reset() => _dev.Set("903000", null);
    }
}