/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/



using System.Collections.Generic;

namespace Redux.Vulkan
{
    public class PipelineVertexInput
    {
        public PipelineVertexInput()
        {

        }

        public List<VertexInputBinding> VertexBindingDescriptions { get; set; } = new List<VertexInputBinding>();
        public List<VertexInputAttribute> VertexAttributeDescriptions { get; set; } = new List<VertexInputAttribute>();
    }
}
