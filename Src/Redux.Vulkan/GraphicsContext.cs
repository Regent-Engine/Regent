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
    public class GraphicsContext
    {
        public CommandBuffer CommandBuffer { get; set; }


        public GraphicsContext(Device graphicsDevice, CommandBuffer? commandBuffer = null)
        {
            CommandBuffer = commandBuffer is null ? graphicsDevice.NativeCommand : new CommandBuffer(graphicsDevice, CommandBufferType.AsyncGraphics);
        }
    }
}
