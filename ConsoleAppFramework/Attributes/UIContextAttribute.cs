using System;

namespace Application
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class UIContextAttribute : Attribute
    {
        public UIContextAttribute()
        {
            IsMain = false;
        }

        public string Name { get; set; }
        public bool IsMain { get; set; }
    }
}