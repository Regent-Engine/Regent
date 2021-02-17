using System;

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
    public class CommandBuffer : Resource
    {
        internal uint imageIndex;


        public CommandBuffer(Device graphicsDevice, CommandBufferType type) : base(graphicsDevice)
        {
            Type = type;

            //Recreate();
        }

        public CommandBufferType Type { get; }
    }
}