using System;

namespace Application.UI
{
    public interface IUIParser
    {
        string ParseInput(string input, out string args);
    }
}
