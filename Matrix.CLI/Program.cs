using Matrix.CLI.Commands.Configuration;
using Matrix.CLI.Commands.Directory;
using Matrix.CLI.Commands.General;
using Matrix.CLI.Commands.Journal;
using Matrix.CLI.Commands.Registry;
using Microsoft.Extensions.CommandLineUtils;
using System;

namespace Matrix.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: true);

            try
            {
                app.Name = "mx";
                app.Description = "Matrix Command-line Interface";
                app.HelpOption("-? | -h | --help");

                app.Command("system", new RegisterGeneralCommands().Execute);
                app.Command("app", new RegisterApplicationCommands().Execute);
                app.Command("config", new RegisterConfigurationCommands().Execute);
                app.Command("user", new RegisterDirectoryCommands().Execute);
                app.Command("log", new RegisterLogCommands().Execute);

                app.OnExecute(() =>
                {
                    app.ShowHelp("mx");
                    return 0;
                });

                app.Execute(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                while (e.InnerException != null)
                {
                    Console.WriteLine(e.InnerException.Message);
                    e = e.InnerException;
                }

                app.ShowHelp();
            }
        }
    }
}