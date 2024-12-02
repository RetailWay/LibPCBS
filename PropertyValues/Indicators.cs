namespace RetailWay.Integration.LibPCBS.PropertyValues
{
    using Enums;
    using PropertyTypes;

    public class Indicators
    {
        public PropertyBoolean IsStartupSoundEnabled { get; }
        public PropertyBoolean IsClickedSoundEnabled { get; }
        public PropertyBoolean IsSuccessSoundEnabled { get; }
        public PropertyBoolean IsBacklightEnabled { get; }
        public PropertyEnum<SoundVolume> SoundLevel { get; }
        public PropertyInteger FrequencySounds { get; }
        public PropertyBoolean IsShortSuccessSound { get; }
        public PropertyInteger FrequencyErrorSound { get; }
        public PropertyBoolean IsSuccessLightEnabled { get; }
        public PropertyInteger IntervalRead { get; }
        public PropertyInteger IntervalTransfer { get; }

        private Device _dev;

        internal Indicators(Device device)
        {
            _dev = device;

            IsStartupSoundEnabled = new PropertyBoolean(_dev, "841013", true);
            IsClickedSoundEnabled = new PropertyBoolean(_dev, "841014");
            IsSuccessSoundEnabled = new PropertyBoolean(_dev, "841001", true);
            IsBacklightEnabled = new PropertyBoolean(_dev, "898005", true);
            SoundLevel = new PropertyEnum<SoundVolume>(_dev, "841009", SoundVolume.High);
            FrequencySounds = new PropertyInteger(_dev, "841006", 2400);
            IsShortSuccessSound = new PropertyBoolean(_dev, "841002");
            FrequencyErrorSound = new PropertyInteger(_dev, "841007", 240);
            IsSuccessLightEnabled = new PropertyBoolean(_dev, "841008", true);
            IntervalRead = new PropertyInteger(_dev, "851006", 750);
            IntervalTransfer = new PropertyInteger(_dev, "851004");
        }
    }
}