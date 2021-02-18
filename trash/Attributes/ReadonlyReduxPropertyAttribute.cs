using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ReadonlyReduxPropertyAttribute : Attribute
    {
        public static string ToStringStatic() => "ReadonlyReduxPropertyAttribute";
    }
}
