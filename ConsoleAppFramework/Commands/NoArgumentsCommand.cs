using System.Text.RegularExpressions;

namespace Application
{
    public abstract class NoArgumentsCommand : Command
    {
        public NoArgumentsCommand(string args = "")
            :base(args)
        {
            if (!Regex.IsMatch(args, @"^\s*$"))
                throw new CommandCreationException(
                    $"This command doesn't take any arguments");
        }
    }
}
