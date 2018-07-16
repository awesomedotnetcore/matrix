using EasyNetQ.Management.Client;
using Microsoft.Extensions.CommandLineUtils;

namespace Matrix.CLI.Commands.General
{
    public class CleanCommand
    {
        private CommandArgument Hostname { get; set; }
        private CommandArgument Username { get; set; }
        private CommandArgument Password { get; set; }

        public void Execute(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");
            cmd.Description = "clean system components";

            Hostname = cmd.Argument("hostname", "hostname of the rabbitmq server");
            Username = cmd.Argument("username", "administrator username of the rabbitmq server");
            Password = cmd.Argument("password", "administrator password of the rabbitmq server");

            cmd.OnExecute(async () =>
            {
                var management = new ManagementClient(Hostname.Value, Username.Value, Password.Value);

                foreach (var exchange in await management.GetExchangesAsync())
                {
                    if (exchange.Vhost.Equals("matrix") && exchange.Name.StartsWith("ErrorExchange_easynetq.response."))
                    {
                        await management.DeleteExchangeAsync(exchange);
                    }
                }

                return 0;
            });
        }
    }
}