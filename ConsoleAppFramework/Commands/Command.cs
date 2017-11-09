using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Application
{
    public abstract class Command : ICommand
    {
        protected Command(string args) {}

        public static string GetName(Type t)
        {
            CommandAttribute cmdAttr;

            if (!Array.Exists(t.GetInterfaces(), (o) => o.Equals(typeof(ICommand))) ||
                (cmdAttr = t.GetCustomAttribute<CommandAttribute>()) == null)
            {
                return null;
            }

            var name = cmdAttr.Name;

            if (name == null)
            {
                var m = Regex.Match(t.Name, @"^(?<name>.+)Command$");
                name = m.Success ? m.Groups["name"].Value : t.Name;
            }

            return name;
        }

        public abstract void Execute(App app);
    }
}
