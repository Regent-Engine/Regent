/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/

namespace Redux.Vulkan
{
    public enum FillMode : int
    {
        Solid = unchecked(0),

        Wireframe = unchecked(1),

        Point = unchecked(2),

        FillRectangleNV = unchecked(3)
    }
}