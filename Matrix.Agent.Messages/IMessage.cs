using Matrix.Collections.Generic;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Messages
{
    public interface IMessage
    {
        Guid Id { get; }

        Guid Application { get; }

        DateTime Timestamp { get; }

        string Program { get; }

        string Version { get; }

        DateTime Build { get; }

        string Fingerprint { get; }

        string Process { get; }

        SerializableDictionary<string, string> Properties { get; }

        List<string> Tags { get; }
    }
}