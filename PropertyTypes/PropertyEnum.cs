using System;

namespace RetailWay.Integration.LibPCBS.PropertyTypes
{
    public class PropertyEnum<T>: PropertyValue<T> where T: Enum
    {
        public PropertyEnum(Device device, string command, T defaultValue = default) : 
            base(device, command, defaultValue) { }

        protected override T Deserialize(string text) => (T)Enum.ToObject(typeof(T),int.Parse(text));

        protected override string Serialize(T value) => $"{value:d}";
    }
}