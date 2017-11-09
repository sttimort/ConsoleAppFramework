using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;

namespace Application.UI
{
    public abstract class UIContext : IUIContext
    {
        protected Dictionary<string, Type> commandClasses;
        protected Dictionary<string, CommandInfo> commandsInfoByID;
        protected string cmdPrompt;
		protected IUIParser parser;

        protected UIContext()
        {
            commandClasses = findCommandClasses();
            cmdPrompt = ">";
            commandsInfoByID = getCommandsInfo();

            Configure();
        }

        public virtual IEnumerable<CommandInfo> CommandsInfo
        { 
            get { return commandsInfoByID.Values; } 
        }

        public virtual string CommandPrompt { get { return cmdPrompt; } }


        public static string GetName(Type t)
        {
            UIContextAttribute contextAttr;

            if (!Array.Exists(t.GetInterfaces(), (o) => o.Equals(typeof(IUIContext))) |
                (contextAttr = t.GetCustomAttribute<UIContextAttribute>()) == null)
            {
				return null;
            }

            var name = contextAttr.Name;

            if (name == null)
            {
                var m = Regex.Match(t.Name, @"^(?<name>.+)UIContext$");
                name = m.Success ? m.Groups["name"].Value : t.Name;
            }

            return name;
        }

        public virtual ICommand GetCommandFromInput(string input, out string errMsg)
        {
            string cmdClassID;
			errMsg = "";

            if (commandsInfoByID.Count < 1)
            {
                errMsg = "No available commands";
                return null;
            }

            if ((cmdClassID = parser.ParseInput(input, out string args)) == null)
            {
                errMsg = "Unknown command name";
                return null;
            }

            try 
            {
                return (ICommand)Activator
                    .CreateInstance(commandClasses[cmdClassID], new object[] { args });
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException is CommandCreationException)
                {
                    errMsg = e.InnerException.Message;
                    return null;
                }
                else
                    throw;
            }
        }

        Dictionary<string, Type> findCommandClasses()
        {
            var allTypes = typeof(UIContext).Assembly.GetTypes();
            var result = new Dictionary<string, Type>();

            // current context name
            var contextName = GetName(GetType()).ToLower();

            foreach (var t in allTypes)
            {
                var cmdAttr = t.GetCustomAttribute<CommandAttribute>();

                if (Array.Exists(t.GetInterfaces(), (o) => o.Equals(typeof(ICommand))) &&
                    cmdAttr != null && cmdAttr.ContextName == contextName)
                {
                    result.Add(Guid.NewGuid().ToString("N"), t);
                }
            }

            return result;
        }

        Dictionary<string, CommandInfo> getCommandsInfo()
        {
            var result = new Dictionary<string, CommandInfo>();

            foreach (var entry in commandClasses)
            {
                var ID = entry.Key;
                var cmdClass = entry.Value;

                var cmdAttr = cmdClass.GetCustomAttribute<CommandAttribute>();
                var cmdName = cmdAttr.Name;

                if (cmdName == null)
                {
                    var m = Regex.Match(cmdClass.Name, @"^(?<name>.+)Command$");
                    cmdName = m.Success ? m.Groups["name"].Value : cmdClass.Name;
                }

                result.Add(ID, new CommandInfo(cmdName, cmdAttr.ArgsFormat,
                                           cmdAttr.Aliases, cmdAttr.Description));
            }

            return result;
        }

        protected virtual void Configure()
        {
            parser = new UIParser(commandsInfoByID);
        }
    }
}