namespace RetailWay.Integration.LibPCBS
{
    using PropertyValues;
    
    public partial class Device
    {
        public readonly General General;
        public readonly Indicators Indicators;
        
        public readonly CodaBar CodaBar;
        public readonly Code39 Code39;
        public readonly Code93 Code93;
        public readonly Interleaved2of5 Interleaved2of5;
        public readonly Nec2of5 Nec2of5;
        public readonly Straight2of5IATA Straight2of5IATA;
        public readonly Straight2of5Industrial Straight2of5Industrial;

        private Device()
        {
            General = new General(this);
            Indicators = new Indicators(this);
            
            CodaBar = new CodaBar(this);
            Code39 = new Code39(this);
            Code93 = new Code93(this);
            Interleaved2of5 = new Interleaved2of5(this);
            Nec2of5 = new Nec2of5(this);
            Straight2of5IATA = new Straight2of5IATA(this);
            Straight2of5Industrial = new Straight2of5Industrial(this);
        }
    }
}