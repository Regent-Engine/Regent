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
    [Flags]
    public enum BufferFlags
    {
        None = 0,


        ConstantBuffer = 1,


        IndexBuffer = 2,


        VertexBuffer = 4,


        RenderTarget = 8,


        ShaderResource = 16,


        UnorderedAccess = 32,
    }
}
