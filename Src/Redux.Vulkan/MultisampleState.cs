/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/

namespace Redux.Vulkan
{
    public class MultisampleState
    {
        public MultisampleCount MultisampleCount { get; set; }
        public bool SampleShadingEnable { get; set; }
        public float MinSampleShading { get; set; }
        public bool AlphaToCoverageEnable { get; set; }
        public bool AlphaToOneEnable { get; set; }


        public MultisampleState()
        {
            MultisampleCount = MultisampleCount.X1;
        }

    }
}