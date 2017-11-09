using System;
namespace Application
{
    [Command("main",
             Name = "mycommand",
             ArgsFormat = "<my arguents discription",
             Aliases = new string[] {"myAlias1, myAlias2"},
             Description = "My description")]
    public class MyOwnCommand : Command
    {
        public MyOwnCommand(string args)
            :base(args)
        {
            // check of arguments here
            // if they are wrong, throw CommandCreationException(ERROR_MESSAGE)
            // ERROR_MESSAGE will be displayes to the user
        }

        public override void Execute(App app)
        {
            app.UI.Display("This is my own command");
        }
    }
}
