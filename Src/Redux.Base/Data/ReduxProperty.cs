using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Data
{
    public class ReduxProperty<TOwner, TValue>
    {
        private ReduxProperty(string name) 
        {
            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

    }

    public static class ReduxPropertyRegister
    {
        public static ReduxProperty<TOwner, TValue> Register<TOwner, TValue>(string PropertyName) where TOwner : IReduxObject
        {
            return default;
        }
    }
}
