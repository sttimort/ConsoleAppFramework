using System;

namespace Application
{
    public class CommandException : Exception
    {
        public CommandException(string msg = "")
            :base(msg)
        {}

        public CommandException(string msg, Exception e)
            : base(msg, e)
        { }
    }

    public class CommandsLoadException: CommandException
    {
        public CommandsLoadException(string msg, Exception e)
            :base(msg, e)
        { }
    }

    public class CommandCreationException : CommandException
    {
        public CommandCreationException(string msg = "")
            : base(msg)
        {}
    }
}
