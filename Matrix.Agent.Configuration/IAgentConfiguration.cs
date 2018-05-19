using System;

namespace Matrix.Agent.Configuration
{
    public interface IAgentConfiguration
    {
        Guid Id { get; }

        string Name { get; }

        AgentType Type { get; }
    }
}