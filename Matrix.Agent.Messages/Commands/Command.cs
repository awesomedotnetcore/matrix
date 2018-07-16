using System;

namespace Matrix.Agent.Messages.Commands
{
    public abstract class Command : Message, ICommand
    {
        public Guid CommandId { get; set; }

        public Command()
        {
            CommandId = Guid.NewGuid();
        }

        public Command(Guid app)
            : base(app)
        {
            CommandId = Guid.NewGuid();
        }
    }
}