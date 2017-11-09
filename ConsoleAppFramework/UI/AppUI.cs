using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;
using Application;

namespace Application.UI
{
    public partial class AppUI
    {
        Stack<IUIContext> contextsHistory;
        IUIContext currContext;
        Dictionary<string, Type> contextClasses;


        public AppUI()
        {
            contextsHistory = new Stack<IUIContext>();
            contextClasses = findContextClasses();

            if (contextClasses.Count < 1)
                throw new UIContextClassesLoadException("No UI Context Classes found");

            var contextClassesList = new List<Type>(contextClasses.Values);
            var mainContextClass = contextClassesList
                .Find((t) => t.GetCustomAttribute<UIContextAttribute>().IsMain);

            if (mainContextClass == null)
                mainContextClass = contextClassesList[0];

            currContext = (UIContext)Activator.CreateInstance(mainContextClass);

            DisplayHelp();
        }


        public ICommand NextCommand
        {
            get
            {
                ICommand cmd;

                do
                {
                    Console.Write($"{currContext.CommandPrompt} ");

                    var input = Console.ReadLine();
                    if ((cmd = currContext.GetCommandFromInput(input, out string errMsg)) == null)
                        Console.WriteLine($"{errMsg}\n");
                    
                } while (cmd == null);

                return cmd;
            }
        }

        public void ToContext(string name)
        {
            contextsHistory.Push(currContext);
            currContext = (UIContext)Activator
                .CreateInstance(contextClasses[name]);
        }

        public void ToPrevContext()
        {
            if (contextsHistory.Count > 0)
                currContext = contextsHistory.Pop();
        }


        public void Display(string info)
        {
            Console.WriteLine(info);
            Console.WriteLine();
        }

        public void DisplayList(string caption, IEnumerable<string> items)
        {
            Console.WriteLine(caption + ":");

            var ord = 1;
            foreach (var item in items)
                Console.WriteLine($"{new string (' ', 4)}{ord++}.\t{item}");

            Console.WriteLine();
        }

        public void DisplayGreeting()
        {

        }

        public void DisplayHelp()
        {
            var list = currContext.CommandsInfo;

            Console.WriteLine("Available commands are:");

            // find max length of item in left section (where commands names, 
            // aliases and argument format string will be displayed)
			var maxLeftSectionItemWidth = 0;
            foreach (var cmdInfo in list) {
                var nameArgsLineLength = $"{cmdInfo.Name} {cmdInfo.ArgsFormat}".Length;
                if (nameArgsLineLength > maxLeftSectionItemWidth)
                    maxLeftSectionItemWidth = nameArgsLineLength;

                var aliasesLineLength = 
                    $"({string.Join(", ", cmdInfo.Aliases ?? Array.Empty<string>())})".Length;
                if (aliasesLineLength > maxLeftSectionItemWidth)
                    maxLeftSectionItemWidth = aliasesLineLength;
            }

            var descriptionOffset = maxLeftSectionItemWidth + 8;

            if (descriptionOffset > Console.WindowWidth + 5)
                Console.WriteLine("Window width is too narrow to display any help");

            foreach (var cmdInfo in list)
                displayCmdInfo(cmdInfo, descriptionOffset);

            Console.WriteLine(); // additional empty line
        }


        Dictionary<string, Type> findContextClasses()
        {
            var allTypes = typeof(AppUI).Assembly.GetTypes();
            var result = new Dictionary<string, Type>();

            foreach (var t in allTypes)
            {
                var name = UIContext.GetName(t);

                if (name != null)
                {
                    try {
                        result.Add(name.ToLower(), t);
                    } catch (ArgumentException e) {
                        throw new UIContextClassesLoadException(
                            $"UI Context name '{name}' of class {t.FullName} isn't" +
                            "unique (names are case insensitive)", e);
                    }
                }
            }

            return result;
        }

        void displayCmdInfo(CommandInfo cmdInfo, int offset)
        {
            var width = Console.WindowWidth;

            Console.WriteLine($"{new string(' ', 4)}{cmdInfo.Name} {cmdInfo.ArgsFormat}");

            if (cmdInfo.Aliases != null)
                Console.WriteLine($"{new string(' ', 4)}({string.Join(", ", cmdInfo.Aliases)})");

            var description = cmdInfo.Description ?? "";
            var startIndex = 0;
            var cursorTop = Console.CursorTop;
            Console.CursorTop -= cmdInfo.Aliases != null ? 2 : 1;
            while (startIndex < description.Length)
            {
                Console.CursorLeft = offset;
                Console.WriteLine(description.Substring(
                    startIndex,
                    Math.Min(width - offset - 1, description.Length - startIndex)
                ));
                startIndex += width - offset - 1;
            }

            if (Console.CursorTop < cursorTop)
                Console.CursorTop = cursorTop;

            Console.WriteLine();
        }
    }
}
