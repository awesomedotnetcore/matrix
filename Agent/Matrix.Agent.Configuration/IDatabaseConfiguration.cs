namespace Matrix.Agent.Configuration
{
    public interface IDatabaseConfiguration
    {
        bool Enabled { get; }

        string Type { get; }

        string Url { get; }
    }
}