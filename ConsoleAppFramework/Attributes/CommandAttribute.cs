using System;

namespace Application
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string contextName)
        {
            ContextName = contextName;
        }

        public string Name { get; set; }
        public string[] Aliases { get; set; }
        public string ArgsFormat { get; set; } // e.g. <filename{string}>[, <name{string}>]
        public string Description { get; set; }
        public string ContextName { get; }
    }
}
