using System;
using Redux.Vulkan;
using Redux.Core;

namespace Redux.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Adapter adapter = new();
            Device device = new(adapter);
            SwapChain swapChain = new(device, new PresentationParameters(120, 100));

        }
    }
}
