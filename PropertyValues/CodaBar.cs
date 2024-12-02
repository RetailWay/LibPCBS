namespace RetailWay.Integration.LibPCBS.PropertyValues
{
    using Enums;
    using PropertyTypes;
    
    public sealed class CodaBar
    {
        public PropertyBoolean IsEnabled { get; }
        public PropertyBoolean IsBorderCharsEnabled { get; }
        public PropertyInteger CharsMinCount { get; }
        public PropertyInteger CharsMaxCount { get; }
        public PropertyBoolean IsJoinModeEnabled { get; }
        public PropertyEnum<UsageCheckChar> UsageCheckChar { get; }
        
        private readonly Device _dev;

        internal CodaBar(Device device)
        {
            _dev = device;
            
            UsageCheckChar = new PropertyEnum<UsageCheckChar>(_dev, "900001");
            IsJoinModeEnabled = new PropertyBoolean(_dev, "900002");
            IsEnabled = new PropertyBoolean(_dev, "900003", true);
            CharsMaxCount = new PropertyInteger(_dev, "900004", 60);
            CharsMinCount = new PropertyInteger(_dev, "900005", 4); 
            IsBorderCharsEnabled = new PropertyBoolean(_dev, "900006");
        }

        public void Reset() => _dev.Set("900000", null);
    }
}