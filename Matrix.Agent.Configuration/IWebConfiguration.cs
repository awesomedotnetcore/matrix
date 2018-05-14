namespace Matrix.Agent.Configuration
{
    public interface IWebConfiguration
    {
        bool Enabled { get; }

        string Scheme { get; }

        string Server { get; }

        int Port { get; }
    }
}