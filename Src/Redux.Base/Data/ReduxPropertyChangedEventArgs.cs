using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Redux.Data
{
    public class ReduxPropertyChangedEventArgs<TValue> : EventArgs
    {
        public ReduxPropertyChangedEventArgs(string name, TValue? oldValue, TValue? newValue)
        {
            Name = name;
            OldValue = oldValue;
            NewValue = newValue;
        }
        public TValue OldValue
        {
            get;
            private set;
        }

        public TValue NewValue
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }
    }

    public class ReduxPropertyChangedEventArgs : ReduxPropertyChangedEventArgs<object>
    {
        public ReduxPropertyChangedEventArgs(string name, object oldValue, object newValue) : base(name, oldValue, newValue)
        {

        }
    }
}
