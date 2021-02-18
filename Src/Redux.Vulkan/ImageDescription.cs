﻿/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/

using System;
using Vortice.Vulkan;

namespace Redux.Vulkan
{
    public class ImageDescription
    {
        public ImageDimension Dimension { get; set; }

        public byte[] Data { get; set; } = Array.Empty<byte>();

        public int MipMaps { get; set; }

        public int Size { get; set; }

        public bool IsCubeMap { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Depth { get; set; }

        public int ArraySize { get; set; }

        public int MipLevels { get; set; }

        public PixelFormat Format { get; set; }

        public GraphicsResourceUsage Usage { get; set; }

        public TextureFlags Flags { get; set; }


        internal VkFormat format { get; set; }
    }
}