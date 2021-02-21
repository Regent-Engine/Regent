/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Piero Castillo. All Rights Reserved. https://github.com/PieroCastillo
    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Redux.Data
{
    public class ReduxProperty<TValue> : ReduxPropertyBase
    {
        internal ReduxProperty(string name, TValue defaultValue = default(TValue)) : base(name)
        {
            DefaultValue = defaultValue;
        }

        public void OverrideDefaultValue(TValue newDefaultValue)
        {
            DefaultValue = newDefaultValue;
        }

        public TValue DefaultValue
        {
            get;
            private set;
        }

        public Type ValueType
        {
            get => typeof(TValue);
        }

        internal void RaisePropertyChanged(IReduxObject sender, ReduxPropertyChangedEventArgs<TValue> e)
        {
            Changed?.Invoke(sender, e);
        }

        public event EventHandler<ReduxPropertyChangedEventArgs<TValue>> Changed;

        public static bool operator ==(ReduxProperty<TValue> left, ReduxProperty<TValue> right)
            => left.Name == right.Name & left.ValueType == right.ValueType;

        public static bool operator !=(ReduxProperty<TValue> left, ReduxProperty<TValue> right)
            => !(left == right);

        public static bool operator ==(ReduxPropertyBase left, ReduxProperty<TValue> right)
        {
            if(left is ReduxProperty<TValue> prop)
            {
                return left == right;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(ReduxPropertyBase left, ReduxProperty<TValue> right)
            => !(left == right);
    }

    public static class ReduxPropertyRegister
    {
        public static ReduxProperty<T> Register<T>(string name, T defaultValue = default(T)) => new ReduxProperty<T>(name, defaultValue);
    }
}
