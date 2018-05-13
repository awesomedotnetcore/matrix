namespace Matrix.Agent.Configuration
{
    public interface IConfiguration
    {
        IAppConfiguration App { get; }

        IBusConfiguration Bus { get; }

        IWebConfiguration Web { get; }

        IDatabaseConfiguration Database { get; }
    }
}