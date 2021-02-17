namespace Redux.Vulkan
{
    public class Resource
    {
        public Resource(Device device)
        {
            NativeDevice = device;
        }
        public Device NativeDevice { get; set; }
    }
}