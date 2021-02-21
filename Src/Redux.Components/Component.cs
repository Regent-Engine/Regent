/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Piero Castillo. All Rights Reserved. https://github.com/PieroCastillo
    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/
using Redux.Data;
using Redux.Visuals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Components
{
    public class Component : Visual, IReduxObject
    {
        public string Name
        {
            get => GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public readonly static ReduxProperty<string> NameProperty
            = ReduxPropertyRegister.Register<string>(nameof(Name));
    }
}
