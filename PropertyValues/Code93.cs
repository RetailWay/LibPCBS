namespace RetailWay.Integration.LibPCBS.PropertyValues
{
    using PropertyTypes;

    public sealed class Code93
    {
        public PropertyBoolean IsEnabled { get; }
        public PropertyInteger CharsMinCount { get; }
        public PropertyInteger CharsMaxCount { get; }
        
        private readonly Device _dev;

        internal Code93(Device device)
        {
            _dev = device;

            IsEnabled = new PropertyBoolean(_dev, "904002", true);
            CharsMaxCount = new PropertyInteger(_dev, "904003", 80);
            CharsMinCount = new PropertyInteger(_dev, "904004");
        }

        public void Reset() => _dev.Set("904000", null);
    }
}