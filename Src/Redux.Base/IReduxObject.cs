﻿/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Piero Castillo. All Rights Reserved. https://github.com/PieroCastillo
    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/
using Redux.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Redux
{
    /// <summary>
    /// The Redux Object, with INotifyPropertyChanged Support
    /// </summary>
    public interface IReduxObject
    {
        public TValue GetValue<TValue>(ReduxProperty<TValue> reduxProperty);
        public void SetValue<TValue>(ReduxProperty<TValue> reduxProperty, TValue value);
    }
}
