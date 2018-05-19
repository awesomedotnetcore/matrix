namespace Matrix.Agent.Messages.Commands.Responses
{
    public interface IResponse : ICommand
    {
        int Status { get; set; }

        bool Error { get; set; }
    }
}