using System;
namespace Application
{
    [Command("main", // this is name of UI Context to which this command belongs
             Name = "mycommand",
             ArgsFormat = "<my arguents discription",
             Aliases = new string[] {"myAlias1, myAlias2"},
             Description = "My description")]
    public class MyOwnCommand : Command
    {
        public MyOwnCommand(string args)
            :base(args)
        {
            // args is everything that was typed after command name (or it's alias)
            // for example for command: mycommands 1, 22f wefwef
            // args will be: " 1, 22f wefwef"


            // check arguments here
            // if they are wrong, throw CommandCreationException(ERROR_MESSAGE)
            //ERROR_MESSAGE will be displayes to the user
        }

        public override void Execute(App app)
        {
            app.UI.Display("This is my own command");
        }
    }
}
