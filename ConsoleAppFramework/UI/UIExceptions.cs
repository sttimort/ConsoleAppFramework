using System;
namespace Application.UI
{
    public class UIException : Exception
    {
        public UIException(string msg = "")
            :base(msg)
        {}

        public UIException(string msg, Exception e)
            : base(msg, e)
        { }
    }

    public class UIContextClassesLoadException : UIException
    {
        public UIContextClassesLoadException(string msg = "")
            : base("UI Classes loading exception.\n" + 
                   msg)
        { }

        public UIContextClassesLoadException(string msg, Exception e)
            : base("UI Classes loading exception.\n" +msg, e)
        { }
    }

    public class UIContextConfigurationException : UIException
    {
        public UIContextConfigurationException(string msg = "")
            : base("UI Context configuration exception.\n" +
                   msg)
        { }
    }
}
