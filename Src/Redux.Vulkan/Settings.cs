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
    public class Settings
    {
        public ValidationType Validation { get; set; }

        public OptionalDeviceExtensions OptionalDeviceExtensions { get; set; }

        public bool Fullscreen { get; set; }

        public bool VSync { get; set; }

    }
}
