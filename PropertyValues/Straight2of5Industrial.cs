namespace RetailWay.Integration.LibPCBS.PropertyValues
{
    using PropertyTypes;

    public sealed class Straight2of5Industrial
    {
        public PropertyBoolean IsEnabled { get; }
        public PropertyInteger CharsMinCount { get; }
        public PropertyInteger CharsMaxCount { get; }
        
        private readonly Device _dev;

        internal Straight2of5Industrial(Device device)
        {
            _dev = device;

            IsEnabled = new PropertyBoolean(_dev, "905001", true);
            CharsMaxCount = new PropertyInteger(_dev, "905002", 48);
            CharsMinCount = new PropertyInteger(_dev, "905003", 4);
        }

        public void Reset() => _dev.Set("905000", null);
    }
}