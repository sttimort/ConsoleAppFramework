using System;

namespace Application
{
    public class CommandInfo
    {
        public string Name { get; }
        public string ArgsFormat { get; }
        public string[] Aliases { get; }
        public string Description { get; }

        public CommandInfo() {}

        public CommandInfo(string name, string argsFormat, string[] aliases, string desc)
        {
            Name = name;
            ArgsFormat = argsFormat;
            Aliases = aliases;
            Description = desc;
        }
    }
}
