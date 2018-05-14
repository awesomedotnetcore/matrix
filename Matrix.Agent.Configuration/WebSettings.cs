namespace Matrix.Agent.Configuration
{
    public class WebSettings : Settings, IWebConfiguration
    {
        public bool Enabled { get { return bool.Parse(GetConfiguration("matrix.web.enabled")); } }

        public string Scheme { get { return GetConfiguration("matrix.web.scheme"); } }

        public string Server { get { return GetConfiguration("matrix.web.server"); } }

        public int Port { get { return int.Parse(GetConfiguration("matrix.web.port")); } }

        public WebSettings(string name)
            : base(name)
        {
        }
    }
}