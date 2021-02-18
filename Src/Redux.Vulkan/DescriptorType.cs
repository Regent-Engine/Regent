﻿/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/

namespace Redux.Vulkan
{
    public enum DescriptorType
    {
        Sampler = 0,

        CombinedImageSampler = 1,

        SampledImage = 2,

        StorageImage = 3,

        UniformTexelBuffer = 4,

        StorageTexelBuffer = 5,

        UniformBuffer = 6,

        StorageBuffer = 7,

        UniformBufferDynamic = 8,

        StorageBufferDynamic = 9,

        InputAttachment = 10,

        InlineUniformBlockEXT = 1000138000,

        AccelerationStructureKHR = 1000165000,

        AccelerationStructureNV = 1000165000
    }
}
