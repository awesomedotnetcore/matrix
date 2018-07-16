using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;

namespace Matrix.Agent.Registry.Messages.Commands.Requests
{
    [Queue("Registry.ListApplication", ExchangeName = "Registry.ListApplication")]
    public class ListApplicationRequest : Request
    {
    }
}