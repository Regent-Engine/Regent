using System;
using Regent.Vulkan;

namespace Regent.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Adapter adapter = new();


            Console.WriteLine(adapter.Description);
            Console.WriteLine(adapter.DeviceName);
            Console.WriteLine("----");
            foreach (var item in adapter.InstanceExtensionsNames) Console.WriteLine(item);

        }
    }
}
