using System;
using System.Text.RegularExpressions;

namespace Application
{
    public abstract class SetIntegerCommand : Command
    {
        protected int value;

        protected SetIntegerCommand(string args)
            : base(args)
        {
            var m = Regex.Match(args, @"\s*(\+|-)?[0-9]+\s*$");
            if (!m.Success)
                NotAIntegerNumberArgument(args);

            value = int.Parse(args);
        }

        public abstract override void Execute(App app);

        protected abstract void NotAIntegerNumberArgument(string args);
    }
}
