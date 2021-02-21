/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Piero Castillo. All Rights Reserved. https://github.com/PieroCastillo
    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Data
{
    public class ReduxPropertyBase
    {
        internal ReduxPropertyBase(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }
    }
}
