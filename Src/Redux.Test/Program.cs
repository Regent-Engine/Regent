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
            Window window = new Window("Redux Engine", 800, 600);

            Adapter adapter = new();
            Device device = new(adapter);

            PresentationParameters parameters = new()
            {
                Width = window.Width >> 1,
                Height = window.Height >> 1,
                SwapchainSource = window.SwapchainWin32
            };

            SwapChain swapChain = new(device, parameters);

            Framebuffer framebuffer = new(swapChain);
            CommandBuffer commandBuffer = new CommandBuffer(device, CommandBufferType.AsyncGraphics);

            window?.Show();
            window.RenderLoop(() => 
            {
                device.WaitForGPU();

                commandBuffer.Begin(swapChain);
                commandBuffer.BeginFramebuffer(framebuffer);

                commandBuffer.Close();
                commandBuffer.Submit();

                swapChain.Present();
            });

        }
    }
}
