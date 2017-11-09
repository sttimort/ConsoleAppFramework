namespace Application
{
    [Command("main",
             Aliases = new string[] {"quit", "bye", "q"},
             Description = "Exit the programm")]
    public class ExitCommand : NoArgumentsCommand
    {
        public ExitCommand(string args)
            :base(args)
        {}

        public override void Execute(App app) => app.ShouldExit = true;
    }
}
