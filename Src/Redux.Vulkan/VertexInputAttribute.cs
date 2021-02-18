/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/

namespace Redux.Vulkan
{

    public class VertexInputAttribute
    {
        public VertexInputAttribute()
        {

        }

        public int Location { get; set; }
        public int Binding { get; set; }
        public PixelFormat Format { get; set; }
        public int Offset { get; set; }
    }
}
