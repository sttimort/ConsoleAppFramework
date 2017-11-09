using System;
using System.Collections.Generic;
using Application;

namespace Application
{
    public interface IUIContext
    {
        IEnumerable<CommandInfo> CommandsInfo { get; }
        string CommandPrompt { get; }

        ICommand GetCommandFromInput(string input, out string errMsg);
    }
}
