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
    public class RasterizationState
    {
        public bool DepthClampEnable { get; set; }
        public bool RasterizerDiscardEnable { get; set; }
        public FillMode FillMode { get; set; }
        public CullMode CullMode { get; set; }
        public FrontFace FrontFace { get; set; }
        public bool DepthBiasEnable { get; set; }
        public float DepthBiasConstantFactor { get; set; }
        public float DepthBiasClamp { get; set; }
        public float DepthBiasSlopeFactor { get; set; }
        public float LineWidth { get; set; } = 1.0F;


        public static RasterizationState Default() => new RasterizationState()
        {
            FillMode = FillMode.Solid,
            CullMode = CullMode.None,
            FrontFace = FrontFace.Clockwise,
        };
    }
}
