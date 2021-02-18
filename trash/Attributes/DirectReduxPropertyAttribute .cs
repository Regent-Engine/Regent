using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DirectReduxPropertyAttribute : Attribute
    {
        public static string ToStringStatic() => "DirectReduxPropertyAttribute";
    }
}
