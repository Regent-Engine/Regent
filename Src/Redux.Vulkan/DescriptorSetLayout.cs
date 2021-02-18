/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/



namespace Redux.Vulkan
{
    public class DescriptorSetLayout
    {
        public ShaderStage Stage { get; set; }
        public DescriptorType Type { get; set; }
        public int Binding { get; set; }

    }
}
