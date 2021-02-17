using System;
using Redux.Vulkan;
using Redux.Core;
using Redux.Desktop;
namespace Redux.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Adapter adapter = new();
            //Device device = new(adapter);
            //SwapChain swapChain = new(device, new PresentationParameters(120, 100));


            Window window = new Window("Redux Engine", 800, 600);


            window?.Show();
            window.RenderLoop(() => { });

        }
    }
}
