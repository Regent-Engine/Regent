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
    public class InputAssemblyState
    {
        public PrimitiveType PrimitiveType { get; set; }
        public bool PrimitiveRestartEnable { get; set; }

        public InputAssemblyState(PrimitiveType Type, bool RestartEnable = false)
        {
            PrimitiveType = Type;
            PrimitiveRestartEnable = RestartEnable;
        }

        public InputAssemblyState()
        {
            PrimitiveType = PrimitiveType.TriangleList;
            PrimitiveRestartEnable = false;
        }


        public static InputAssemblyState Default() => new InputAssemblyState()
        {
            PrimitiveRestartEnable = false,
            PrimitiveType = PrimitiveType.TriangleList
        };

    }
}
