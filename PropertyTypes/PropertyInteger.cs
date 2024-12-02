namespace RetailWay.Integration.LibPCBS.PropertyTypes
{
    public class PropertyInteger: PropertyValue<int>
    {
        public PropertyInteger(Device device, string command, int defaultValue = default) : 
            base(device, command, defaultValue) { }

        protected override int Deserialize(string text) => int.Parse(text);

        protected override string Serialize(int value) => $"{value}";
    }
}