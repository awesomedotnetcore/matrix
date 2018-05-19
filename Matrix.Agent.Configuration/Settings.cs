using System;
using System.Configuration;

namespace Matrix.Agent.Configuration
{
    public class Settings : IConfiguration
    {
        public IAgentConfiguration Agent { get { return new AgentSettings(Name); } }

        public IAppConfiguration App { get { return new AppSettings(Name); } }

        public IBusConfiguration Bus { get { return new BusSettings(Name); } }

        public IWebConfiguration Web { get { return new WebSettings(Name); } }

        public IDatabaseConfiguration Database { get { return new DatabaseSettings(Name); } }

        private string Name { get; }

        public Settings()
        {
            Name = string.Empty;
        }

        public Settings(string name)
        {
            Name = name;
        }

        protected string GetConfiguration(string key)
        {
            var result = string.Empty;

            result = ConfigurationManager.AppSettings[key];

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine)))
                result = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine);

            key = key.Replace("matrix.", $"matrix.{Name}.");

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine)))
                result = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine);

            return result;
        }
    }
}