/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Vulkan
{
    public enum DeviceType : int
    {
        Other = unchecked(0),

        IntegratedGPU = unchecked(1),

        DiscreteGPU = unchecked(2),

        VirtualGPU = unchecked(3),

        CPU = unchecked(4)
    }
}
