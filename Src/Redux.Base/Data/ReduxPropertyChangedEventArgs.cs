/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Piero Castillo. All Rights Reserved. https://github.com/PieroCastillo
    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Redux.Data
{
    public class ReduxPropertyChangedEventArgs<TValue> : ReduxPropertyChangedEventArgs
    {

        public ReduxPropertyChangedEventArgs(string name, IReduxObject sender, TValue oldValue, TValue newValue, ReduxProperty<TValue> changedProperty) : 
            base(name, sender, oldValue, newValue, changedProperty)
        {
            ChangedProperty = changedProperty;
        }

        public new ReduxProperty<TValue> ChangedProperty
        {
            get;
            private set;
        }
    }

    public abstract class ReduxPropertyChangedEventArgs : EventArgs
    {
        public ReduxPropertyChangedEventArgs(string name, IReduxObject sender, object? oldValue, object? newValue, ReduxPropertyBase changedProperty)
        {
            Name = name;
            OldValue = oldValue;
            NewValue = newValue;
            Sender = sender;
        }
        public object OldValue
        {
            get;
            private set;
        }

        public object NewValue
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public IReduxObject Sender
        {
            get;
            private set;
        }

        public ReduxPropertyBase ChangedProperty
        {
            get;
            private set;
        }
    }
}
