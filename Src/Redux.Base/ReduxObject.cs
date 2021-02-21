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


        public TValue GetValue<TValue>(ReduxProperty<TValue> reduxProperty)
        {
            if (propertyValues.ContainsKey(reduxProperty.Name))
            {
                return (TValue)propertyValues[reduxProperty.Name];
            }
            else if(reduxProperty.DefaultValue != null)
            {
                return reduxProperty.DefaultValue;
            }
            else
            {
                return default;
            }
        }

        public void SetValue<TValue>(ReduxProperty<TValue> reduxProperty, TValue value)
        {
            if (propertyValues.ContainsKey(reduxProperty.Name))
            {
                var oldValue = (TValue)propertyValues[reduxProperty.Name];
                reduxProperty.RaisePropertyChanged(this, new ReduxPropertyChangedEventArgs<TValue>(reduxProperty.Name, this, oldValue, value, reduxProperty));
                OnPropertyChanged<TValue>(new ReduxPropertyChangedEventArgs<TValue>(reduxProperty.Name, this, oldValue, value, reduxProperty));
                propertyValues[reduxProperty.Name] = value;
            }
            else if (!propertyValues.ContainsKey(reduxProperty.Name))
            {
                var oldValue = default(TValue);
                reduxProperty.RaisePropertyChanged(this, new ReduxPropertyChangedEventArgs<TValue>(reduxProperty.Name, this, oldValue, value, reduxProperty));
                OnPropertyChanged<TValue>(new ReduxPropertyChangedEventArgs<TValue>(reduxProperty.Name, this, default, value, reduxProperty));
                propertyValues.Add(reduxProperty.Name, value);
            }
        }

        protected virtual void OnPropertyChanged<T>(ReduxPropertyChangedEventArgs<T> e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
