namespace RetailWay.Integration.LibPCBS.PropertyValues
{
    using Enums;
    using PropertyTypes;

    public sealed class Code39
    {
        public PropertyBoolean IsEnabled { get; }
        public PropertyBoolean IsBorderCharsEnabled { get; }
        public PropertyInteger CharsMinCount { get; }
        public PropertyInteger CharsMaxCount { get; }
        public PropertyBoolean IsJoinDataEnabled { get; }
        public PropertyEnum<UsageCheckChar> UsageCheckChar { get; }
        public PropertyBoolean IsSupportCode32Enabled { get; }
        public PropertyBoolean IsSupportFullAsciiEnabled { get; }
        
        private readonly Device _dev;

        internal Code39(Device device)
        {
            _dev = device;
            
            IsEnabled = new PropertyBoolean(_dev, "901001", true);
            IsJoinDataEnabled = new PropertyBoolean(_dev, "901002");
            IsSupportFullAsciiEnabled = new PropertyBoolean(_dev, "901003");
            UsageCheckChar = new PropertyEnum<UsageCheckChar>(_dev, "901004");
            IsSupportCode32Enabled = new PropertyBoolean(_dev, "901005", true);
            CharsMaxCount = new PropertyInteger(_dev, "901007", 48);
            CharsMinCount = new PropertyInteger(_dev, "901008"); 
            IsBorderCharsEnabled = new PropertyBoolean(_dev, "901009");
        }

        public void Reset() => _dev.Set("901000", null);
    }
}