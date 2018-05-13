namespace Matrix.Agent.Configuration
{
    public class BusSettings : Settings, IBusConfiguration
    {
        public bool Enabled { get { return bool.Parse(GetConfiguration("matrix.bus.enabled")); } }

        public string Type { get { return GetConfiguration("matrix.bus.type"); } }

        public string Url { get { return GetConfiguration("matrix.bus.url"); } }

        public BusSettings(string name)
            : base(name)
        {
        }
    }
}