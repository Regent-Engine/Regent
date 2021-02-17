using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ReduxPropertyAttribute : Attribute
    {
        public ReduxPropertyAttribute(ReduxPropertyType reduxPropertyType = ReduxPropertyType.Direct)
        {
            ReduxPropertyType = reduxPropertyType;
        }

        public ReduxPropertyType ReduxPropertyType
        {
            get;
        }
    }
}
