using System;

namespace Matrix.Agent.Configuration
{
    public interface IAppConfiguration
    {
        Guid Id { get; }

        string Name { get; }

        string Description { get; }
    }
}