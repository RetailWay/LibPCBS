namespace RetailWay.Integration.LibPCBS.PropertyValues
{
    using PropertyTypes;

    public sealed class Straight2of5IATA
    {
        public PropertyBoolean IsEnabled { get; }
        public PropertyInteger CharsMinCount { get; }
        public PropertyInteger CharsMaxCount { get; }
        
        private readonly Device _dev;

        internal Straight2of5IATA(Device device)
        {
            _dev = device;

            IsEnabled = new PropertyBoolean(_dev, "906001", true);
            CharsMaxCount = new PropertyInteger(_dev, "906002", 48);
            CharsMinCount = new PropertyInteger(_dev, "906003", 4);
        }

        public void Reset() => _dev.Set("906000", null);
    }
}