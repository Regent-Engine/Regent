using Redux.Attributes;
using Redux.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Redux
{
    /// <inheritdoc/>
    public class ReduxObject : IReduxObject
    {
        public event EventHandler<ReduxPropertyChangedEventArgs> PropertyChanged;

        private Dictionary<string, object> propertyValues = new();

        public ReduxObject()
        {

        }


        public TValue GetValue<TValue>(ReduxProperty<ReduxObject, TValue> reduxProperty)
        {
            return (TValue)propertyValues[reduxProperty.Name];
        }

        public void SetValue<TValue>(ReduxProperty<ReduxObject, TValue> reduxProperty, TValue value)
        {
            if (propertyValues.ContainsKey(reduxProperty.Name))
            {
                var oldValue = (TValue)propertyValues[reduxProperty.Name];

                OnPropertyChanged<TValue>(new ReduxPropertyChangedEventArgs<TValue>(reduxProperty.Name, oldValue, value));

                propertyValues[reduxProperty.Name] = value;
            }
            else if (!propertyValues.ContainsKey(reduxProperty.Name))
            {
                OnPropertyChanged<TValue>(new ReduxPropertyChangedEventArgs<TValue>(reduxProperty.Name, default, value));

                propertyValues.Add(reduxProperty.Name, value);
            }
        }

        protected virtual void OnPropertyChanged<T>(ReduxPropertyChangedEventArgs<T> e)
        {
            PropertyChanged.Invoke(this, new ReduxPropertyChangedEventArgs(e.Name, e.OldValue, e.NewValue));
        }
    }
}
