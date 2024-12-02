namespace RetailWay.Integration.LibPCBS.PropertyTypes
{
    public sealed class PropertyBoolean: PropertyValue<bool>
    {
        public PropertyBoolean(Device device, string command, bool defaultValue = default) 
            : base(device, command, defaultValue) { }

        protected override bool Deserialize(string text) => text == "1";
        protected override string Serialize(bool value) => value ? "1" : "0";
    }
}