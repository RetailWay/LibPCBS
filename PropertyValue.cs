namespace RetailWay.Integration.LibPCBS
{
    public abstract class PropertyValue<T>
    {
        private readonly string _command;
        private readonly T _default;
        private readonly Device _device;
        
        public T Value
        {
            get => Deserialize(_device.Get(_command));
            set => _device.Set(_command, Serialize(value));
        }

        internal PropertyValue(Device device, string command, T defaultValue = default)
        {
            _device = device;
            _command = command; 
            _default = defaultValue;
        }

        public void Reset() => _device.Set(_command, $"{_default}");

        protected abstract T Deserialize(string text);
        protected abstract string Serialize(T value);
    }
}