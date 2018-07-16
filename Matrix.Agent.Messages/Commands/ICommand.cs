using System;

namespace Matrix.Agent.Messages.Commands
{
    public interface ICommand : IMessage
    {
        Guid CommandId { get; set; }
    }
}