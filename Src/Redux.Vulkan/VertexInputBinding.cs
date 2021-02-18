/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/

namespace Redux.Vulkan
{
    public class VertexInputBinding
    {
        public VertexInputBinding()
        {

        }

        public int Binding { get; set; }
        public int Stride { get; set; }
        public VertexInputRate InputRate { get; set; }
    }
}
