using System;
using Redux.Vulkan;

namespace Redux.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Adapter adapter = new();
            Device device = new(adapter);

        }
    }
}
