using System;
using Redux.Vulkan;

namespace Redux.Test
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
