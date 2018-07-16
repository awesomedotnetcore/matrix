using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI.Commands.General
{
    public class GetErrorCommand
    {
        private CommandArgument Hostname { get; set; }
        private CommandArgument Username { get; set; }
        private CommandArgument Password { get; set; }

        public void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = "get last errors";

            Hostname = cmd.Argument("hostname", "hostname of the rabbitmq server");
            Username = cmd.Argument("username", "administrator username of the rabbitmq server");
            Password = cmd.Argument("password", "administrator password of the rabbitmq server");

            cmd.OnExecute(async () =>
            {
                var management = new ManagementClient(Hostname.Value, Username.Value, Password.Value);

                var queue = new Queue()
                {
                    Name = "EasyNetQ_Default_Error_Queue",
                    Vhost = "matrix"
                };

                foreach (var msg in (await management.GetMessagesFromQueueAsync(queue, new GetMessagesCriteria(256, false))))
                {
                    Console.WriteLine(msg.Payload);
                }

                return 0;
            });
        }
    }
}