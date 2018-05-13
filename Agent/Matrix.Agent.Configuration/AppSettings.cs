using System;

namespace Matrix.Agent.Configuration
{
    public class AppSettings : Settings, IAppConfiguration
    {
        public Guid Id { get { return AssemblyInfo.Id; } }

        public string Name { get { return AssemblyInfo.Name; } }

        public string Description { get { return AssemblyInfo.Description; } }

        public AppSettings(string name)
            : base(name)
        {
        }
    }
}