using System;

namespace Matrix.Messages.Errors
{
    public interface IError : IMessage
    {
        int Code { get; set; }

        Exception Exception { get; set; }
    }
}