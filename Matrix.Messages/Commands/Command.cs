using System;

namespace Matrix.Messages.Commands
{
    public abstract class Command : Message, ICommand
    {
        public Command()
        {
        }

        public Command(Guid app)
            : base(app)
        {
        }
    }
}