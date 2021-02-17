using System.Linq;
using Vortice.Mathematics;
using Vortice.Vulkan;
using static Vortice.Vulkan.Vulkan;

namespace Redux.Vulkan
{

    public enum CommandBufferType
    {
        Generic,

        AsyncGraphics,

        AsyncCompute,

        AsyncTransfer,

        Count
    }
    public unsafe class CommandBuffer : Resource
    {

        internal uint imageIndex;
        internal VkCommandBuffer handle;
        internal VkFence waitFences; // TODO: VkFence -> Fence(0)



        public CommandBuffer(Device graphicsDevice, CommandBufferType type) : base(graphicsDevice)
        {
            Type = type;

            Recreate();
        }

        public CommandBufferType Type { get; set; }


        public void Recreate()
        {

            switch (Type)
            {
                case CommandBufferType.Generic:
                    handle = NativeDevice.create_command_buffer_primary(NativeDevice.graphics_cmd_pool); // TODO: CommandBufferType.Count
                    break;

                case CommandBufferType.AsyncGraphics:
                    handle = NativeDevice.create_command_buffer_primary(NativeDevice.graphics_cmd_pool);
                    break;

                case CommandBufferType.AsyncCompute:
                    handle = NativeDevice.create_command_buffer_primary(NativeDevice.compute_cmd_pool);
                    break;

                case CommandBufferType.AsyncTransfer:
                    handle = NativeDevice.create_command_buffer_primary(NativeDevice.transfer_cmd_pool);
                    break;

                case CommandBufferType.Count:
                    handle = NativeDevice.create_command_buffer_primary(NativeDevice.graphics_cmd_pool); // TODO: CommandBufferType.Count
                    break;
            }

            // TODO: Fence
            // Fences (Used to check draw command buffer completion)
            VkFenceCreateInfo fenceCreateInfo = new()
            {
                sType = VkStructureType.FenceCreateInfo
            };

            // Create in signaled state so we don't wait on first render of each command buffer
            fenceCreateInfo.flags = VkFenceCreateFlags.Signaled;
            vkCreateFence(NativeDevice.handle, &fenceCreateInfo, null, out waitFences);

        }

        //public CommandBufferType GetPhysicalQueueType(CommandBufferType type)
        //{
        //    if (type != CommandBufferType.AsyncGraphics)
        //    {
        //        return type;
        //    }

        //    else
        //    {
        //        if (NativeDevice.GraphicsFamily == NativeDevice.ComputeFamily && NativeDevice.queueFamilyProperties /*graphics_queue*/ != NativeDevice.queueFamilyProperties /*compute_queue*/)
        //        {
        //            return CommandBufferType.AsyncCompute;
        //        }
        //        else
        //        {
        //            return CommandBufferType.Generic;
        //        }
        //    }
        //}

        public void Begin(SwapChain swapChain)
        {
            // By setting timeout to UINT64_MAX we will always wait until the next image has been acquired or an actual error is thrown
            // With that we don't have to handle VK_NOT_READY
            vkAcquireNextImageKHR(NativeDevice.handle, swapChain.handle, ulong.MaxValue, NativeDevice.image_available_semaphore, new VkFence(), out uint i);
            imageIndex = i;



            // Use a fence to wait until the command buffer has finished execution before using it again
            fixed (VkFence* ptrfence = &waitFences)
            {
                vkWaitForFences(NativeDevice.handle, 1, ptrfence, true, ulong.MaxValue);
                vkResetFences(NativeDevice.handle, 1, ptrfence);
            }


            VkCommandBufferBeginInfo beginInfo = new()
            {
                sType = VkStructureType.CommandBufferBeginInfo,
                flags = VkCommandBufferUsageFlags.RenderPassContinue,
            };

            vkBeginCommandBuffer(handle, &beginInfo);



        }


        public void BeginFramebuffer(Framebuffer framebuffer, float r = 0, float g = .2f, float b = .4f, float a = 1.0f)
        {
            // Set clear values for all framebuffer attachments with loadOp set to clear
            // We use two attachments (color and depth) that are cleared at the start of the subpass and as such we need to set clear values for both
            VkClearValue* clearValues = stackalloc VkClearValue[1];
            clearValues[0].color = new(r, g, b, a);
            //clearValues[1].depthStencil = new(1, 0);

            int h = framebuffer.SwapChain.Parameters.Height;
            int w = framebuffer.SwapChain.Parameters.Width;
            int x = 0;
            int y = 0;

            VkRenderPassBeginInfo renderPassBeginInfo = new()
            {
                sType = VkStructureType.RenderPassBeginInfo,
                renderArea = new(x, y, w, h),

                renderPass = framebuffer.renderPass,
                clearValueCount = 1,
                pClearValues = clearValues,
                framebuffer = framebuffer.framebuffers[imageIndex], // Set target frame buffer
            };

            vkCmdBeginRenderPass(handle, &renderPassBeginInfo, VkSubpassContents.Inline);
        }


        public void Clear(float R, float G, float B, float A = 1.0f)
        {
            VkClearColorValue clearValue = new(R, G, B, A);

            VkImageSubresourceRange clearRange = new()
            {
                aspectMask = VkImageAspectFlags.Color,
                baseMipLevel = 0,
                baseArrayLayer = 0,
                layerCount = 1,
                levelCount = 1
            };

            //vkCmdClearColorImage(NativeCommandBuffer, NativeDevice.SwapChain.Images[(int)imageIndex], VkImageLayout.ColorAttachmentOptimal, &clearValue, 1, &clearRange);
        }



        







        public void SetScissor(int width, int height, int x, int y)
        {
            // Update dynamic scissor state
            Rectangle scissor = new(x, y, width, height);

            vkCmdSetScissor(handle, 0, 1, &scissor);
        }

        public void SetViewport(float Width, float Height, float X, float Y, float MinDepth = 0.0f, float MaxDepth = 1.0f)
        {
            float vpY = Height + Y;
            float vpHeight = -Height;


            Viewport Viewport = new(X, Y, Width, Height, MinDepth, MaxDepth);

            vkCmdSetViewport(handle, 0, 1, &Viewport);
        }


        public void Draw(int vertexCount, int instanceCount, int firstVertex, int firstInstance)
        {
            vkCmdDraw(handle, (uint)vertexCount, (uint)instanceCount, (uint)firstVertex, (uint)firstInstance);
        }

        public void DrawIndexed(int indexCount, int instanceCount, int firstIndex, int vertexOffset, int firstInstance)
        {
            vkCmdDrawIndexed(handle, (uint)indexCount, (uint)instanceCount, (uint)firstIndex, vertexOffset, (uint)firstInstance);
        }





        public void Close()
        {
            CleanupRenderPass();
            vkEndCommandBuffer(handle);
        }


        internal unsafe void CleanupRenderPass()
        {
            vkCmdEndRenderPass(handle);
        }




        public void Submit()
        {
            VkSemaphore signalSemaphore = NativeDevice.render_finished_semaphore;
            VkSemaphore waitSemaphore = NativeDevice.image_available_semaphore;
            VkPipelineStageFlags waitStages = VkPipelineStageFlags.ColorAttachmentOutput;
            VkCommandBuffer commandBuffer = handle;


            VkSubmitInfo submitInfo = new()
            {
                sType = VkStructureType.SubmitInfo,
                waitSemaphoreCount = 1,
                pWaitSemaphores = &waitSemaphore,
                pWaitDstStageMask = &waitStages,
                pNext = null,
                commandBufferCount = 1,
                pCommandBuffers = &commandBuffer,
                signalSemaphoreCount = 1,
                pSignalSemaphores = &signalSemaphore,
            };

            vkQueueSubmit(NativeDevice.command_queue, 1, &submitInfo, waitFences);
        }

        public void Start()
        {

            VkCommandBufferBeginInfo cmdBufInfo = new VkCommandBufferBeginInfo()
            {
                flags = VkCommandBufferUsageFlags.OneTimeSubmit,
            };
            vkBeginCommandBuffer(handle, &cmdBufInfo);

        }

        public void End()
        {
            vkEndCommandBuffer(handle);
        }
    }
}