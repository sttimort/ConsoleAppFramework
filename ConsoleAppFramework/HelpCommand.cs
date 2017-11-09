namespace Application
{
    [Command("main",
             Name = "help",
             Aliases = new string[] {"h", "?"},
             Description = "Show help message")]
    public class HelpCommand : NoArgumentsCommand
    {
        public HelpCommand(string args)
            :base(args)
        {}

        public override void Execute(App app) => app.UI.DisplayHelp();
    }
}
