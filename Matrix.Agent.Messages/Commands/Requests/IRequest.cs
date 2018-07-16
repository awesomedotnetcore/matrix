using System;

namespace Matrix.Agent.Messages.Commands.Requests
{
    public interface IRequest : ICommand
    {
        Guid RequestId { get; set; }
    }
}