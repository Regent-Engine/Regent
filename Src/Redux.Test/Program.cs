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
            Window window = new("Redux Engine", 800, 600);

            Settings Settings = new()
            {
                VSync = false,
                Fullscreen = false,
                Validation = ValidationType.None,
            };

            PresentationParameters parameters = new()
            {
                Width = window.Width,
                Height = window.Height,
                SwapchainSource = window.SwapchainWin32
            };

            Adapter adapter = new(Settings);
            Device device = new(adapter);
            SwapChain swapChain = new(device, parameters);
            Framebuffer framebuffer = new(swapChain);
            CommandBuffer commandBuffer = new(device, CommandBufferType.AsyncGraphics);


            window?.Show();
            window.RenderLoop(() => 
            {
                device.WaitIdle();

                commandBuffer.Begin(swapChain);
                commandBuffer.BeginFramebuffer(framebuffer);

                commandBuffer.Close();
                commandBuffer.Submit();

                swapChain.Present();
            });

        }
    }
}
