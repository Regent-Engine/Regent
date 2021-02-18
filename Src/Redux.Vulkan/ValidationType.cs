/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/

namespace Redux.Vulkan
{
    public enum ValidationType
    {
        None = 1 << 0,
        Default = 1 << 1,
        Console = 1 << 2,
        ImGui = 1 << 3,
        Debug = 1 << 4,
    }
}
