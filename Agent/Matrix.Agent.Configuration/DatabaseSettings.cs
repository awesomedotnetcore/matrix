namespace Matrix.Agent.Configuration
{
    public class DatabaseSettings : Settings, IDatabaseConfiguration
    {
        public bool Enabled { get { return bool.Parse(GetConfiguration("matrix.db.enabled")); } }

        public string Type { get { return GetConfiguration("matrix.db.type"); } }

        public string Url { get { return GetConfiguration("matrix.db.url"); } }

        public DatabaseSettings(string name)
            : base(name)
        {
        }
    }
}