using System;
using System.Text.RegularExpressions;

namespace Application
{
    public abstract class SetRealCommand : Command
    {
        protected double value;

        public SetRealCommand(string args)
            : base(args)
        {
            var m = Regex.Match(args, @"\s*(\+|-)?[0-9]+(?:\.[0-9]+)?(?:[eE](?:\+|-)?[0-9]+)?\s*$");
            if (!m.Success)
                NotARealNumberArgument(args);

            value = double.Parse(args);
        }

        public abstract override void Execute(App app);

        protected abstract void NotARealNumberArgument(string args);
    }
}
