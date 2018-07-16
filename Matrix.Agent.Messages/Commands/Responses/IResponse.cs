using System;

namespace Matrix.Agent.Messages.Commands.Responses
{
    public interface IResponse : ICommand
    {
        Guid RequestId { get; set; }

        Guid ResponseId { get; set; }

        int Status { get; set; }

        bool Error { get; set; }
    }
}