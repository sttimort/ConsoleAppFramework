using System;

namespace Application
{
    public interface ICommand
    {
        void Execute(App app);
    }
}
