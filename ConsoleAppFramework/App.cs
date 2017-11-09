using System;
using Application.UI;

namespace Application
{
    public class App
    {
        public bool ShouldExit;
        public AppUI UI;
        public AppData Data;

        public App()
        {
            ShouldExit = false;

            UI = new AppUI();
            Data = new AppData();
        }

        public void Start()
        {
            while (!ShouldExit)
            {
                UI.NextCommand.Execute(this);
            }
        }
    }
}
