using System;

namespace Matrix.Agent.Configuration
{
    public class AgentSettings : Settings, IAgentConfiguration
    {
        public Guid Id { get { return Guid.Parse(GetConfiguration("matrix.agent.id")); } }

        public string Name { get { return GetConfiguration("matrix.bus.enabled"); } }

        public AgentType Type { get { return (AgentType)Enum.Parse(typeof(AgentType), GetConfiguration("matrix.bus.enabled")); } }

        public AgentSettings(string name)
            : base(name)
        {
        }
    }
}