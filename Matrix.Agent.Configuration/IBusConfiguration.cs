namespace Matrix.Agent.Configuration
{
    public interface IBusConfiguration
    {
        bool Enabled { get; }

        string Type { get; }

        string Url { get; }
    }
}