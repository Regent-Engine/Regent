/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/

namespace Redux.Vulkan
{
    public enum OptionalDeviceExtensions
    {
        RayTracing = 1 << 0,
        ConservativeRasterization = 1 << 1,
        Multiview = 1 << 4,
        ShadingRate = 1 << 6,
    }
}
